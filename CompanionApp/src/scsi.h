#ifndef SCSI_H
#define SCSI_H
#include "dvd_device.h"
#include <stdint.h>

int scsi_read10(dvd_device *dev, int lba, int blocks, uint8_t *out_buffer);

void scsi_test_unit_ready(dvd_device *dev);
void scsi_read_capacity(dvd_device *dev);
void set_sense(dvd_device *dev, uint8_t key, uint8_t asc, uint8_t ascq);
int scsi_request_sense(dvd_device *dev, uint8_t *buffer);
int scsi_inquiry(dvd_device *dev, uint8_t *buffer, int alloc_len);
int scsi_mode_sense10(dvd_device *dev, uint8_t *buffer, int alloc_len);
int scsi_read_toc(dvd_device *dev, uint8_t *buffer, int alloc_len);
int scsi_get_configuration(dvd_device *dev, uint8_t *buffer, int alloc_len);
int scsi_read_capacity10(dvd_device *dev, uint8_t *buffer);

#endif