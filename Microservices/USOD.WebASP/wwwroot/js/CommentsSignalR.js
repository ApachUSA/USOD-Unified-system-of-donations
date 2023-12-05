
const connection = new signalR.HubConnectionBuilder()
	.withUrl("http://localhost:5261/comment-hub")
	.configureLogging(signalR.LogLevel.Information)
	.build();

async function start() {
	try {
		await connection.start();
		console.log("SignalR Connected.");
	} catch (err) {
		console.log(err);
		setTimeout(start, 5000);
	}
};

connection.on("ReceiveComment", function (comment) {
	ReceiveComment(comment);
});

start();