// The GLib library contains many useful data structures and building blocks
// such as linked lists, hash tables, and others.
//
// The GNU Scientific Library provides a broad range of numerical and
// mathematical functions. If you want to do something in C that you have been
// doing in MATLAB, or Python/NumPy/SciPy, or R, then probably you will need to
// look at something like GSL for functions you need (or code them up yourself).
//
// To install GSL on macOS, run:
//
//   brew install gsl
//
// To build the application, run:
//
//   gcc -o gsl gsl.c -lgsl -lgslcblas

#include <stdio.h>
#include <gsl/gsl_linalg.h>

int main(void)
{
    double a_data[] = {0.18, 0.60, 0.57, 0.96,
                       0.41, 0.24, 0.99, 0.58,
                       0.14, 0.30, 0.97, 0.66,
                       0.51, 0.13, 0.19, 0.85};

    double b_data[] = {1.0, 2.0, 3.0, 4.0};

    gsl_matrix_view m = gsl_matrix_view_array(a_data, 4, 4);

    gsl_vector_view b = gsl_vector_view_array(b_data, 4);

    gsl_vector *x = gsl_vector_alloc(4);

    int s;

    gsl_permutation *p = gsl_permutation_alloc(4);

    gsl_linalg_LU_decomp(&m.matrix, p, &s);

    gsl_linalg_LU_solve(&m.matrix, p, &b.vector, x);

    printf("x = \n");
    gsl_vector_fprintf(stdout, x, "%g");

    gsl_permutation_free(p);
    gsl_vector_free(x);
    return 0;
}