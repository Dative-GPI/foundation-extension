# Helpers

## Foundation.Extension.Proxy

This project is used to create a small proxy that will help you develop your extension without the need for DAT'Foundation to have access to your local development server.
More over it enables you to develop your extensions without adding your extension to DAT'Foundation.

In order to work smoothly, some hacks are done by this proxy :

### Endpoints aggregations

In order to have your extensions routes added to the layout, the proxy module will request both your local server and DAT'Foundation and merge the result before passing it to the client (front-end)

It will works for routes & actions.

### Extension installation

Something similar to `endpoints aggregations`is done for extension application. We add to the DAT'Foundation results a custom extension named **Local extension** with a id = `null` and when you press the install button to add this extension to your DAT'Foundation environment, we intercept the request and if the extension you try to install is your local extension, we create a JWT specific to your user and send it to your local server. **Warning**, your extension will have the same exact rights as the connected user. On production environment, your extension may have more or less rights than the connected user.

