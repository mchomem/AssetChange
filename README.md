# AssetChange

AssetChange is an information system that allows you to obtain data from the Yahoo Financial system, extract and store this data in a database and generate a report on asset price changes.
This project was inspired by [Guidti Challenge](https://github.com/guideti/variacao-ativo){:target="_blank"}.

## Getting Started: How to use

After proper deployment on an application server with IIS (Internet Information Services), ask your administrator for the url made available for the application.

Note: This REST API can be consumed by applications with free access to the AssetChange url.

### Routes and features

|								URL										   |					Function     					                                  |
|--------------------------------------------------------------------------|--------------------------------------------------------------------------------------|
| https://(url_server):7162/api/Asset/price-change?assetName=(asset_name)  | Get an analytical price change report for the last 30 trading sessions of an asset   |
| https://(url_server):7162/api/YahooFinance/asset?assetName=(asset_name)  | Obtains the entire data structure of an asset by querying the Yahoo Financial system |
| https://(url_server):7162/api/YahooFinance/import?assetName=(asset_name) | Imports the data needed to generate the analytical report                            |
|                                                                          |                                                                                      |

## Development Team

-   Misael C. Homem (Developer, Analyst, Architect)

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