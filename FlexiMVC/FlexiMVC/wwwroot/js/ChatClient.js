var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connectionUserCount.on("ReceiveMessage", (user, message) => {

    var sender = document.getElementById("senderMessage")
    sender.innerText = user.toString();

    var msg = document.getElementById("Message")
    msg.innerText = message.toString();


})







function newWindowLoadedOnClient() {
    connectionUserCount.send("SendMessageMainFunction");
}


//start connection
function fulfilled() {
    console.log("Connection to user hub is successful")
    newWindowLoadedOnClient()
}

function rejected() {
    console.log("Connection to user hub rejected")
}


connectionUserCount.start().then(fulfilled, rejected)



