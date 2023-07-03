#include <stdio.h>

double fahrenheit_to_celcius(double fahrenheit)
{
    return (fahrenheit - 32) / 1.8;
}

int main()
{
    printf("%d F == %.1f C\n", 0, fahrenheit_to_celcius(0));
    printf("%d F == %.1f C\n", 100, fahrenheit_to_celcius(100));
}

