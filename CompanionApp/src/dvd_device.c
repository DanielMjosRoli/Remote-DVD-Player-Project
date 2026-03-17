#include <stdio.h>
#include "dvd_device.h"

void insert_disc(dvd_device *dev, const char *path) {

    dev->iso = fopen(path, "rb");

    if (!dev->iso) {
        perror("Failed to open ISO");
        return;
    }

    fseek(dev->iso, 0, SEEK_END);
    dev->size = ftell(dev->iso);
    rewind(dev->iso);

    dev->sectors = dev->size / 2048;

    dev->inserted = 1;
    dev->changed = 1;

    printf("Disc inserted: %s\n", path);
}

void eject_disc(dvd_device *dev) {

    if (dev->iso) {
        fclose(dev->iso);
    }

    dev->iso = NULL;
    dev->inserted = 0;
    dev->changed = 1;

    printf("Disc ejected\n");
}