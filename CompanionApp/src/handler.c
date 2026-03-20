#include <stdio.h>
#include <stdint.h>
#include <string.h>

#include "dvd_device.h"
#include "scsi.h"

dvd_device dev;

int main() {

    // Initialize your device
    insert_disc(&dev, "fake.iso");

    while (1) {

        uint8_t cdb[16];
        uint8_t buffer[4096];
        uint8_t status;

        int cdb_len;

        // Placeholder: simulate receiving command
        if (fread(&cdb_len, sizeof(int), 1, stdin) != 1)
            break;

        fread(cdb, 1, cdb_len, stdin);

        int len = scsi_dispatch(&dev, cdb, cdb_len, buffer, sizeof(buffer), &status);

        fwrite(&status, 1, 1, stdout);
        fwrite(&len, sizeof(int), 1, stdout);

        if (len > 0)
            fwrite(buffer, 1, len, stdout);

        fflush(stdout);
    }

    return 0;
}