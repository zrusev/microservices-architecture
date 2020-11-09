#!/bin/bash
count=0
UUID=$(cat /proc/sys/kernel/random/uuid)

until [ "$started" = true ] || [[ ( "$count" == 3 ) ]]; do
  count=$((count+1))
  echo "[$STAGE_NAME] User Registration [Attempt: $count]"

  testStart=$(curl --write-out '%{http_code}' --silent --output /dev/null --location --request POST 'http://localhost:5001/api/v1/users/register' --header 'Content-Type: application/json' --data-raw "{ \"Email\": \"${UUID}@zrusev.me\", \"Password\": \"${UUID}\" }")

  if [[ ( "$testStart" == 200 ) ]]; then
    started=true
    echo "Registration Success"
    else
    sleep 1
  fi
done

if [[ "$started" ]]; then
  echo "Registration Failure"
  exit 1
fi