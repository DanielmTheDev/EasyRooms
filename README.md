### About this project 
This application parses an .xps file from oldschool practice management program [Praxxo](https://www.praxxo.de/), distributing the therapies into the rooms configured in the options and generates a .pdf file with the results.

### Getting started 
Clone the repository. To build, use

`dotnet build`

To run, use 

`dotnet run`

Use the options screen of the application to configure available rooms and buffer.

Choose one of the .xps files in the `EasyRooms.Tests\IntegrationTests\TestData\` directory.

Hit `Berechnen` and admire the resulting .pdf :)
