#!/bin/bash

while getopts h:u:e flag
do
	case "${flag}" in
		h) hostname=${OPTARG};;
		u) user=${OPTARG};;
		e) env=1;;
        esac
done

if [[ $user != "root" ]]
then
	absolute_user_path=/mnt/$hostname/home/$user
else
	absolute_user_path=/mnt/$hostname/$user
fi

if [[ $env = 1 ]]
then
	cp /opt/applications/diaulos/.env $absolute_user_path/applications/diaulos/
fi

cp /opt/applications/diaulos/Diaulos.WebApi/appsettings.* $absolute_user_path/applications/diaulos/Diaulos.WebApi/
