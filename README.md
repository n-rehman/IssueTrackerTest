# Starter Kit used from facebook creat-react-app

## Get Started

1. **Install [Node 18](https://nodejs.org)** or newer.
2. **Install .net core 6

### Project Structure

**Server**
| Built in .net core 6, web api
| IssueTracker.API project is for controller/api calls, it's using token based authentication to secure endpoints(configured to use only in production)
| IssueTracker.Business project have services & business logic
| IssueTracker.Data contains model classes and DbContext related code
| IssueTracker.Common contains common request/response classes used by business/api projects
| IssueTracker.Business.UnitTests cover business logic unit tests
| For Testing Purpose, it's using in-memory EntityFramework database so all but static data will be lost whenever Api project re-run
| Web Api must be running for Front-End to work, Open IssueTracker.Application.sln in VS that support .net core 6 & Start it
| Note down port after localhost as it will be required in Front-End Setting

**Client/FrontEnd**
| Built in react 17 & connecting to server via Web API
| For more details and how to run setup Front-End go to README.md in IssueTracker\issuetracker.frontend folder



Any issues please reach-out