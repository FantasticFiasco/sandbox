/// <reference path="typings/index.d.ts" />
declare var ko: KnockoutStatic;

module demo {
	var name = ko.observable('John');
	var id = ko.observable(1);

	var guy = {
		id: id,
		fullName: name
	};

	var value: string = guy.fullName();
	console.log(value);
}