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