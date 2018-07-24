# VnotifieR
Notification server and overlay for OpenVR

## Usage
You will need an application that will communicate with the server.
To send notifications to the server just send a `JSON` formatted `POST` request to `http://localhost:45689/notification` (with default settings).

## Configuration
You can change settings after running the server for the first time and then editing the `server.ini`.

### [Server]
`binds`: Comma seperated values of names and addresses the http server should be bound to

`port`: The port on which the http server should run on

### [Panel]
`color`: The font color

`shadow`: The shadow color

`opacity`: The opacity while dashboard is closed

`dashboard_opacity`: The opacity while dashboard is open

`font_size`: The font size

`format`: The string format for the notification output. (Note: This will most likely change to named parameters instead of indexed ones.)

## Credits
This projects uses
* OVRlay package by Ben Otter (https://github.com/benotter/OVRLay)
* INI file parser by L4rry (https://gist.github.com/Larry57/5725301)
* JSON Object by Defective Studios (https://assetstore.unity.com/packages/tools/input-management/json-object-710)
