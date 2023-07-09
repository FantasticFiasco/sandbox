// gcc main.c primes.c log/logger.c

#include <stdio.h>
#include "primes.h"
#include "log/logger.h"

int main()
{
    char message[256];

    for (int i = 1; i < 20; i++)
    {
        int result = is_prime(i);

        sprintf(message, "%2d is %s", i, result != 0 ? "a prime\n": "not a prime\n");
        info(message);
    }

    return 0;
}