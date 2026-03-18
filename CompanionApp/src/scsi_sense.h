#ifndef SCSI_SENSE_H
#define SCSI_SENSE_H

// Sense Keys
#define SENSE_NO_SENSE        0x00
#define SENSE_NOT_READY       0x02
#define SENSE_MEDIUM_ERROR    0x03
#define SENSE_ILLEGAL_REQUEST 0x05
#define SENSE_UNIT_ATTENTION  0x06

// Additional Sense Codes (ASC)
#define ASC_MEDIUM_NOT_PRESENT 0x3A
#define ASC_LBA_OUT_OF_RANGE   0x21
#define ASC_MEDIUM_CHANGED     0x28

#endif