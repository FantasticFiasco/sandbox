"use strict";
var protractor_1 = require('protractor');
describe('protractor with typescript typings', function () {
    beforeEach(function () {
        protractor_1.browser.get('http://www.angularjs.org');
    });
    it('should greet the named user', function () {
        protractor_1.element(protractor_1.by.model('yourName')).sendKeys('Julie');
        var greeting = protractor_1.element(protractor_1.by.binding('yourName'));
        expect(greeting.getText()).toEqual('Hello Julie!');
    });
    it('should list todos', function () {
        var todoList = protractor_1.element.all(protractor_1.by.repeater('todo in todoList.todos'));
        expect(todoList.count()).toEqual(2);
        expect(todoList.get(1).getText()).toEqual('build an angular app');
    });
});
