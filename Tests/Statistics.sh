#!/bin/bash
count=0
started=false

until [ "$started" = true ] || [[ ( "$count" == 3 ) ]]; do
  count=$((count+1))
  echo "[$STAGE_NAME] Get SeenProduct [Attempt: $count]"

  testStart=$(curl --write-out '%{http_code}' --silent --output /dev/null --location --request GET 'http://localhost:5005/api/v1/SeenProducts/1')

  if [[ ( "$testStart" == 200 ) ]]; then
    started=true
    echo "SeenProduct Success"
    else
    sleep 1
  fi
done

if [ "$started" = false ]; then
  echo "SeenProduct Failure"
  exit 1
fi