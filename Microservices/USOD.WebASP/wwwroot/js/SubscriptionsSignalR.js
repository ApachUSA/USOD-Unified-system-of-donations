const connectionToFund = new signalR.HubConnectionBuilder()
	.withUrl("http://localhost:7051/subscription-hub")
	.configureLogging(signalR.LogLevel.Information)
	.build();

async function start() {
	try {
		await connectionToFund.start();
		console.log("SignalR Connected.");
	} catch (err) {
		console.log(err);
		setTimeout(start, 5000);
	}
};

connectionToFund.on("ReceiveMessage", function (message) {
	ToastShow(message);
});

start();