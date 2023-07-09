// gcc ReadFromFile.c

#include <stdio.h>

int main()
{
    FILE *fp = fopen("data.txt", "r");
    if (fp == NULL)
    {
        printf("Error opening file!\n");
        return 1;
    }

    double celcius, farenheit;

    while (fscanf(fp, "%lf %lf", &celcius, &farenheit) == 2)
    {
        printf("%10.1f°C equals %10.1f°F\n", celcius, farenheit);
    }

    fclose(fp);
}
