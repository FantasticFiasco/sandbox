#include <stdio.h>
#include <stdlib.h>

typedef struct {
    char *first_name;
    char *surname;
    int age;
} Person;

void print(Person p[], int n)
{
    for (int i = 0; i < n; i++)
    {
        printf("%s %s is %d year old\n", p[i].first_name, p[i].surname, p[i].age);
    }

    printf("\n");
}

int main()
{
    int count = 1;

    Person *persons = calloc(count, sizeof(Person));

    persons[0].first_name = "John";
    persons[0].surname = "Doe";
    persons[0].age = 42;

    print(persons, count);

    count = 2;
    persons = realloc(persons, count * sizeof(Person));

    persons[1].first_name = "Jane";
    persons[1].surname = "Doe";
    persons[1].age = 41;

    print(persons, count);

    free(persons);

    return 0;
}