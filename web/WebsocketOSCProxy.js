var osc = require('node-osc'),
    WebSocket = require('ws');
	
	
oscClient = new osc.Client("127.0.0.1", 7781);
ws = new WebSocket('ws://websocket.com:1234');
	
	
	
ws.on('open', function() {
    
});

ws.on('message', function(data, flags) {
	
	if (oscClient != null) {
		oscClient.send('/test', data);
	}
});