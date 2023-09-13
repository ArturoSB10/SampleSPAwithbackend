# SampleSPAwithbackend

## Instalations

Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

## ConnectionStrings

Check the connection string in the appsettings.json.

## Database

Run the file SampleSPA.sql in the database your going to use.

## About

This is the back end of the first excersice. To store the users from the JSON file you must previously had to login with the admin credentials and send the JSON file with the next format for example:

{
  "token": "3302d25a39",
  "users": [
    {
      "username": "usuario1",
      "password": "Co@ntraseña1"
    },
    {
      "username": "usuario2",
      "password": "contraseña2"
    },
    {
      "username": "usuario3",
      "password": "Co@ntraseña1"
    }
  ]
}

You has to inclue the token from the login result.
