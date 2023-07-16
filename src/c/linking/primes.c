#define TRUE 1
#define FALSE 0

int is_prime(int number)
{
    if (number < 2)
        return FALSE;

    if (number == 2)
        return TRUE;

    if (number % 2 == 0)
        return FALSE;

    for (int i = 3; i < number / 2; i += 2)
    {
        if (number % i == 0)
            return FALSE;
    }

    return TRUE;
}