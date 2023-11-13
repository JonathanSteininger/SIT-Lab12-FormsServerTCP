# lab12-FormsServerTCP

This is a Windows Forms GUI client application that will connect to a localhost server and send it a json object requesting for a card. The server will then send back a card to the client and have it display and image of it on the application.

The client app will make a request object, serialize it into json and send it to the server.
The server will then send back a random card object in json where the client will deserialize it and display the matching card to the user.
