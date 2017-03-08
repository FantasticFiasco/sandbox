"use strict";
var protractor_1 = require('protractor');
var AngularHomepage = (function () {
    function AngularHomepage() {
        this.nameInput = protractor_1.element(protractor_1.by.model('yourName'));
        this.greeting = protractor_1.element(protractor_1.by.binding('yourName'));
    }
    AngularHomepage.prototype.get = function () {
        protractor_1.browser.get('http://www.angularjs.org');
    };
    AngularHomepage.prototype.setName = function (name) {
        this.nameInput.sendKeys(name);
    };
    // getGreeting returns a webdriver.promise.Promise.<string>. For simplicity
    // setting the return value to any
    AngularHomepage.prototype.getGreeting = function () {
        return this.greeting.getText();
    };
    return AngularHomepage;
}());
exports.AngularHomepage = AngularHomepage;
