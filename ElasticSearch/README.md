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

	curl -X PUT SERVER/items -d @ItemsSettings.json --header "Content-Type:application/json"

	curl -X PUT SERVER/pages -d @PagesSettings.json --header "Content-Type:application/json"
    
	curl -X PUT SERVER/authors -d @AuthorsSettings.json --header "Content-Type:application/json"
    
	curl -X PUT SERVER/keywords -d @KeywordsSettings.json --header "Content-Type:application/json"
    
	curl -X PUT SERVER/names -d @NamesSettings.json --header "Content-Type:application/json"

	where 

	SERVER is the address of the search server, including the port number

	Example: 

    curl -X PUT http://localhost:9200/items -d @ItemsSettings.json --header "Content-Type:application/json"

	curl -X PUT http://localhost:9200/pages -d @PagesSettings.json --header "Content-Type:application/json"
    
	curl -X PUT http://localhost:9200/authors -d @AuthorsSettings.json --header "Content-Type:application/json"
    
	curl -X PUT http://localhost:9200/keywords -d @KeywordsSettings.json --header "Content-Type:application/json"
    
	curl -X PUT http://localhost:9200/names -d @NamesSettings.json --header "Content-Type:application/json"

5) Run the following commands to add the mappings to the indexes.

	curl -X POST SERVER/items/\_mapping/item -d @ItemsMappings.json --header "Content-Type:application/json"

	curl -X POST SERVER/pages/\_mapping/page -d @PagesMappings.json --header "Content-Type:application/json"
    
	curl -X POST SERVER/authors/\_mapping/author -d @AuthorsMappings.json --header "Content-Type:application/json"
    
	curl -X POST SERVER/keywords/\_mapping/keyword -d @KeywordsMappings.json --header "Content-Type:application/json"
    
	curl -X POST SERVER/names/\_mapping/name -d @NamesMappings.json --header "Content-Type:application/json"

	where 

	SERVER is the address of the search server, including the port number

	Example: 

	curl -X POST http://localhost:9200/items/\_mapping/item -d @ItemsMappings.json --header "Content-Type:application/json"

	curl -X POST http://localhost:9200/pages/\_mapping/page -d @PagesMappings.json --header "Content-Type:application/json"
    
	curl -X POST http://localhost:9200/authors/\_mapping/author -d @AuthorsMappings.json --header "Content-Type:application/json"
    
	curl -X POST http://localhost:9200/keywords/\_mapping/keyword -d @KeywordsMappings.json --header "Content-Type:application/json"
    
	curl -X POST http://localhost:9200/names/\_mapping/name -d @NamesMappings.json --header "Content-Type:application/json"


Bash shell
-----------------

1) Open a command prompt.

2) Navigate to the &lt;BHLRoot&gt;\ElasticSearch folder.

3) Run the following commands to build the indexes.

	bash BuildESIndex.sh SERVER items item ItemsSettings.json ItemsMappings.json

	bash BuildESIndex.sh SERVER pages page ItemsSettings.json ItemsMappings.json
    
    bash BuildESIndex.sh SERVER authors author ItemsSettings.json ItemsMappings.json

	bash BuildESIndex.sh SERVER keywords keyword ItemsSettings.json ItemsMappings.json

	bash BuildESIndex.sh SERVER names name ItemsSettings.json ItemsMappings.json

	where 

	SERVER is the address of the search server, including the port number

	Example: 

	bash BuildESIndex.sh http://localhost:9200 items item ItemsSettings.json ItemsMappings.json

	bash BuildESIndex.sh http://localhost:9200 pages page PagesSettings.json PagesMappings.json
    
    bash BuildESIndex.sh http://localhost:9200 authors author AuthorsSettings.json AuthorsMappings.json

	bash BuildESIndex.sh http://localhost:9200 keywords keyword KeywordsSettings.json KeywordsMappings.json

	bash BuildESIndex.sh http://localhost:9200 names name NamesSettings.json NamesMappings.json
