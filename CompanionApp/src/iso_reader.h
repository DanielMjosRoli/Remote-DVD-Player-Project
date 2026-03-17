#ifndef ISO_READER_H
#define ISO_READER_H

int read_sector(FILE *iso, int lba, unsigned char *buffer);

long get_iso_size(FILE *iso);

#endif

