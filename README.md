# ![image of dj sacrilicious](img/logo.jpg)

http://arrington.github.io/djsacrilicious 

#### _This application is a revamped simplified version of Michael's DJing website that informs the user about Michael, dates he's already been booked for, his mixes, and permits the admin to book an event through a calendar interface. 11-9-17_

#### By _Michael Arrington, Kim Bordon, Elliot Burin, Alan Falcon_


## Description

_The application informs the admin about Michael, the mixes he's made, and the clients who have booked him in the past. The purpose of this application is to allow potential clients to book Michael's services through a calendar interface._

## Specs

_Front-End: Webpage implements three different breakpoints.  These operate at a width of 1000 pixels, 660 pixels, and 479 pixels._

#### BDD-Specs

| Behavior | Input | Output |
|-|-|-|
| As **casual user**, can view DJ Sacrilicious's upcoming events, as well as events within the past month on main page. | User goes to main page. | Page displays events starting from those furthest out in a black border with details of the event. |
| As **admin**, user can add a new event with details starting time, ending time, event name, venue name, and venue address. | User goes to admin specific page, and enters information form. User clicks "Add." | Admin, with newly added event. |
| As **admin**, on the add page, user can select an event, and form will automatically fill with that events details. | User clicks a displayed event. | Form automatically fills with the clicked event's name, venue name, and venue address. |
| As **admin**, user can edit an event that already exists. | User goes to admin specific edit page, and clicks one of the events. User enters new details, and clicks "EDIT." | Edit page reloads, with the edited event's details included. |
| As **admin**, on the edit page, user can select an event, and form will automatically fill with that events details. | User clicks a displayed event. | Form automatically fills with the clicked event's name, start time, end time, venue name, and venue address. |
| As **admin**, user cannot add or edit events with start and end times that are less an than an hour long. | User enters:<br><br>Starts @:<br>11/17/2017 11:00PM<br>Ends @:<br>11/17/2017 1:30AM <br><br> User clicks "Add" or "Edit" button. | Form shows error message:<br><br> *"Date and time entry was invalid. Make sure start and end time are at least an hour apart, and end time doesn't not come before start."* |
| As **admin**, user cannot add an event that overlaps existing events. | User enters:<br><br>Starts @:<br>11/17/2017 11:00PM<br>Ends @:<br>11/18/2017 11:30PM <br><br> User clicks "Add" button. <br><br> Then, User enters:<br><br>Starts @:<br>11/18/2017 12:00AM<br>Ends @:<br>11/18/2017 03:00AM <br><br> User clicks "Add" button. |  Form shows error message:<br><br> *"Date and time entry was invalid. Make sure entry times do not conflict with exist events."* |
| As **admin**, user can delete a specific event. | User clicks the event, and clicks "DELETE" button. | Admin edit pages refreshes without the deleted event. |

##### Wishlist-Specs

| Behavior | Input | Output |
|-|-|-|
| As **admin**, user has to log in as admin in order add or edit events. | User enters admin username and password | User goes to page to add form. |
| As **casual user**, can click on an individual event to get details, including a map and link to address on Google maps. | User clicks a event. | Details open a pop-up window with details. |

<!-- ## Setup/Installation Requirements

_To install, connect to the internet, open your shell program of choice and type the following:_

_git clone https://github.com/arringtonm/djsacrilicious.git _

_After cloning, open index.html with your web browser or any of the included files in your desired text editor._ -->

## Setup/Installation
* Using your terminal or powershell, clone this repository by typing ```>git clone https://github.com/arringtonm/djsacrilicious.git```
    * Alternatively, you can use a browser to download the .zip file from the Github web interface at the URL: https://github.com/arringtonm/djsacrilicious.git
* To look at project code, navigate to the project folder *djsacrilicious*, and use a text editor like Atom to open the README.md.
* For any use of the application, make sure you have [.NET Core 1.1 SDK (Software Development Kit)](https://download.microsoft.com/download/F/4/F/F4FCB6EC-5F05-4DF8-822C-FF013DF1B17F/dotnet-dev-win-x64.1.1.4.exe) and [.NET runtime](https://download.microsoft.com/download/6/F/B/6FB4F9D2-699B-4A40-A674-B7FF41E0E4D2/dotnet-win-x64.1.1.4.exe) both installed.
* To test the application:
  * First ensure you have the proper database setup by entering starting up MySql, and entering the following commands:
  ```SQL
  > CREATE database dj_s_test;
  > USE dj_s;
  > CREATE TABLE events (id serial PRIMARY KEY, start_time DATETIME UNIQUE KEY, end_time DATETIME UNIQUE KEY, event_name VARCHAR (255), venue_name VARCHAR (255), venue_address VARCHAR (255));
  ```
  * Using powershell or terminal, navigate to the folder named djsacrilicious. Then enter the following commands:
  ```
  >cd DJ.Solution
  >cd DJ.Tests
  >dotnet restore
  >dotnet test
  ```
  * You can view the tests code by using your powershell or terminal in the DJ.Tests folder, then typing ```>cd DJ.Tests``` and then ```> atom .``` to open both the tests on the Stylist and Client classes. If you don't have Atom, use whichever text editor you have available.

* To run the application:
  * First, you must have the proper database setup by following these commands:
  ```SQL
  > CREATE database dj_s;
  > USE dj_s;
  > CREATE TABLE events (id serial PRIMARY KEY, start_time DATETIME UNIQUE KEY, end_time DATETIME UNIQUE KEY, event_name VARCHAR (255), venue_name VARCHAR (255), venue_address VARCHAR (255));
  ```
  * Using powershell or terminal, navigate to the folder named djsacrilicious. Then enter the following commands:
  ```
  >cd DJ.Solution
  >cd DJ
  >dotnet restore
  >dotnet run
  ```
  * Then, on your browser, go to the URL address: localhost:5000 or, whichever server your app might be running on.
  * Use the buttons, and forms to navigate the app.
  * Once you're finished, close the browser and turn off the server by entering <kbd>Ctrl</kbd> + <kbd>C</kbd> on your powershell or terminal.

#### WARNING
 **As of 11/09/2017,** this application doesn't have an actual log in. To alter information, user must enter the page through an "undisclosed" URL.

## Known Bugs

_none so far._

## Support and contact details

_Please contact Michael Arrington at djsacrilicious@gmail.com for any inquiries._

## Technologies Used

_SASS, CSS, HTML, GIT, C#, .NET_

### License

*MIT*

Copyright (c) 2017 **_Michael Arrington, Kim Bordon, Elliot Burin, Alan Falcon_**
