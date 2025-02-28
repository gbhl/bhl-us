REM Export the data
BHLExportProcessor TSV

REM Zip the data
7za.exe a -tzip "data\data.zip" "data\*.txt"

REM Copy the data to the web server
copy data\title.txt "\\SERVER\FOLDER\title.txt"
copy data\titleidentifier.txt "\\SERVER\FOLDER\titleidentifier.txt"
copy data\subject.txt "\\SERVER\FOLDER\subject.txt"
copy data\creator.txt "\\SERVER\FOLDER\creator.txt"
copy data\doi.txt "\\SERVER\FOLDER\doi.txt"
copy data\item.txt "\\SERVER\FOLDER\item.txt"
copy data\part.txt "\\SERVER\FOLDER\part.txt"
copy data\partcreator.txt "\\SERVER\FOLDER\partcreator.txt"
copy data\partidentifier.txt "\\SERVER\FOLDER\partidentifier.txt"
copy data\partpage.txt "\\SERVER\FOLDER\partpage.txt"
copy data\data.zip "\\SERVER\FOLDER\data.zip"

REM Delete the export files
del data\title.txt
del data\titleidentifier.txt
del data\subject.txt
del data\creator.txt
del data\doi.txt
del data\item.txt
del data\part.txt
del data\partcreator.txt
del data\partidentifier.txt
del data\partpage.txt
del data\page.txt
del data\pagename.txt
del data\data.zip

