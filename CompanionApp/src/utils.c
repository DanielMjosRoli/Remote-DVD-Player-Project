#include <stdio.h>
#include <stdint.h>

void print_hex(uint8_t *buf, int len) {

    for (int i = 0; i < len; i++) {

        printf("%02X ", buf[i]);

        if ((i + 1) % 16 == 0)
            printf("\n");
    }

    printf("\n");
}