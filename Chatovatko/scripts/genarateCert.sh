#!/bin/bash
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout private.key -out public.crt
openssl pkcs12 -export -in public.crt -inkey private.key -out private.p12
