package password

import (
	"testing"
)

func TestGenerateWithLength1(t *testing.T) {
	c := make(chan []byte)
	got := make([][]byte, 0)
	go Generate(1, c)
	for {
		v, more := <-c
		if !more {
			break
		}
		got = append(got, v)
	}
	expect := [][]byte{
		{'a'},
		{'b'},
		{'c'},
		{'d'},
		{'e'},
		{'f'},
		{'g'},
		{'h'},
		{'i'},
		{'j'},
		{'k'},
		{'l'},
		{'m'},
		{'n'},
		{'o'},
		{'p'},
		{'q'},
		{'r'},
		{'s'},
		{'t'},
		{'u'},
		{'v'},
		{'w'},
		{'x'},
		{'y'},
		{'z'}}
	if len(got) != len(expect) {
		t.Errorf("got %d passwords; expected %d", len(got), len(expect))
	}
	for i := 0; i < len(got); i++ {
		if string(got[i]) != string(expect[i]) {
			t.Errorf("got %s; expect %s", string(got[i]), string(expect[i]))
		}
	}
}
