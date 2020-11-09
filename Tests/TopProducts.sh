#!/bin/bash
count=0
started=false

until [ "$started" = true ] || [[ ( "$count" == 3 ) ]]; do
  count=$((count+1))
  echo "[$STAGE_NAME] Loading TopProducts [Attempt: $count]"

  testStart=$(curl --write-out '%{http_code}' --silent --output /dev/null --location --request GET 'http://localhost:5007/api/v1/products/top')

  if [[ ( "$testStart" == 200 ) ]]; then
    started=true
    echo "TopProducts Success"
    else
    sleep 1
  fi
done

if [ "$started" = false ]; then
  echo "TopProducts Failure"
  exit 1
fi