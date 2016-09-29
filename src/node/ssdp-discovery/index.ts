import {NetworkInterfaces} from './NetworkInterfaces';
import {Ssdp} from './Ssdp';

// Get all IPv4 addresses from all network interfaces
const networkInterfaces = new NetworkInterfaces();
const addresses = networkInterfaces.getAddresses();

// Start listening for SSDP messages
const ssdp = new Ssdp();

ssdp.on('message', data => {
	console.log(`[Reply from ${data.remote.address}:${data.remote.port}]`);
	console.log('' + data.message);
	console.log('\r\n');
});

addresses.forEach(address => {
	ssdp.start(address);
});

// Trigger a search
ssdp.search();