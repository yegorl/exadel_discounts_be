#!/usr/bin/env bash

openssl x509 -inform DER -in /https-root/aspnetapp-root-cert.cer -out /https-root/aspnetapp-root-cert.crt
cp /https-root/aspnetapp-root-cert.crt /usr/local/share/ca-certificates/
update-ca-certificates