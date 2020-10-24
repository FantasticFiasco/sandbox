package main

import (
	"crypto/md5"
	"errors"
	"fmt"
	"io/ioutil"
	"net/http"
	"net/url"
	"strings"
)

const (
	address  = "192.168.1.227"
	port     = 80
	username = "test"
	password = "abc123!#"
)

func check(err error) {
	if err != nil {
		panic(err)
	}
}

func main() {
	u, err := url.Parse(fmt.Sprintf("http://%s:%d/axis-cgi/param.cgi?action=list&group=Brand.Brand", address, port))
	check(err)

	a := probeAuthentication(u.String())

	nc := "00000001"
	cnonce := "0a4f113b"

	ha1 := hash(fmt.Sprintf("%s:%s:%s", username, a.realm, password))
	ha2 := hash(fmt.Sprintf("%s:%s", http.MethodGet, u.Path+"?"+u.RawQuery))
	response := hash(fmt.Sprintf("%s:%s:%s:%s:%s:%s", ha1, a.nonce, nc, cnonce, a.qop, ha2))

	c := http.Client{}
	req := http.Request{
		Method: http.MethodGet,
		URL:    u,
		Header: make(http.Header),
	}
	req.Header.Add(
		"Authorization",
		fmt.Sprintf("Digest username=\"%s\", realm=\"%s\", nonce=\"%s\", uri=\"%s\", qop=auth, nc=%s, cnonce=\"%s\", response=\"%s\"", username, a.realm, a.nonce, u.Path+"?"+u.RawQuery, nc, cnonce, response))
	res, err := c.Do(&req)
	check(err)
	fmt.Println(res.StatusCode)
	defer res.Body.Close()
	body, err := ioutil.ReadAll(res.Body)
	check(err)
	fmt.Println(string(body))
}

type authentication struct {
	realm     string
	nonce     string
	algorithm string
	qop       string
}

func probeAuthentication(u string) authentication {
	res, err := http.Get(u)
	check(err)

	h := res.Header.Get("WWW-Authenticate")
	if h == "" {
		panic(errors.New("response from device did not contain WWW-Authenticate header"))
	}

	a := authentication{}
	for _, section := range strings.Split(h, ",") {
		kv := strings.SplitN(section, "=", 2)
		k := strings.Trim(kv[0], " ")
		v := strings.Trim(kv[1], " \"")
		switch k {
		case "Digest realm":
			a.realm = v
		case "nonce":
			a.nonce = v
		case "algorithm":
			a.algorithm = v
		case "qop":
			a.qop = v
		}
	}

	if a.algorithm != "MD5" {
		panic(errors.New("algorithm != MD5"))
	}
	if a.qop != "auth" {
		panic(errors.New("qop != auth"))
	}

	return a
}

func hash(s string) string {
	h := md5.Sum([]byte(s))
	return fmt.Sprintf("%x", h)
}
