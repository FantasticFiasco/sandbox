package password

import "passwordcracker/logger"

const (
	LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz"
	UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
	Numbers             = "0123456789"
)

func Generate(maxLength int, c chan<- string) {
	prefix := ""
	for length := 1; length <= maxLength; length++ {
		generateWithLength(prefix, length, c)
	}
}

func generateWithLength(prefix string, length int, c chan<- string) {
	logger.Infof("password: generate passwords with length %d", length)
	recursiveGenerate(prefix, length, c)
	close(c)
}

func recursiveGenerate(prefix string, remainingLength int, out chan<- string) {
	remainingLength--
	for _, c := range LowercaseCharacters + UppercaseCharacters + Numbers {
		p := prefix + string(c)
		if remainingLength > 0 {
			recursiveGenerate(p, remainingLength, out)
		} else {
			out <- p
		}
	}
}
