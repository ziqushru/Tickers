#!/bin/bash

mkdir /opt/certificates
ln -s /opt/certificates /root/certificates
ln -s /opt/certificates /home/sa/certificates
mkdir /opt/certificates/ssl
mkdir /opt/certificates/iText

mkdir /opt/applications
ln -s /opt/applications /root/applications
ln -s /opt/applications /home/sa/applications
mv diaulos /opt/applications/diaulos
