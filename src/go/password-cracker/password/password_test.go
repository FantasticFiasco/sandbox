package password

import (
	"fmt"
	"testing"
)

func TestGenerate(t *testing.T) {
	c := make(chan []byte)
	go Generate(2, c)
	for count := 1; ; count++ {
		v, more := <-c
		if !more {
			break
		}
		fmt.Println(count, string(v))
	}
}
