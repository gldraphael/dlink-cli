# Unofficial Dlink Tool

Use the tool as:
```bash
$ dlink <status|release|renew|refresh|ip>
```

## Installation (for *nix systems)

**Dependencies:** dotnet SDK 2.0

> **WARNING:** Before installation, make sure you don't have a program called `dlink` already installed. Also ensure that `/opt/dlink` doesn't exist. If it does, either modify the install script to change the paths or use the tool without installing.

```bash
# Clone the repository
$ git clone git@github.com:gldraphael/dlink-cli.git --depth 1
$ cd fd-notification-cli
# Run the install script as root
$ sudo ./install
```

![Installation](dist/installation.png)

You may uninstall the cli by running `sudo ./uninstall` instead.

## Running without installation

It is recommended to install the program and then use it. However if you're not on *nix systems or just don't want to install it you may run the `run.sh` script instead.

You'll need to restore and build the program only once after every time you update the source using:

```
$ dotnet restore
$ dotnet build
```

## Updating installation

To update the installed CLI to the latest version, pull the latest source and run the `./install.sh` script again.
