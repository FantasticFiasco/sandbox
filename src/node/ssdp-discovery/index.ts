import {NetworkInterfaces} from './NetworkInterfaces';
import {Ssdp} from './Ssdp';

// Get all IPv4 addresses from all network interfaces
const networkInterfaces = new NetworkInterfaces();
const addresses = networkInterfaces.getAddresses();

// Start listening for SSDP messages
const ssdp = new Ssdp();
addresses.forEach(address => {
  ssdp.start(address, (message, remote) => {
     console.log(`Reply from ${remote.address}:${remote.port}`);
     console.log('Message:' + message);
   });
});

// Trigger a search
ssdp.search();