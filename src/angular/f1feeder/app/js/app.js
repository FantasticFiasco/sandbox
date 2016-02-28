angular.module('F1FeederApp', [
    'F1FeederApp.controllers',
    'F1FeederApp.services',
    'ngRoute'
    ])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when(
                "/drivers",
                {
                    templateUrl: "js/partials/drivers.html",
                    controller: "driversController"
                })
            .when(
                "/drivers/:id",
                {
                    templateUrl: "js/partials/driver.html", controller: "driverController"
                })
            .otherwise(
                {
                    redirectTo: '/drivers'
                });
    }]);