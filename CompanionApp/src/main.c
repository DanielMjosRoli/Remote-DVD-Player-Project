/*
#include <stdio.h>
#include <string.h>
#include "dvd_device.h"
#include "scsi.h"
#include "utils.h"
#include <stdlib.h>

int main() {

    dvd_device dev = {0};
    char command[256];

    printf("Virtual DVD Emulator\n\n");

    while (1) {

        printf("> ");
        if (!fgets(command, sizeof(command), stdin)) {
            printf("Input error or EOF\n");
            break;
        }

        if (strncmp(command, "INSERT", 6) == 0) {

            char path[200];
            sscanf(command, "INSERT %s", path);

            insert_disc(&dev, path);
        }

        else if (strncmp(command, "EJECT", 5) == 0) {
            eject_disc(&dev);
        }

        else if (strncmp(command, "READ_CAPACITY10", 15) == 0) {

            uint8_t buffer[8];

            int len = scsi_read_capacity10(&dev, buffer);

            print_hex(buffer, len);
        }

        else if (strncmp(command, "READ_TOC", 8) == 0) {

            uint8_t buffer[32];

            int len = scsi_read_toc(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        else if (strncmp(command, "GET_CONFIGURATION", 17) == 0) {

            uint8_t buffer[64];

            int len = scsi_get_configuration(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        else if (strncmp(command, "MODE_SENSE10", 12) == 0) {

            uint8_t buffer[64];

            int len = scsi_mode_sense10(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        else if (strncmp(command, "TEST_UNIT_READY", 15) == 0) {
            scsi_test_unit_ready(&dev);
        }

        else if (strncmp(command, "READ_CAPACITY", 13) == 0) {
            scsi_read_capacity(&dev);
        }

        else if (strncmp(command, "READ10", 6) == 0) {

            int lba, blocks;
            sscanf(command, "READ10 %d %d", &lba, &blocks);

            uint8_t *buffer = malloc(2048 * blocks);

            if (!buffer) {
                printf("Memory allocation failed\n");
                return 1;
            }

            if (scsi_read10(&dev, lba, blocks, buffer) == 0) {
                print_hex(buffer, 64);
            }
            free(buffer);
        }

        else if (strncmp(command, "EXIT", 4) == 0) {
            break;
        }

        else if (strncmp(command, "REQUEST_SENSE", 13) == 0) {

            uint8_t buffer[18];

            int len = scsi_request_sense(&dev, buffer);

            print_hex(buffer, len);
        }

        else if (strncmp(command, "INQUIRY", 7) == 0) {

            uint8_t buffer[96];

            int len = scsi_inquiry(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        else {
            printf("Unknown command\n\n");
        }
    }

    return 0;
}
*/
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include "dvd_device.h"
#include "scsi.h"
#include "utils.h"

int main() {

    dvd_device dev = {0};
    char command[256];

    printf("Virtual DVD Emulator\n\n");

    while (1) {

        printf("> ");

        if (!fgets(command, sizeof(command), stdin)) {
            printf("Input error or EOF\n");
            break;
        }

        // ✅ Strip newline
        command[strcspn(command, "\n")] = 0;

        // -------------------------
        // INSERT <file>
        // -------------------------
        if (strncmp(command, "INSERT ", 7) == 0) {

            char path[200];

            if (sscanf(command + 7, "%199s", path) == 1) {
                insert_disc(&dev, path);
            } else {
                printf("Usage: INSERT <file>\n");
            }
        }

        // -------------------------
        // EJECT
        // -------------------------
        else if (strcmp(command, "EJECT") == 0) {
            eject_disc(&dev);
        }

        // -------------------------
        // READ CAPACITY (10)
        // -------------------------
        else if (strcmp(command, "READ_CAPACITY10") == 0) {

            uint8_t buffer[8];

            int len = scsi_read_capacity10(&dev, buffer);

            print_hex(buffer, len);
        }

        // -------------------------
        // READ TOC
        // -------------------------
        else if (strcmp(command, "READ_TOC") == 0) {

            uint8_t buffer[32];

            int len = scsi_read_toc(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        // -------------------------
        // GET CONFIGURATION
        // -------------------------
        else if (strcmp(command, "GET_CONFIGURATION") == 0) {

            uint8_t buffer[64];

            int len = scsi_get_configuration(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        // -------------------------
        // MODE SENSE (10)
        // -------------------------
        else if (strcmp(command, "MODE_SENSE10") == 0) {

            uint8_t buffer[64];

            int len = scsi_mode_sense10(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        // -------------------------
        // TEST UNIT READY
        // -------------------------
        else if (strcmp(command, "TEST_UNIT_READY") == 0) {
            scsi_test_unit_ready(&dev);
        }

        // -------------------------
        // READ(10)
        // -------------------------
        else if (strncmp(command, "READ10 ", 7) == 0) {

            int lba, blocks;

            if (sscanf(command + 7, "%d %d", &lba, &blocks) != 2) {
                printf("Usage: READ10 <lba> <blocks>\n");
                continue;
            }

            if (blocks <= 0) {
                printf("Invalid block count\n");
                continue;
            }

            uint8_t *buffer = malloc(2048 * blocks);

            if (!buffer) {
                printf("Memory allocation failed\n");
                continue;
            }

            if (scsi_read10(&dev, lba, blocks, buffer) == 0) {
                print_hex(buffer, 64);  // preview
            }

            free(buffer);
        }

        // -------------------------
        // REQUEST SENSE
        // -------------------------
        else if (strcmp(command, "REQUEST_SENSE") == 0) {

            uint8_t buffer[18];

            int len = scsi_request_sense(&dev, buffer);

            print_hex(buffer, len);
        }

        // -------------------------
        // INQUIRY
        // -------------------------
        else if (strcmp(command, "INQUIRY") == 0) {

            uint8_t buffer[96];

            int len = scsi_inquiry(&dev, buffer, sizeof(buffer));

            print_hex(buffer, len);
        }

        // -------------------------
        // EXIT
        // -------------------------
        else if (strcmp(command, "EXIT") == 0) {
            break;
        }

        else if (strncmp(command, "CDB ", 4) == 0) {

            uint8_t cdb[16] = {0};
            int cdb_len = 0;

            char *ptr = command + 4;
            
            // Parse hex bytes
            while (*ptr && cdb_len < 16) {

                unsigned int byte;

                if (sscanf(ptr, "%2x", &byte) != 1)
                    break;

                cdb[cdb_len++] = (uint8_t)byte;

                // Move to next byte
                while (*ptr && *ptr != ' ') ptr++;
                while (*ptr == ' ') ptr++;
            }

            if (cdb_len == 0) {
                printf("Invalid CDB input\n");
                continue;
            }

            uint8_t buffer[4096];

            uint8_t status;

            int len = scsi_dispatch(&dev, cdb, cdb_len, buffer, sizeof(buffer), &status);

            printf("Status: 0x%02X\n", status);

            if (len > 0) {
                print_hex(buffer, len > 64 ? 64 : len);
            }
        }

        else {
            printf("Unknown command\n\n");
        }
    }

    return 0;
}