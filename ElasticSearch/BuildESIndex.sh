#!/bin/bash

sServer=$1
sIndex=$2
sSettings=$3

if [ $# -eq 3 ]
then
  echo
  echo Building index $sServer/$sIndex with settings in $sSettings
  curl -X PUT $sServer/$sIndex -d @$sSettings --header "Content-Type:application/json"
  echo
  echo Done
else
  echo
  echo Usage
  echo
  echo BuildESIndex SERVER-NAME INDEX-NAME SETTINGS-FILENAME
  echo
  echo SERVER-NAME is the name of the search server, including the port number
  echo INDEX-NAME is the name of the index to be created
  echo SETTINGS-FILENAME is the name of a file containing index settings and mappings
  echo
  echo Example: BuildESIndex http://localhost:9200 items Items.json
  echo
fi
echo
