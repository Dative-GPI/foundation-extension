# Template extension

Welcome to XXXXX, a simple projet which shows you how to build an extension for DAT Foundation !

It contains the basic functionnalities required for an extension. This includes:

- The project's architecture
- Endpoints to install and uninstall the extension
- Permissions
- Translations
- Common routes, repositories, models, ...

## Initializing your extension
First install the template locally : 
```
dotnet new install Foundation.Extension.Template::VERSION
```
with VERSION the last Foundation.Extension.Template tag
The create de project with template and custom name using : 
```
dotnet new dativegpiextension -o PROJECT_NAME
```

### Old way (depricated) :

To initialize your extension before coding, launch the following command : `bash rename.sh <YOUR_PROJECT_NAME>`.

This will rename files and folders containing XXXXX placeholders, as well as replace those placeholders in files.  
They are usually found in C# files in the namespaces.

Then, in the `/dev/docker-compose.yml` file, replace occurences of *extension* with the name of your project (except maybe in the network name).

If you are working with a local version of DAT'Foundation, create fixtures, a migration, an extension in the Foundation database and modify Foundation's `docker-compose.yml` file to add your network as well as extra_hosts for *shell* and *admin*.

You should also modify the value of COMPOSE_PROJECT_NAME in the *.env* file and the routes in the controllers and frontends' urls.  
There are example routes and components that have been set up. These are here so you can use them as a template for your own routes and components.  
They are not meant to stay, so you can delete them, they are not mandatory.

## Testing the extension

In order to do a first test on the extension, you can try to fetch the extension's routes.

To do so, launch the following command:

```sh
curl -H "X-Application-Id: 3b6a8439-c6bf-46f3-bd1e-54b89fe9c432" -H "X-User-Id: 3b6a8439-c6bf-46f3-bd1e-54b89fe9c432" localhost/api/admin/routes
```
