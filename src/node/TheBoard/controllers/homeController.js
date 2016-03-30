(function(homeController) {

    var data = require("../data");

    homeController.init = function(app) {

        app.get("/", function (req, res) {
            data.getNoteCategories(function(err, results) {
                
                // Raw data
                //res.send("<html><body><h1>Express</h1></body></html>");

                // Jade view engine
                //res.render("jade/index", { title: "Express + Jade" });

                // EJS view engine
                //res.render("ejs/index", { title: "Express + EJS" });

                // Vash view engine
                res.render(
                    "index",
                    {
                        title: "The Board",
                        error: err,
                        categories: results
                    });
            });
        });

    };

})(module.exports);