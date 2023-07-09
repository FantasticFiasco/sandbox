// gcc Initialization.c

#include <stdio.h>

void print_array(int array[], int length)
{
    for (int i = 0; i < length; i++)
    {
        printf("%d: %d\n", i, array[i]);
    }

    printf("\n");
}

int main()
{
    // Uninitialized
    int x[5];
    print_array(x, 5);

    // Initialized with zeros
    int y[5] = {};
    print_array(y, 5);

    // Initialized with specific values
    int z[5] = {[0] = 3, [4] = 6};
    print_array(z, 5);
}