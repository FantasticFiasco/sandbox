package main

import (
	"fmt"
	"io/ioutil"
	"net/http"
	"strings"
)

const (
	address  = "192.168.1.227"
	port     = 80
	username = "test"
	password = "abc123!#"
)

func main() {
	c := http.Client{}
	url := fmt.Sprintf("http://%s:%d/axis-cgi/param.cgi?action=list&group=Network", address, port)
	req, err := http.NewRequest(http.MethodGet, url, nil)
	if err != nil {
		panic(err)
	}

	res, err := c.Do(req)
	if err != nil {
		panic(err)
	}
	defer res.Body.Close()
	printResponse(res)

	if res.StatusCode == http.StatusUnauthorized {
		digestHeader := res.Header["Www-Authenticate"]
		fmt.Println(digestHeader)
		kalle := strings.Split(digestHeader[0], ",")
		fmt.Println(kalle)
		olle := make(map[string]string)
		for _, v := range kalle {
			tt := strings.SplitN(v, "=", 2)
			olle[tt[0]] = tt[1]
		}
		fmt.Println(olle)
	}
}

func printResponse(res *http.Response) {
	fmt.Println("Status:", res.StatusCode)

	fmt.Println("Headers")
	for h, _ := range res.Header {
		fmt.Printf("  %s: %s\n", h, strings.Join(res.Header[h], ";"))
	}

	body, err := ioutil.ReadAll(res.Body)
	if err != nil {
		panic(err)
	}
	fmt.Println(string(body))
}
