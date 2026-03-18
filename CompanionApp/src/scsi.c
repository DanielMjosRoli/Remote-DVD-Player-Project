#include <stdio.h>
#include "scsi.h"
#include "dvd_device.h"
#include <stdint.h>
#include "iso_reader.h"
#include "constants.h"
#include "scsi_sense.h"
#include <string.h>

int scsi_read_toc(dvd_device *dev, uint8_t *buffer, int alloc_len) {

    (void)dev; // not used yet

    memset(buffer, 0, alloc_len);

    int total_len = 12;  // header (4) + 1 descriptor (8)

    if (alloc_len < total_len)
        total_len = alloc_len;

    // TOC Data Length (excluding these 2 bytes)
    buffer[0] = ((total_len - 2) >> 8) & 0xFF;
    buffer[1] = (total_len - 2) & 0xFF;

    // First and Last Track
    buffer[2] = 1;
    buffer[3] = 1;

    // Track Descriptor
    buffer[4] = 0x00;

    // ADR + CONTROL
    // 0x14 = data track, copy allowed
    buffer[5] = 0x14;

    buffer[6] = 1;  // Track number
    buffer[7] = 0x00;

    // Track Start Address (LBA = 0)
    buffer[8]  = 0x00;
    buffer[9]  = 0x00;
    buffer[10] = 0x00;
    buffer[11] = 0x00;

    return total_len;
}

int scsi_mode_sense10(dvd_device *dev, uint8_t *buffer, int alloc_len) {

    (void)dev; // not used yet

    memset(buffer, 0, alloc_len);

    int total_len = 8;  // minimal header only

    if (alloc_len < total_len)
        total_len = alloc_len;

    // Mode Data Length (does NOT include these 2 bytes)
    buffer[0] = ((total_len - 2) >> 8) & 0xFF;
    buffer[1] = (total_len - 2) & 0xFF;

    buffer[2] = 0x00;  // Medium type (obsolete, OK as 0)

    // Device-Specific Parameter
    buffer[3] = 0x80;  // Write Protect = 1 (VERY IMPORTANT)

    buffer[4] = 0x00;
    buffer[5] = 0x00;

    // Block Descriptor Length = 0 (no descriptors)
    buffer[6] = 0x00;
    buffer[7] = 0x00;

    return total_len;
}

int scsi_inquiry(dvd_device *dev, uint8_t *buffer, int alloc_len) {

    (void)dev;  // suppress unused warning
    memset(buffer, 0, alloc_len);

    int response_len = 36;
    if (alloc_len < response_len)
        response_len = alloc_len;

    // Peripheral Device Type: 0x05 = CD/DVD device
    buffer[0] = 0x05;

    // Removable medium
    buffer[1] = 0x80;

    // Version (SPC-3 is fine)
    buffer[2] = 0x05;

    // Response data format
    buffer[3] = 0x02;

    // Additional length (bytes after byte 4)
    buffer[4] = response_len - 5;

    // Vendor ID (8 bytes)
    memcpy(&buffer[8],  "HL-DT-ST", 8);         // LG drives

    // Product ID (16 bytes)
    memcpy(&buffer[16], "DVD-ROM GDR8164B", 16);

    // Product revision (4 bytes)
    memcpy(&buffer[32], "1.00", 4);

    return response_len;
}

int scsi_request_sense(dvd_device *dev, uint8_t *buffer) {

    memset(buffer, 0, 18);

    buffer[0] = 0x70; // current errors
    buffer[2] = dev->sense_key;
    buffer[7] = 10;   // additional length

    buffer[12] = dev->asc;
    buffer[13] = dev->ascq;

    return 18;
}

void set_sense(dvd_device *dev, uint8_t key, uint8_t asc, uint8_t ascq) {
    dev->sense_key = key;
    dev->asc = asc;
    dev->ascq = ascq;
}

void scsi_test_unit_ready(dvd_device *dev) {

    if (!dev->inserted) {
        set_sense(dev, SENSE_NOT_READY, ASC_MEDIUM_NOT_PRESENT, 0x00);
        printf("NOT READY\n\n");
        return;
    }

    if (dev->changed) {
        set_sense(dev, SENSE_UNIT_ATTENTION, ASC_MEDIUM_CHANGED, 0x00);
        dev->changed = 0;
        printf("UNIT ATTENTION\n\n");
        return;
    }

    set_sense(dev, SENSE_NO_SENSE, 0x00, 0x00);
    printf("READY\n\n");
}

void scsi_read_capacity(dvd_device *dev) {

    if (!dev->inserted) {
        printf("ERROR: No disc\n\n");
        return;
    }

    printf("READ CAPACITY 2\n");
    printf("Last LBA: %d\n", dev->sectors - 1);
    printf("Sector size: 2048\n\n");
}

int scsi_read10(dvd_device *dev, int lba, int blocks, uint8_t *out_buffer) {

    if (!dev->inserted) {
        set_sense(dev, SENSE_NOT_READY, ASC_MEDIUM_NOT_PRESENT, 0x00);
        return -1;
    }

    if (lba + blocks > dev->sectors) {
        set_sense(dev, SENSE_ILLEGAL_REQUEST, ASC_LBA_OUT_OF_RANGE, 0x00);
        return -1;
    }

    for (int i = 0; i < blocks; i++) {

        uint8_t *sector_ptr = out_buffer + (i * SECTOR_SIZE);

        if (read_sector(dev->iso, lba + i, sector_ptr) != 0) {
            printf("ERROR: Read failed at LBA %d\n", lba + i);
            return -1;
        }
    }

    return 0;
}

int scsi_read_capacity10(dvd_device *dev, uint8_t *buffer) {

    memset(buffer, 0, 8);

    if (!dev->inserted) {
        // No media → typically handled via REQUEST SENSE
        return 0;
    }

    uint32_t total_sectors = dev->size / SECTOR_SIZE;

    // VERY IMPORTANT
    uint32_t last_lba = total_sectors - 1;

    // Last LBA (big-endian)
    buffer[0] = (last_lba >> 24) & 0xFF;
    buffer[1] = (last_lba >> 16) & 0xFF;
    buffer[2] = (last_lba >> 8) & 0xFF;
    buffer[3] = last_lba & 0xFF;

    // Block size = 2048 bytes
    buffer[4] = 0x00;
    buffer[5] = 0x00;
    buffer[6] = 0x08;
    buffer[7] = 0x00;

    return 8;
}

int scsi_get_configuration(dvd_device *dev, uint8_t *buffer, int alloc_len) {

    (void)dev;

    memset(buffer, 0, alloc_len);

    int total_len = 12;  // header (8) + 1 feature (4)

    if (alloc_len < total_len)
        total_len = alloc_len;

    // Data Length (excluding these 4 bytes)
    int data_len = total_len - 4;

    buffer[0] = (data_len >> 24) & 0xFF;
    buffer[1] = (data_len >> 16) & 0xFF;
    buffer[2] = (data_len >> 8) & 0xFF;
    buffer[3] = data_len & 0xFF;

    buffer[4] = 0x00;
    buffer[5] = 0x00;

    // Current Profile = DVD-ROM (0x0010)
    buffer[6] = 0x00;
    buffer[7] = 0x10;

    // --- Feature Descriptor ---

    // Feature Code: Profile List (0x0000)
    buffer[8]  = 0x00;
    buffer[9]  = 0x00;

    // Flags:
    // bit 0 = current
    buffer[10] = 0x01;

    // Additional Length
    buffer[11] = 0x00;

    return total_len;
}