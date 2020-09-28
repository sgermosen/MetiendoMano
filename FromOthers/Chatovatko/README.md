![Chatovatko](https://github.com/stastnypremysl/Chatovatko/blob/master/Premy.Chatovatko/Premy.Chatovatko.Client/Premy.Chatovatko.Client/Images/logo1.png)

A chatting C# application with end-to-end encryption. The main purpuse of application is to deliver solution for secure chatting, where you can be sure your messages will remain private even if server will be misused.
The application is now under development. The works on front end hasn't started yet.

Chatovatko is currently tested under Ubuntu 18.04 and Windows 10.

## Function overview
* End-to-end encryption
* Backuping encrypted conversations to server
* On live chatting (chat, where messages are not sended, but chat textbox is cloned to recepient)
* Multiplatform client app (UWP/Windows, Android)
* Multiplatform server app (Windows, Linux)

## Releases
The last release is [**0.3.1**](https://github.com/stastnypremysl/Chatovatko/releases/tag/0.3.1). Android app can be downloaded from [Google Play](https://play.google.com/store/apps/details?id=cos.premy.chatovatko.client). UWP client app isn't available on Microsoft Store and must be [sideloaded](https://github.com/stastnypremysl/Chatovatko/releases/download/0.3.1/Premy.Chatovatko.Client.UWP_0.3.1.0_x86_x64_arm.appxbundle).

## Important technical details
* **.NET Core 2.1** used for server-side service and for command-based console application (for testing purpuse)
* **.NET Standart 2.0** used for multiplatform client libraries and server-client shared libraries
* **Tls 1.2** is used for encrypting communication between server and client and for server verification
* **RSA** with key size **4096** is used for client verification and sending AES keys
* **AES256** used for encrypting messages
* **MySql** is required as server-side database.
* Sqlite3 is used as client side database
* The project is developed using **Visual Studio 2017** and **MySql Workbench 8.0**
* Also, the project also benefits from Entity Framework Core, Newtonsoft.Json and Pomelo.EntityFrameworkCore.MySql
* SHA256 and SHA1 used for hashing
* Only little endian is supported

## Getting started
There is no offical release currently, but you can download git repository and compile.
To clone repository, enter to your console:
    
    git clone https://github.com/stastnypremysl/Chatovatko/
    
For deep technical details, please, visit wiki.

### Installation of server
Install `MySql 14` and run script [`Chatovatko/sql/serverBuildScripts/build.sql`](https://github.com/stastnypremysl/Chatovatko/blob/master/sql/serverBuildScripts/build.sql). It is necessary to have `.NET Core 2.1` installed. This [manual](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial#install) seems to be useful.

You will need **p12 certificate**. You can run this script to generate it: [`Chatovatko/scripts/genarateCert.sh`](https://github.com/stastnypremysl/Chatovatko/blob/master/scripts/genarateCert.sh).

Then make a config file. Here is an example for inspiration:

    ConnectionString=Server=localhost; database=chatovatko;UID=MyUserName;password=SuperSecretPassword
    CertAddress=/mnt/c/keys/private.p12
    ServerName=Unforgattable server name

When everything is ready, run server with this command

    cd /pathToRepository/Chatovatko/Premy.Chatovatko/Premy.Chatovatko.Server
    dotnet run -c Release -- /otherPath/configFile.txt

Server uses **TCP** in ports **8470-8472**.
    
### Console client
#### Convenstion
`<user>` means `<user_id>` or `<user_name>`. `<thread>` means `<private_thread_id>` or **unique** `<thread_name>`, which doesn't contain any space and isn't number.
#### Installation
As it was in server installation, install [`.NET Core 2.1`](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial#install) haven't done it already. There are no more prerequsities. Just run:

    cd /pathToRepository/Chatovatko/Premy.Chatovatko/Premy.Chatovatko.Client.Console
    dotnet run -c Release

#### First run
There are two inicialization commands:

    init new <server_address>

This one will generate new p12 certificate and you will be asked to enter path to save it. 
**It is necessary to keep it SAFE!** After that you will be asked to enter your new unique username.
Username must be 4 chars at least long and can't be longer than 45 chars. It must match regex ^[a-zA-Z][-a-zA-Z0-9_]+$.

    init login <server_address>
    
If you have your p12 certificate, use this one. If the certificate hasn't been paired with this server already, the entered username will be used for your registration. Otherwise the username will be ignored.

#### Reset/Relogin
If anything goes wrong, you can always relogin. Run

    delete database

and continue as first time.

#### Casual commands
To exit application, just enter

    exit
or

    quit
If you want to add comments to your script, please respect this convention

    # <comment>
    -- <comment>

#### Online only commands
To open new connection to server, use

    connect
 
If a connection crashed, or you just wanted to disconnect, run
 
    disconnect
     
##### Pulling and pushing
Almost all changes are kept in local database. It is necessary to push them on server to finalize them. Analogily the same, you need pull if you want to download all changes, which are on server already. Two magical selfdescribing keywords:

    push
    pull
    
##### Searching and saving contacts
You must find, verify and save a contact to your chain (user's private database),
if you want to do any further operation with the user. To do so, please enter to your console 

    search <user>

##### Trutification
Before you can send messages to an user, you must trustify him. To do so, enter

    trust <user>

This will send server information, that you trust this user and it will generate confirmatory message to user's chain. If you've done this first time, it will also create new AES key and send it encryped to server. The key is for encrypting your messages for the user and for his ability of reading it.

The user must have trustifed you to receive your messages.

If you change your mind and want to untrust some user, just enter to console

    untrust <user>
    
Please, remember the already loaded messages from untrusted users will not be loaded again after database deletion.

#### Offline commands
These commands will be always executed offline and it is necessary to pull/push to keep everything up-to-date.

##### Lists
To list all saved users, please enter

    ls users
     
To list all of your threads, please enter

    ls threads
   
Each thread has its own private id (`id`) and its public id (`public_id`). Private id is unique only on the client, but public id is unique globally.

Thread writeout can be invoced by

    ls messages <thread>
    
##### Posts
A new thread can be created by

    post thread <user> <name>

Please, keep in mind, a name can't contain double space.

To send new message, please enter

    post message <thread> <eof>
    ......................
    ....message content...
    ......................
    <eof>
    
Remember that `<eof>` cannot contain any space.

##### Rename
A thread can be renamed by

    rename <thread> <name>
    
The rule for `<name>` are same as you were creating new thread.

##### Nickname
User's nick name can be set using command

    nickname <user> <nick_name>

A nick name can't contain double space.

##### Delete
To delete message thread, please enter

    delete thread <thread>
     
Messages can be deleted by

    delete message <message_id> 

##### Hash
If you want to compute SHA-256 hash of your certificate, use

    hash
