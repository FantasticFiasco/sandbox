import { Component, OnInit } from '@angular/core';
import { CustomerComponent } from './customer.component';

@Component({
	selector: 'app-customers',
	templateUrl: 'app/customer/customers.component.html',
	directives: [CustomerComponent]
})
export class CustomersComponent implements OnInit {
	customers = [
		{ id: 1, name: 'Ward' },
		{ id: 2, name: 'Kevin' },
		{ id: 3, name: 'Eric' },
		{ id: 4, name: 'Sally' },
		{ id: 5, name: 'Emmet' },
	];

	constructor() { }

	ngOnInit() { }
}
