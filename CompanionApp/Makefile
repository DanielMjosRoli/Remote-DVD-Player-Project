CC=gcc
CFLAGS=-Wall -Wextra -Werror -O2

SRC=src/main.c src/scsi.c src/iso_reader.c src/dvd_device.c src/utils.c 
OUT=dvd_emulator

all:
	$(CC) $(CFLAGS) $(SRC) -o $(OUT)

clean:
	rm -f $(OUT)