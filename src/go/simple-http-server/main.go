package main

import (
	"fmt"
	"log"
	"net/http"
	"time"
)

func main() {
	http.HandleFunc("/", rootHandler)
	http.HandleFunc("/isitlunchtimeyet", handleLunchTimeRequest)
	log.Fatal(http.ListenAndServe(":8080", nil))
}

func rootHandler(res http.ResponseWriter, req *http.Request) {
	fmt.Fprint(res, "Hello, Simple Talk\n")
}

func handleLunchTimeRequest(res http.ResponseWriter, req *http.Request) {
	now := time.Now()
	if now.Hour() > 11 && now.Hour() < 14 {
		fmt.Fprintln(res, "yes")
			return
	}

	fmt.Fprintln(res, "no")
}