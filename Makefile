.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build project-finder-api

.PHONY: serve
serve:
	docker-compose build project-finder-api && docker-compose up -d dev-database
	-dotnet tool install -g dotnet-ef
	CONNECTION_STRING="Host=127.0.0.1;Port=5432;Username=postgres;Password=mypassword;Database=testdb" dotnet ef database update -p ProjectFinderApi
	docker-compose up project-finder-api

.PHONY: shell
shell:
	docker-compose run project-finder-api bash

.PHONY: test
test:
	docker-compose build project-finder-api-test && docker-compose up project-finder-api-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format

.PHONY: migrate-test-db
migrate-test-db:
	-dotnet tool install -g dotnet-ef
	CONNECTION_STRING="Host=127.0.0.1;Port=5432;Username=postgres;Password=mypassword;Database=testdb" dotnet ef database update -p ProjectFinderApi

.PHONY: restart-db
restart-db:
	docker stop $$(docker ps -q --filter ancestor=test-database -a)
	-docker rm $$(docker ps -q --filter ancestor=test-database -a)
	docker rmi test-database
	docker-compose up -d test-database
