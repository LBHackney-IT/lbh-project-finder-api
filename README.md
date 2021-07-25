# LBH Project Finder API

The project finder API is the back-end API for the project finder front-end which allows Hackit employees to view projects going on within the council

## Stack

- .NET Core as a web framework.
- nUnit as a test framework.

## Contributing

### Setup

1. Install [Docker][docker-download].
2. Install [AWS CLI][AWS-CLI].
3. Clone this repository.
4. Open it in your IDE.


### Development

To serve the application, run it using your IDE of choice, we use Visual Studio CE and JetBrains Rider on Mac.

**Note**
When running locally the appropriate database conneciton details are still needed.
##### Postgres
For Postgres an approprate `CONNECTION_STRING` environment variable is needed,
and if you want to use a local Postgres instance then that will of course need to be installed and running.

The application can also be served locally using docker:
1.  Add you security credentials to AWS CLI.
```sh
$ aws configure
```
2. Log into AWS ECR.
```sh
$ aws ecr get-login --no-include-email
```
3. Build and serve the application. It will be available in the port 3000.
```sh
$ make build && make serve
```
## Static Code Analysis

### Using [FxCop Analysers](https://www.nuget.org/packages/Microsoft.CodeAnalysis.FxCopAnalyzers)

FxCop runs code analysis when the Solution is built.

Both the API and Test projects have been set up to **treat all warnings from the code analysis as errors** and therefore, fail the build.

However, we can select which errors to suppress by setting the severity of the responsible rule to none, e.g `dotnet_analyzer_diagnostic.<Category-or-RuleId>.severity = none`, within the `.editorconfig` file.
Documentation on how to do this can be found [here](https://docs.microsoft.com/en-us/visualstudio/code-quality/use-roslyn-analyzers?view=vs-2019).

## Testing

### Run the tests

```sh
$ make test
```

To run database tests locally (e.g. via Visual Studio) and you are using Postgres the `CONNECTION_STRING` environment variable will need to be populated with:

`Host=localhost;Database=testdb;Username=postgres;Password=mypassword"`

Note: The Host name needs to be the name of the stub database docker-compose service, in order to run tests via Docker.

If changes to the database schema are made then the docker image for the database will have to be removed and recreated. The restart-db make command will do this for you.

### Agreed Testing Approach
- Use nUnit, FluentAssertions and Moq
- Always follow a TDD approach
- Tests should be independent of each other
- Gateway tests should interact with a real test instance of the database
- Test coverage should never go down
- All use cases should be covered by E2E tests
- Optimise when test run speed starts to hinder development
- Unit tests and E2E tests should run in CI
- Test database schemas should match up with production database schema
- Have integration tests which test from the PostgreSQL database to API Gateway

## Contacts

### Active Maintainers

- **Miles Alford**, Apprentice Developer at London Borough of Hackney (miles.alford@hackney.gov.uk)


