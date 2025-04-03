# telegra.ph

telegra.ph as a service in ASP.NET Core

## Overview

This project provides a service to interact with telegra.ph, a minimalist publishing tool, using ASP.NET Core. It allows users to create, edit, and manage their articles through a simple API.

## Features

- Create new articles
- Edit existing articles
- Retrieve articles by their path

## Requirements

- .NET 9 SDK
- Visual Studio 2022 or later

## Getting Started

1. Clone the repository:
2. Open the solution in Visual Studio:
3. Restore the dependencies:
4. Run the application:
## Usage

### Create a new article

To create a new article, send a POST request to `/api/articles` with the following JSON payload:
### Edit an existing article

To edit an existing article, send a PUT request to `/api/articles/{id}` with the updated JSON payload:
### Retrieve an article

To retrieve an article, send a GET request to `/api/articles/{path}`.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
