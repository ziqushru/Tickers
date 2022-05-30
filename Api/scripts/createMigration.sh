#!/bin/bash

# get arguments
while getopts ":n:" arg; do
	case $arg in
        	n) name=$OPTARG;;
    	esac
done

# create migration with the specified name
dotnet ef migrations add -s AspDotNet/AspDotNet.csproj "$name"
