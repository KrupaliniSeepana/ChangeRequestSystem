# Software Change Request System

This project is a web-based application developed using ASP.NET MVC. It is designed to manage software change requests within a team, including workflows for request approval, assignment, and tracking.

## Project Overview

The Software Change Request (SCR) System allows users assigned to a project to create, approve, and track change requests. Requests follow a predefined workflow involving requesters, managers, and developers. The system provides a timeline to display each stage of a change request's progress.

## Prerequisites

- **.NET Framework**: Ensure you have the required .NET Framework version installed.
- **Visual Studio**: Visual Studio 2019 or later is recommended.
- **SQL Server**: For database management.
- **SMTP Server** (optional): For email notifications.

## Setup Instructions

1. **Clone the Repository**
   - Clone this repository to your local machine:
     ```bash
     git clone https://github.com/your-username/ChangeRequestSystem.git
     ```

2. **Open the Project in Visual Studio**
   - Open the `ChangeRequestSystem` solution file in Visual Studio.

3. **Database Configuration**
   - Use the provided `schema.sql` file to set up the database tables:
     - `Users`: Stores details of users, including their roles.
     - `Roles`: Contains roles such as Requester, Manager, Developer.
     - `ChangeRequests`: Fields include title, description, priority, due date, and timestamps.
     - `Teams`: Stores information on developer teams.
     - `Timeline`: Tracks the request's stages (start/completion dates, responsible user, comments).
   - Update the connection string in the `web.config` file under `<connectionStrings>`:
     ```xml
     <connectionStrings>
         <add name="DefaultConnection" connectionString="Your Connection String Here" providerName="System.Data.SqlClient" />
     </connectionStrings>
     ```

4. **Install NuGet Packages**
   - Restore required NuGet packages by building the solution.

5. **Configure Email Notifications (Optional)**
   - To enable email notifications, configure SMTP settings in `web.config`.

## Running the Project

1. Set the `ChangeRequestSystem` project as the startup project.
2. Press `F5` or select **Start Debugging** from Visual Studio's toolbar to run the project.
3.Restore NuGet Packages
Ensure all NuGet packages required by the project are restored. Visual Studio typically handles this automatically when you build the project, but if needed, you can run:
nuget restore
4.Build the Project
Build the solution to ensure all dependencies are compiled. In the Visual Studio terminal, run
msbuild ChangeRequestSystem.sln
5.Run the Project
To start the project in Visual Studio:
Using Debug Mode: Press F5 or run
start ChangeRequestSystem.sln
6.Without Debug Mode: Press Ctrl + F5 or run:
start ChangeRequestSystem.sln /no-debug
7.Run Database Migrations
Update-Database
8.Build and Run Tests
dotnet test ChangeRequestSystem.Tests

## Testing Instructions

1. **Access the Application**
   - Once the project is running, go to `http://localhost:[Port]` in the browser.

2. **Test User Roles and Workflows**
   - Log in as different roles (Requester, Manager, Developer) and perform role-specific actions:
     - **Requester**: Raise a change request.
     - **Manager**: Approve/reject requests and assign them to developers.
     - **Developer**: Update request progress and submit for review.

3. **Validate Timeline Tracking**
   - Check that each stage (e.g., request raised, approved, in development) displays correctly in the timeline with accurate start and completion dates.

4. **Check Notifications**
   - Verify that email notifications (if configured) are sent at appropriate workflow stages.

5. **Optional Tests**
   - If implemented, test any bonus features like JWT-based authentication, AJAX updates on the timeline, and the Kanban board for task tracking.


