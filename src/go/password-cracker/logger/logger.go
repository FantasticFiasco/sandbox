package logger

import "fmt"

func Infoln(a ...interface{}) {
	fmt.Println(a...)
}

func Infof(format string, a ...interface{}) {
	fmt.Printf(format, a...)
}
