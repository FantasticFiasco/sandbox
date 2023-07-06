#include <stdio.h>

int main()
{
    FILE *fp = fopen("data.txt", "w");
    if (fp == NULL)
    {
        printf("Error opening file!\n");
        return 1;
    }

    double celcius[11] = {0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
    double farenheit;

    for (int i = 0; i < sizeof(celcius) / sizeof(celcius[0]); i++)
    {
        farenheit = (celcius[i] * 9 / 5) + 32;
        fprintf(fp, "%10.1f %10.1f\n", celcius[i], farenheit);
    }

    fclose(fp);
    
    return 0;
}