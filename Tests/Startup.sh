#!/bin/bash
count=0

until [ "$started" = true ] || [[ ( "$count" == 3 ) ]]; do
  count=$((count+1))
  echo "[$STAGE_NAME] Starting Application [Attempt: $count]"

  testStart=$(curl --write-out '%{http_code}' --silent --output /dev/null http://localhost:3000)

  if [[ ( "$testStart" == 200 ) ]]; then
    started=true
    echo "Startup Success"
    else
    sleep 1
  fi
done

if [ "$started" = false ]; then
  echo "Startup Failure"
  exit 1
fi