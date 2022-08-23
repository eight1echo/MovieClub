# Movie Club
A sample Web App where users can create a Movie Club and schedule meetups to watch a movie together with other Club members.

## Application Features:
* Register for an account to create a Movie Club or search to join an existing one.
* Host a meetup by searching TheMovieDatabase for a movie and setting the Date, Time, and Location for the meetup.
* Respond to meetup invitations by setting your RSVP status to Attending, Can't Attend, or Undecided.

## Frameworks & Technologies used:
* ASP.NET Core Razor Pages Web Application
* ASP.NET Core Identity
* Entity Framework Core
* MS SQL Server
* Bootstrap

## Architecture & Design:
* Split Application logic into separate services for Commands that modify Database Tables, and Queries that present data to the user.

## Third-Party Dependencies:
* Movie Data obtained from calls to the public TheMovieDatabase API.
