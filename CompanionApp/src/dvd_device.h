#ifndef DVD_DEVICE_H
#define DVD_DEVICE_H

#include <stdio.h>
#include <stdint.h>

typedef struct {
    FILE *iso;
    int inserted;
    int changed;
    long size;
    int sectors;
    uint8_t sense_key;
    uint8_t asc;
    uint8_t ascq;
} dvd_device;


void insert_disc(dvd_device *dev, const char *path);
void eject_disc(dvd_device *dev);


#endif