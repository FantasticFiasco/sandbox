// -llibrary searches the library library while linking. The m stands for libm,
// the library that contains <math.h>.
//
//   gcc -o go go.c -lm
// 
// To measure the execution time, run:
//
//   time ./go
//
// Now recompile it for the profiler, and have the profiler investigate it:
//
//   gcc -o go go.c -lm -pg
//   gprof go -p

#include <stdio.h>
#include <math.h>

#define MAXLOOP 1e7

double myFun1(double x)
{
    double a = sin(x);
    return a;
}

double myFun2(double x)
{
    double a = pow(x, 3);
    return a;
}

double myFun3(double x)
{
    double a = sqrt(x);
    return a;
}

int main(int argc, char *argv[])
{
    int i;
    double x;
    double xsum = 0.0;
    for (i = 1; i < MAXLOOP; i++)
    {
        x = myFun1(i) + myFun2(i) + myFun3(i);
        xsum += x;
    }
    printf("xsum = %.6f\n", xsum);
    return 0;
}