# Asset Price Change

AssetChange is an information system that allows you to obtain data from the Yahoo Financial system, extract and store this data in a database and generate a report on asset price changes.
This project was inspired by [Guidti Challenge](https://github.com/guideti/variacao-ativo).

## Getting Started: How to use

After proper deployment on an application server with IIS (Internet Information Services), ask your administrator for the url made available for the application.

The database environment can be built from the project itself, for this it is enough to open the project with Visual Studio 2022, locate the `appsettings.json` file, change the connection strings and later, open the Package Manager Console terminal, select the project `4-Infra\AssetChange.Infra.Data` and run the Entity Framework Core command: `update-database`, so that the table structure is built.

Note: This REST API can be consumed by applications with free access to the AssetChange url.

### Steps for good use:

A. Execute asset data import by selecting an asset symbol (name);<br>

B. Afterwards, run the price change query, again selecting the value of the imported asset symbol.

Below is a table containing the routes that can be used for the api operations mentioned in the steps above. Note that there is an additional route that just queries the asset, basically mirroring the data structure.

### Routes and features

|  Nº |                             URL                                          |					               Function                                             |
|-----|--------------------------------------------------------------------------|--------------------------------------------------------------------------------------|
|  1  | https://(url_server):7162/api/YahooFinance/import?assetName=(asset_name) | Imports the data needed to generate the analytical report                            |
|  2  | https://(url_server):7162/api/Asset/price-change?assetName=(asset_name)  | Get an analytical price change report for the last 30 trading sessions of an asset   |
|  3  | https://(url_server):7162/api/YahooFinance/asset?assetName=(asset_name)  | Obtains the entire data structure of an asset by querying the Yahoo Financial system |
|     |                                                                          |                                                                                      |

## Development Team

-   [Misael C. Homem](https://www.linkedin.com/in/misael-da-costa-homem-8b07a158/) (Developer, Analyst, Architect)

## Features

-   Query data directly from the Yahoo Finance API;
-   Scale and record the data in its own database;
-   Generate analytical report of asset price variation and percentages in relation to the previous day and in relation to the first trading session;

## Technologies Used

-   C#. net core
-   ASP.NET core api rest
-   Swagger
-   Entity Framework core
-   Sql Server 2019

## How to Contribute

1.  Fork the project
2.  Clone the fork to your local machine
3.  Create a branch for your changes: `git checkout -b my-branch`
4.  Make changes and commit: `git commit -am "My changes"`
5.  Push your branch to your fork: `git push origin my-branch`
6.  Open a Pull Request in the original repository
7.  Wait for your Pull Request to be reviewed and merged