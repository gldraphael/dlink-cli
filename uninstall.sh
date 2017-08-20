#!/bin/bash

if [[ "root" != $USER ]]; then
    echo You need to be root. Use sudo.
    exit 1
fi

sudo rm -rf /opt/dlink
rm /usr/local/bin/dlink
echo Done.