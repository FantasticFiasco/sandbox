# Web server tests

This project is investigating the different methodologies one could test a web server.

A very basic implementation of a web server is found in `src`, while the test methodologies are found in `test`. Currently the following methodologies are investigated:

- `integration` - Integration tests by instantiating the actual web server and run requests at it
- `unit` - Unit tests by mocking controller dependencies and only test the actual controllers

I've tried a new test naming convention inspired by the article [Unit Test Naming Convention](https://ardalis.com/unit-test-naming-convention).
