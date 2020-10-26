package password

import (
	"passwordcracker/logger"
)

const (
	LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz"
	UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
	Numbers             = "0123456789"
)

func Generate(length int, c chan<- []byte) {
	logger.Infof("password: generate passwords with length %d\n", length)
	passwordBuffer := make([]byte, length)
	recursiveGenerate(passwordBuffer, 0, c)
	close(c)
}

func recursiveGenerate(passwordBuffer []byte, i int, c chan<- []byte) {
	for _, r := range LowercaseCharacters {
		passwordBuffer[i] = byte(r)
		if i == len(passwordBuffer)-1 {
			password := make([]byte, len(passwordBuffer))
			copy(password, passwordBuffer)
			c <- password
		} else {
			recursiveGenerate(passwordBuffer, i+1, c)
		}
	}
}
