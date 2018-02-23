# Hair_Salon

#### Mvc C# exercise, 02/09/2018

#### By **Justin Lardani**

## Description

Hair_Salon is a web application built with the C# Asp Net Core MVC framework as well as MySQL. Users can add stylists to a hair salon as well as add clients to stylists. Afterwards, they can view all stylists along with their clients.

## Setup/Installation Requirements

* Clone this repository to your desktop.
* Navigate to the cloned directory called HairSalon in a terminal capable of running dotnet commands.
* Run the command >dotnet add package Microsoft.AspNetCore.StaticFiles -v 1.1.3.
* Run the command >dotnet add package MySqlConnector
* Run the command >dotnet restore
* In MySQL, run the following commands:
    CREATE DATABASE justin_lardani;
    USE justin_lardani;
    CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
    CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT)
* Run the command >dotnet run
* Navigate to localhost:5000 in your browser.

## Known Bugs

No known bugs at this time.

## Support and contact details

If you have suggestions for how to help us make any additions, or if you have other feedback, please feel free to contact Justin at jlardani93@gmail.com. All feedback is welcome.

## Technologies Used

* C# .Net Core MVC
* Razor
* MySql
* HTML
* Bootstrap
* CSS


### License

*This software is licensed under the MIT license.*

Copyright (c) 2018 **Justin Lardani**
