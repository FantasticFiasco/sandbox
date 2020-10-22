package main

import (
	"fmt"
	"time"
)

func main() {
	now := time.Now()
	secs := now.Unix()
	nanos := now.UnixNano()

	millis := nanos / 1000000

	fmt.Println(now)
	fmt.Println("s: ", secs)
	fmt.Println("ms:", millis)
	fmt.Println("ns:", nanos)

	fmt.Println(time.Unix(secs, 0))
	fmt.Println(time.Unix(0, nanos))
}
