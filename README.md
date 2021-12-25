# BugTracker-NET-Core
***
A bug tracker portofolio project. The website is available [here](https://bugtracky.azurewebsites.net/).
***

## Intro
One of the most importants things when working is to plan the tasks and time it precisely. Applications like Jira, Monday and Trello are helpful accomplishing these goals. Building a similar projects helped me to understand better how the management apps work.

## Features
In the app there are implemented the following features:

1. Projects
  <ul>
    <li>Create projects</li>
    <li>Assign priority</li>
  </ul>
2. Issues
  <ul>
    <li>Write a summary and description of the tasks</li>
    <li>Assign yourself to tasks</li>
  </ul>
3. Types and Priorities
  <ul>
    <li>The issues and projects have different priorities</li>
    <li>The issue types are helpful to identify the type of work involved</li>
  </ul>
4. Activity
  <ul>
    <li>Track time of your work</li>
    <li>Report work</li>
  </ul>

## Technology
The app was built in ASP.NET Core 5. I used as many features from the framework as possible, by starting from the default controllers and views generated from the database models.
In order to provide email confirmation at register, I user the SendGrid SMTP service. There is a Email client, which sents the emails with the confirmation links.

## Run
Open BugTracker.sln using Visual Studio 2019. Press the run button from the tooltip, with the IIS Express enabled.

## Next
For the moment, the application is quite simple and it needs improvement. Some of the features proposed are:
  <ul>
    <li>Kanban board for planning</li>
    <li>Work ordering system</li>
    <li>Notifications</li>
    <li>Messaging service</li>
    <li>Smart time tracking</li>
  </ul>

