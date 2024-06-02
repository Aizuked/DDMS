
const connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .withAutomaticReconnect()
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
}

connection.on("ReceiveMessage", (msgDto) => {
    const currHref = window.location.href;

    if (currHref.includes("chat/" + msgDto.chatId + "/")) {
        const ul = $("ul.chat");

        ul.find("li:first").remove();

        const newMsg =
`
<li class="p-2" id="message_${msgDto.id}">
    <a href='${msgDto.userDetailsPath}'>
        <img src='${msgDto.userProfilePicturePath}' alt=''>
    </a>
    <p>${msgDto.content}</p>
`
+ !msgDto.localFilePhysicalPath ? '<a href={msgDto.localFilePhysicalPath}>${msgDto.localFileDisplayName}</a>' : '' +
`
</li>
`;
        ul.append(newMsg);
    }
});

connection.onclose(async () => {
    await start();
});

// Start the connection.
await start();