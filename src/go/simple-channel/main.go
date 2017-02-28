package main

import (
	"fmt"
)

func someThing(parameterOne string, parameterTwo int, exitChannel chan bool) {
	fmt.Println("parameterOne: ", parameterOne, " parameterTwo: ", parameterTwo)
	exitChannel <- true
}

func main() {
	exitChannel := make(chan bool)

	fmt.Println("calling someThing")
	go someThing("aaaa", 1980, exitChannel)
	fmt.Println("done calling someThing")

	fmt.Println("waiting for stragglers to exit")

	<-exitChannel

	fmt.Println("and now (finally) exiting")
}
