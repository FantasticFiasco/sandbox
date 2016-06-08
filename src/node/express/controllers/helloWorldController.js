(function (helloWorldController) {
	
	helloWorldController.init = function(app)  {
		app.get('/api/hello-world/:greeting', function(req, res) {
			var greeting = req.params.greeting;
			
			res.set('Content-Type', 'application/json');
			res.send({greeting: greeting});
		});
		
		app.post('/api/hello-world/:greeting', function(req, res) {
			var item = {
				greeting: req.params.greeting,
				sender: req.body.sender
			};
			
			res.set('Content-Type', 'application/json');
			res.send(item);
		});
	};
	
})(module.exports);