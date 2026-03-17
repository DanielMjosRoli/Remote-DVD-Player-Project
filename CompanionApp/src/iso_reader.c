#include <stdio.h>
#include <stdlib.h>
#include "constants.h"


long get_iso_size(FILE *iso) {
    fseek(iso, 0, SEEK_END);
    long size = ftell(iso);
    rewind(iso);
    return size;
}

int read_sector(FILE *iso, int lba, unsigned char *buffer) {

    if (fseek(iso, lba * SECTOR_SIZE, SEEK_SET) != 0) {
        perror("fseek failed");
        return -1;
    }

    size_t read = fread(buffer, 1, SECTOR_SIZE, iso);

    if (read != SECTOR_SIZE) {
        perror("fread failed");
        return -1;
    }

    return 0;
}