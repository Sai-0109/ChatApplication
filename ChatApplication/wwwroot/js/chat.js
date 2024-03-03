

    var connection = new signalR.HubConnectionBuilder().withUrl("/ChatApplication").build();


    document.getElementById("sendButton").disabled = true;

    connection.on("ReceiveMessage", function (message) {
        var user = $("#userDropdown").val();
    var li = document.createElement("li");
    var userNameSpan = document.createElement("span");
    document.getElementById("messagesList").appendChild(li);
    li.appendChild(userNameSpan);
    li.appendChild(userNameSpan);
    userNameSpan.style.color = "red";
    userNameSpan.textContent = `${user} says `;
    li.appendChild(document.createTextNode(message));
    });

    connection.start().then(function () {
        console.log("Connection established.");
    document.getElementById("sendButton").disabled = false;

    var groupName = document.getElementById("names").value;
    connection.invoke("JoinGroup", groupName).catch(function (err) {
            return console.error(err.toString());
        });
    console.log(groupName + " Joined Group");

    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("join").addEventListener("click", function (event) {
        var groupName = $("#userDropdown").val();
    connection.invoke("JoinGroup", groupName).catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
    });

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var user = $("#userDropdown").val();
    console.log("USER", user);
    var message = document.getElementById("messageInput").value;
    var groupName = $("#userDropdown").val();

    if (connection.state === signalR.HubConnectionState.Connected) {
        connection.invoke("SendMessageToGroup", groupName, message).catch(function (err) {
            return console.error(err.toString());
        });
        }
    event.preventDefault();
    console.log("After send message");
    });
