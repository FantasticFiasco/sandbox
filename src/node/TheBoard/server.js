var http = require("http");
var express = require("express");

var app = express();
var controllers = require("./controllers");

// Jade view engine
//app.set("view engine", "jade");

// EJS view engine
//var ejsEngine = require("ejs-locals");
//app.engine("ejs", ejsEngine);		// Support master pages
//app.set("view engine", "ejs");		// EJS view engine

// Vash view engine
app.set("view engine", "vash");

// Set the public static resource folder
app.use(express.static(__dirname + "/public"));

// Map the routes
controllers.init(app);

app.get("/api/users", function (req, res) {
    res.set("Content-Type", "application/json");
    res.send({ name: "FantasticFiasco", isValid: true, group: "Admin" });
});

var server = http.createServer(app);
server.listen(3000);