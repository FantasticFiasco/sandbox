---
title: reveal.js
---

# reveal.js

The HTML presentation framework

created by [FantasticFiasco][1] | 2017-02-14 | [src][2]

[1]: https://github.com/FantasticFiasco
[2]: https://github.com/FantasticFiasco/sandbox/tree/master/src/reveal.js

Note:
This is the [speakers note](https://github.com/hakimel/reveal.js#speaker-notes). It won't show on the slides, but can be viewed by the speaker by pressing the key 's'.

----

## TL;DR

reveal.js comes with a broad range of features including [nested slides](https://github.com/hakimel/reveal.js#markup), [Markdown contents](https://github.com/hakimel/reveal.js#markdown), [PDF export](https://github.com/hakimel/reveal.js#pdf-export), [speaker notes](https://github.com/hakimel/reveal.js#speaker-notes) and a [JavaScript API](https://github.com/hakimel/reveal.js#api).

----

But then, who wants to write HTML?

---

# Enter reveal-md

reveal.js on steroids! Get beautiful reveal.js presentations from your Markdown files.

----

## Installation

`> npm install -g reveal-md`

---

# Usuage

----

## Start single file

`> reveal-md slides.md`

----

## Start file on web

`> reveal-md https://raw.github.com/webpro/reveal-md/master/demo/a.md`

----

## Start files in directory

`> reveal-md dir/`

----

## Start files in current directory

`> reveal-md`

----

## Start in watch mode

`reveal-md -w slides.md`

---

# Examples

----

## Bullet lists

* <!-- .element: class="fragment" --> Where I make the first point
* <!-- .element: class="fragment" --> Where I make the second point
* <!-- .element: class="fragment" --> Where I make the third point

----

## Code

```javascript
// Change heading:
document.getElementById("myH").innerHTML = "My First Page";

// Change paragraph:
document.getElementById("myP").innerHTML = "My first paragraph.";
```

----

## File structure

```sh
~/repos/my_presi> tree
.
├── .git/
├── .gitignore
├── README.md   <-- Short description and usage
├── slides.md   <-- All slides are defined here
└── images/
    └── thanks.jpg  <-- good place for images
```

---

# The End.

![](images/potato.jpg)