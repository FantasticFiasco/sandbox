// gcc pthread.c

// POSIX Threads (Pthreads for short) is a standard for programming with
// threads, and defines a set of C types, functions and constants.
//
// More generally, threads are a way that a program can spawn concurrent units
// of processing that can then be delegated by the operating system to multiple
// processing cores. Clearly the advantage of a multithreaded program (one that
// uses multiple threads that are assigned to multiple processing cores) is that
// you can achieve big speedups, as all cores of your CPU (and all CPUs if you
// have more than one) are used at the same time.

#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

#define NTHREADS 5

void *myFun(void *x)
{
    int thread_id = *((int *)x);
    printf("Hi from thread %d!\n", thread_id);
    return NULL;
}

int main(int argc, char *argv[])
{
    pthread_t threads[NTHREADS];
    int thread_args[NTHREADS];
    int rc, i;

    /* spawn the threads */
    for (i = 0; i < NTHREADS; ++i)
    {
        thread_args[i] = i;
        printf("spawning thread %d\n", i);
        rc = pthread_create(&threads[i], NULL, myFun, (void *)&thread_args[i]);
    }

    /* wait for threads to finish */
    for (i = 0; i < NTHREADS; ++i)
    {
        rc = pthread_join(threads[i], NULL);
    }

    return 1;
}