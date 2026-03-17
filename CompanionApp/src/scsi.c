#include <stdio.h>
#include "scsi.h"
#include "dvd_device.h"
#include <stdint.h>
#include "iso_reader.h"
#include "constants.h"

void scsi_test_unit_ready(dvd_device *dev) {

    if (!dev->inserted) {
        printf("TEST UNIT READY: NOT READY (no disc)\n\n");
        return;
    }

    if (dev->changed) {
        printf("TEST UNIT READY: UNIT ATTENTION (disc changed)\n\n");
        dev->changed = 0;
        return;
    }

    printf("TEST UNIT READY: READY\n\n");
}

void scsi_inquiry() {

    printf("INQUIRY RESPONSE\n");
    printf("Vendor: HL-DT-ST\n");
    printf("Product: DVD-ROM GDR8164B\n");
    printf("Revision: 1.00\n\n");
}

void scsi_read_capacity(dvd_device *dev) {

    if (!dev->inserted) {
        printf("ERROR: No disc\n\n");
        return;
    }

    printf("READ CAPACITY\n");
    printf("Last LBA: %d\n", dev->sectors - 1);
    printf("Sector size: 2048\n\n");
}

int scsi_read10(dvd_device *dev, int lba, int blocks, uint8_t *out_buffer) {

    if (!dev->inserted) {
        printf("ERROR: No disc\n");
        return -1;
    }

    if (lba + blocks > dev->sectors) {
        printf("ERROR: LBA out of range\n");
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