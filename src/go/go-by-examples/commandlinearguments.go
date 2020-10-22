package main

import (
	"fmt"
	"os"
	"strings"
)

func main() {
	argsWithProg := os.Args
	fmt.Println(strings.Join(argsWithProg, " "))

	argsWithoutProg := os.Args[1:]
	fmt.Println(strings.Join(argsWithoutProg, " "))
}
