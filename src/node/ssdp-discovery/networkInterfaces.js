var os = require('os');
var _ = require('lodash');

module.exports = {

	getInterfaceAddresses: () => {
		var interfaces = os.networkInterfaces();
		
		var addresses = _.chain(interfaces)
			.values()
			.flatten()
			.filter(entry => entry.family === 'IPv4')
			.filter(entry => !entry.internal)
			.map(entry => entry.address)
			.value();

		return addresses;
	}
};
