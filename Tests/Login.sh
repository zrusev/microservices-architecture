#!/bin/bash
count=0
started=false

until [ "$started" = true ] || [[ ( "$count" == 3 ) ]]; do
  count=$((count+1))
  echo "[$STAGE_NAME] User Login [Attempt: $count]"

  testStart=$(curl --write-out '%{http_code}' --silent --output /dev/null --location --request POST 'http://localhost:5001/api/v1/Users/Login' --header 'Content-Type: application/json' --data-raw '{ "Email": "admin@admin.bg", "Password": "admin13" }')

  if [[ ( "$testStart" == 200 ) ]]; then
    started=true
    echo "Login Success"
    else
    sleep 1
  fi
done

if [ "$started" = false ]; then
  echo "Login Failure"
  exit 1
fi