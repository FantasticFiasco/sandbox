
var networkInterfaces = require('./networkInterfaces');
var ssdp = require('./ssdp');

// Get all IPv4 addresses from all network interfaces
var addresses = networkInterfaces.getInterfaceAddresses();

// Start listening for SSDP messages
addresses.forEach(address => {
  ssdp.start(address, (message, remote) => {
    console.log(`Reply from ${remote.address}:${remote.port}`);
    console.log('Message:' + message);
  });
});

// Trigger a search
ssdp.search();