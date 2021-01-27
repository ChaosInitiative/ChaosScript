#!/bin/bash

if [[ "$(uname)" = "Linux" ]]; then  
    dotnet build --configuration Debug -o ../game/bin/managed/linux64
else
    dotnet build --configuration Debug -o ../game/bin/managed/win64
fi 