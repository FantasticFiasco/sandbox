#include <stdio.h>
#include <math.h>

// ax^2 + bx + c
void solve(double a, double b, double c)
{
    double temp = sqrt(pow(b, 2) - 4 * a * c);

    double x1 = (-b + temp) / (2 * a);
    double x2 = (-b - temp) / (2 * a);

    printf("x1 = %f, x3 = %f\n", x1, x2);
}

int main()
{
    solve(2, 4, -4);
    return 0;
}