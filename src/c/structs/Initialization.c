// gcc Initialization.c

#include <stdio.h>

typedef struct
{
    char *first_name;
    char *surname;
    int age;
} Person;

void print_person(Person p)
{
    printf("%s %s is %d years old\n", p.first_name, p.surname, p.age);
}

int main()
{
    Person p = {.first_name = "John", .surname = "Doe", .age = 42};
    print_person(p);

    return 0;
}