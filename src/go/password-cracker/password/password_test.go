package password

import (
	"testing"
)

func TestGenerateWithLength1(t *testing.T) {
	c := make(chan []byte)
	go Generate(1, c)
	got := make([]string, 0)
	for {
		password, more := <-c
		if !more {
			break
		}
		got = append(got, string(password))
	}
	expect := []string{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s",
		"t", "u", "v", "w", "x", "y", "z"}
	if len(got) != len(expect) {
		t.Fatalf("got %d passwords; expected %d", len(got), len(expect))
	}
	for i := 0; i < len(got); i++ {
		if got[i] != expect[i] {
			t.Fatalf("got %s; expect %s", got[i], expect[i])
		}
	}
}

func TestGenerateWithLength2(t *testing.T) {
	c := make(chan []byte)
	go Generate(2, c)
	got := make([]string, 0)
	for {
		password, more := <-c
		if !more {
			break
		}
		got = append(got, string(password))
	}
	expectedLength := 676
	if len(got) != expectedLength {
		t.Fatalf("got %d passwords, expected %d", len(got), expectedLength)
	}
	expectedFirstPassword := "aa"
	if got[0] != expectedFirstPassword {
		t.Fatalf("got %s as first password; expected %s", got[0], expectedFirstPassword)
	}
	expectedLastPassword := "zz"
	if (got[len(got)-1]) != expectedLastPassword {
		t.Fatalf("got %s as first password; expected %s", got[len(got)-1], expectedLastPassword)
	}
}
