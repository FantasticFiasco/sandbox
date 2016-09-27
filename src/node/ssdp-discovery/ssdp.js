var _ = require('lodash');
var dgram = require('dgram');

const searchMessage = 
	'M-SEARCH * HTTP/1.1\r\n' +
	'Host:239.255.255.250:1900\r\n' +
	'ST:urn:axis-com:service:BasicService:1\r\n' +
	'Man:"ssdp:discover"\r\n' +
	'MX:3\r\n' +
	'\r\n';

var sockets = [];

module.exports = {

	start: (address, callback) => {
		const socket = dgram.createSocket({ type: 'udp4', reuseAddr: true });
		sockets.push(socket);

		socket.on('listening', () => {
      		console.log(`SSDP now listening on ${socket.address().address}:${socket.address().port}`);
	    });

		socket.on('message', function (message, remote) {   
        	if (callback != null) {
				callback(message, remote);
			}
    	});

		socket.on('error', error => {
      		console.log('SSDP socket error', error);
      		socket.close();
			_(sockets).pull(socket);
    	});

		socket.bind({ address: address });
	},

	search: () => {
		_.forEach(sockets, socket => {
			const message = new Buffer(searchMessage);
			socket.send(message, 0, message.length, 1900, '239.255.255.250');
	    	console.log('Search message sent');
		});
	}
}
