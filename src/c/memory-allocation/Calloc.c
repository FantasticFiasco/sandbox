// gcc Calloc.c

#include <stdlib.h>
#include <stdio.h>

typedef struct {
    char *first_name;
    char *surname;
    int age;
} Person;

void print(Person *p)
{
    printf("%s %s is %d year old\n", p->first_name, p->surname, p->age);
}

int main()
{
    int count = 3;

    Person *persons = calloc(count, sizeof(Person));

    persons[0].first_name = "John";
    persons[0].surname = "Doe";
    persons[0].age = 42;

    for (int i = 0; i < count; i++)
    {
        print(&persons[i]);
    }

    free(persons);
    
    return 0;
}