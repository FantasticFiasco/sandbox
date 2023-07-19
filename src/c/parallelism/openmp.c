// gcc -fopenmp openmp.c

// Parallelizing for loops is really simple. By default, loop iteration counters
// in OpenMP loop constructs in the for loop are set to private variables.

#include <stdio.h>
#include <omp.h>

int main(int argc, char **argv)
{
    int i, thread_id, nloops;

#pragma omp parallel private(thread_id, nloops)
    {
        nloops = 0;

#pragma omp for
        for (i = 0; i < 1000; ++i)
        {
            ++nloops;
        }

        thread_id = omp_get_thread_num();

        printf("Thread %d performed %d iterations of the loop.\n",
               thread_id, nloops);
    }

    return 0;
}