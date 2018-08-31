# VnotifieR
Notification server and overlay for OpenVR

## Usage
You will need an application that will communicate with the server.
To send notifications to the server just send a `JSON` formatted `POST` request to `http://localhost:45689/notification` (with default settings).

## Configuration
You can change settings after running the server for the first time and then editing the `server.ini`.

### `[Server]`
`Binds`: Comma seperated values of names and addresses the http server should be bound to (example: `{localhost,127.0.0.1}`)

`Port`: The port on which the http server should run on

### `[Panel]`
`Color`: The font color (has to start with `#` if hex; HTML color names are supported but converted)

`Shadow`: The shadow color (has to start with `#` if hex; HTML color names are supported but converted)

`Opacity`: The opacity while dashboard is closed and after fade out

`NotificationOpacity`: The opacity when receiving a new notification

`DashboardOpacity`: The opacity while dashboard is open

`FadeTime`: The time in seconds it takes to fade to `Opacity` after receiving a new notification

`FontSize`: The font size

`Format`: The string format for the notification output. (Note: This will most likely change to named parameters instead of indexed ones.)

### `[Overlay]`
`Position`: The relative position of the panel to the HMD

`Rotation`: The relative rotation (in euler angles) of the panel to the HMD

## Credits
This projects uses
* OVRlay package by Ben Otter (https://github.com/benotter/OVRLay)
* MadMilkman.Ini by Mario Zorica (https://github.com/MarioZ/MadMilkman.Ini)
* JSON Object by Defective Studios (https://assetstore.unity.com/packages/tools/input-management/json-object-710)
