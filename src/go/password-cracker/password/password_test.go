package password

import (
	"fmt"
	"testing"
)

func TestGenerate(t *testing.T) {
	c := make(chan string)
	go Generate("", 1, c)
	for count := 1; ; count++ {
		v, more := <-c
		if !more {
			break
		}
		fmt.Println(count, v)
	}
}
