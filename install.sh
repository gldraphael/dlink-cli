#!/bin/bash

if [[ "root" != $USER ]]; then
    echo You need to be root. Use sudo.
    exit 1
fi

CLISYM=/usr/local/bin/dlink

# Restore dependencies
dotnet restore

cd src
# Create a publish package
mkdir /tmp/dlink
dotnet publish -o /tmp/dlink

# Clear existing files
sudo rm -rf /opt/dlink

# Install the tool
sudo cp -r /tmp/dlink /opt

cat > $CLISYM <<- EOM
#!/bin/bash
dotnet /opt/dlink/cli.dll "\$@"
EOM
chmod +x $CLISYM

# Clean up
rm -rf /tmp/dlink
cd ..
