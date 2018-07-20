Prerequisites
=============

- ElasticSearch 5.x
- ICU Analysis ElasticSearch Plugin

Notes
=====

- The folder holding the bhl-us source code is referred to throughout this document as &lt;BHLRoot&gt;.

Setup
=====

After downloading the bhl-us source code, do the following to create and configure the ElasticSearch indexes.

Windows command prompt
-----------------

1) Open a Windows command prompt.

2) If curl is not already available on the computer, install it from https://curl.haxx.se/download.html.

3) Navigate to the &lt;BHLRoot&gt;\ElasticSearch folder.

4) Run the following commands to build the indexes.

	curl -X PUT SERVER/catalog -d @Catalog.json --header "Content-Type:application/json"

	curl -X PUT SERVER/items -d @Items.json --header "Content-Type:application/json"

	curl -X PUT SERVER/pages -d @Pages.json --header "Content-Type:application/json"
    
	curl -X PUT SERVER/authors -d @Authors.json --header "Content-Type:application/json"
    
	curl -X PUT SERVER/keywords -d @Keywords.json --header "Content-Type:application/json"
    
	curl -X PUT SERVER/names -d @Names.json --header "Content-Type:application/json"

	where 

	SERVER is the address of the search server, including the port number

	Example: 

    curl -X PUT http://localhost:9200/catalog -d @Catalog.json --header "Content-Type:application/json"

    curl -X PUT http://localhost:9200/items -d @Items.json --header "Content-Type:application/json"

	curl -X PUT http://localhost:9200/pages -d @Pages.json --header "Content-Type:application/json"
    
	curl -X PUT http://localhost:9200/authors -d @Authors.json --header "Content-Type:application/json"
    
	curl -X PUT http://localhost:9200/keywords -d @Keywords.json --header "Content-Type:application/json"
    
	curl -X PUT http://localhost:9200/names -d @Names.json --header "Content-Type:application/json"



Bash shell
-----------------

1) Open a command prompt.

2) Navigate to the &lt;BHLRoot&gt;\ElasticSearch folder.

3) Run the following commands to build the indexes.

	bash BuildESIndex.sh SERVER catalog Catalog.json

	bash BuildESIndex.sh SERVER items Items.json

	bash BuildESIndex.sh SERVER pages Pages.json
    
    bash BuildESIndex.sh SERVER authors Authors.json

	bash BuildESIndex.sh SERVER keywords Keywords.json

	bash BuildESIndex.sh SERVER names Names.json

	where 

	SERVER is the address of the search server, including the port number

	Example: 

	bash BuildESIndex.sh http://localhost:9200 catalog Catalog.json

	bash BuildESIndex.sh http://localhost:9200 items Items.json

	bash BuildESIndex.sh http://localhost:9200 pages Pages.json
    
    bash BuildESIndex.sh http://localhost:9200 authors Authors.json

	bash BuildESIndex.sh http://localhost:9200 keywords Keywords.json

	bash BuildESIndex.sh http://localhost:9200 names Names.json
