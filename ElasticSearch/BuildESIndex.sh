#!/bin/bash

sServer=$1
sIndex=$2
sDocType=$3
sSettings=$4
sMappings=$5

if [ $# -eq 5 ]
then
  echo
  echo Building index $sServer/$sIndex with settings in $sSettings
  curl -X PUT $sServer/$sIndex -d @$sSettings --header "Content-Type:application/json"
  echo
  echo
  echo Adding mappings for $sServer/$sIndex/_mapping/$sDocType with mappings in $sMappings
  curl -X POST $sServer/$sIndex/_mapping/$sDocType -d @$sMappings --header "Content-Type:application/json"
  echo
  echo Done
else
  echo
  echo Usage
  echo
  echo BuildESIndex SERVER-NAME INDEX-NAME DOC-TYPE SETTINGS-FILENAME MAPPINGS-FILENAME
  echo
  echo SERVER-NAME is the name of the search server, including the port number
  echo INDEX-NAME is the name of the index to be created
  echo DOC-TYPE is the type of documents to be added to the index
  echo SETTINGS-FILENAME is the name of a file containing index settings
  echo MAPPINGS-FILENAME is the name of a file containing index mappings
  echo
  echo Example: BuildESIndex http://localhost:9200 items item ItemsSettings.json ItemsMappings.json
  echo
fi
echo
