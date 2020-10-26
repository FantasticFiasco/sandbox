package password

import "passwordcracker/logger"

const (
	LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz"
	UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
	Numbers             = "0123456789"
)

func Generate(prefix string, length int, c chan<- string) {
	logger.Infof("password: generate passwords with length %d\n", length)
	recursiveGenerate(prefix, length, c)
	close(c)
}

func recursiveGenerate(prefix string, remainingLength int, out chan<- string) {
	remainingLength--
	for _, c := range LowercaseCharacters + UppercaseCharacters + Numbers {
		p := prefix + string(c)
		if remainingLength == 0 {
			out <- p
		} else {
			recursiveGenerate(p, remainingLength, out)
		}
	}
}
