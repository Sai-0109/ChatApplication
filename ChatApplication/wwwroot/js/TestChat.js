"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatApplication").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    //debugger;
    var user = document.getElementById("userInput").value;
    var li = document.createElement("li");
    var userNameSpan = document.createElement("span");
    document.getElementById("messagesList").appendChild(li);
    li.appendChild(userNameSpan);
    userNameSpan.style.color = "red";
    userNameSpan.textContent = `${user} says `;
    li.appendChild(document.createTextNode(message));
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var groupName = document.getElementById("groupName").value;
    connection.invoke("SendMessageToGroup", groupName, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("joinGroup").addEventListener("click", function (event) {
    var groupName = document.getElementById("groupName").value;
    connection.invoke("JoinGroup", groupName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});