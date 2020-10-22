package main

import "fmt"

type point struct {
	x, y int
}

func main() {
	p := point{1, 2}

	// this prints an instance of our point struct
	fmt.Printf("%v\n", p)

	// the %+v variant will include the struct’s field names
	fmt.Printf("%+v\n", p)

	// prints a Go syntax representation of the value
	fmt.Printf("%#v\n", p)

	// print the type of a value
	fmt.Printf("%T\n", p)

	// formatting booleans
	fmt.Printf("%t\n", true)

	// formatting integers
	fmt.Printf("%d\n", 123)

	// binary representation
	fmt.Printf("%b\n", 14)

	// character corresponding to the given integer
	fmt.Printf("%c\n", 33)

	// hex encoding
	fmt.Printf("%x\n", 456)

	// basic decimal formatting
	fmt.Printf("%f\n", 78.9)

	// scientific notation
	fmt.Printf("%e\n", 123400000.0)
	fmt.Printf("%E\n", 123400000.0)

	// basic string printing
	fmt.Printf("%s\n", "\"string\"")

	// double-quote strings
	fmt.Printf("%q\n", "\"string\"")

	// renders the string in base-16
	fmt.Printf("%x\n", "hex this")

	// print a representation of a pointer
	fmt.Printf("%p\n", &p)

	// control the width and precision of the resulting figure
	fmt.Printf("|%6d|%6d|\n", 12, 345)

	// width of printed floats, though usually you’ll also want to restrict the decimal precision
	// at the same time with the width.precision syntax
	fmt.Printf("|%6.2f|%6.2f|\n", 1.2, 3.45)

	// left-justify, use the - flag
	fmt.Printf("|%-6.2f|%-6.2f|\n", 1.2, 3.45)

	// basic right-justified width
	fmt.Printf("|%6s|%6s|\n", "foo", "b")

	// left-justify use the - flag as with numbers
	fmt.Printf("|%-6s|%-6s|\n", "foo", "b")

	// formats and returns a string without printing it anywhere
	s := fmt.Sprintf("a %s", "string")
	fmt.Println(s)
}
