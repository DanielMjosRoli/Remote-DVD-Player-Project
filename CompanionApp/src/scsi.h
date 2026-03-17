#ifndef SCSI_H
#define SCSI_H
#include "dvd_device.h"
#include <stdint.h>

int scsi_read10(dvd_device *dev, int lba, int blocks, uint8_t *out_buffer);

void scsi_inquiry();
void scsi_test_unit_ready(dvd_device *dev);
void scsi_read_capacity(dvd_device *dev);

#endif