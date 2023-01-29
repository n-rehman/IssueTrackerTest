# Starter Kit used from facebook creat-react-app

## Get Started

1. **Install [Node 18](https://nodejs.org)** or newer.
2. **Navigate to this project's root directory on the command line.**
3. **Install Node Packages.** - `npm install`

### Project Structure

**Server**
| Built in .net core 6, web api
| IssueTracker.API project is for controller/api calls
| IssueTracker.Business project have services & business logic
| IssueTracker.Data contains model classes and DbContext related code
| IssueTracker.Common contains common request/response classes used by business/api projects
| IssueTracker.Business.UnitTests cover business logic unit tests
| For Testing Purpose, it's using in-memory EntityFramework database so all but static data will be lost whenever Api project re-run
| Web Api must be running for Front-End to work, Open IssueTracker.Application.sln in VS that support .net core 6 & run in
| Note down port after localhost as it will be required in Front-End Setting

**Client/FrontEnd**
| Built in react 17
| Recommend using Vs Code
| Open Folder in vs code IssueTracker\issuetracker.frontend
| Run npm install
| Go to Package.json and replace port for variable REACT_APP_TICKETS_API_URL to web api port noted when running web api from Visual Studio i.e REACT_APP_TICKETS_API_URL=https://localhost:7206
| If you don't see data, it means web api isn't running or port settings are incorrect in pacakage.json file
| Having issues? See below.

### Having Issues running front end ? Try these things first:

1. Run `npm install` - If you forget to do this, you'll get an error when you try to start the app later.
2. Don't run the project from a symbolic link. It will cause issues with file watches.
3. Delete any .eslintrc in your user directory and disable any ESLint plugin / custom rules within your editor since these will conflict with the ESLint rules defined in the course.
4. On Windows? Open your console as an administrator. This will assure the console has the necessary rights to perform installs.
5. Ensure you do not have NODE_ENV=production in your env variables as it will not install the devDependencies. To check run this on the command line: `set NODE_ENV`. If it comes back as production, you need to clear this env variable.
6. Nothing above work? Delete your node_modules folder and re-run npm install.
7. If don't see data ensure web api is running & ensure port is correct in REACT_APP_TICKETS_API_URL i.e same where web api is running

### Production Dependencies

| **Dependency**      | **Use**                                              |
| ------------------- | ---------------------------------------------------- |
| bootstrap           | CSS Framework                                        |
| immer               | Helper for working with immutable data               |
| prop-types          | Declare types for props passed into React components |
| react               | React library                                        |
| react-dom           | React library for DOM rendering                      |
| react-flux          | Connects React components to Flux                    |
| react-router-dom    | React library for routing                            |
| react-toastify      | Display messages to the user                         |
| react-google-charts | Library for unidirectional data flows                |
| react-pro-sidebar   | NavBar library                                       |
| react-axios         | Api Calls library                                    |

### Development Dependencies

| **Dependency**                     | **Use**                                                          |
| ---------------------------------- | ---------------------------------------------------------------- |
| @babel/core                        | Transpiles modern JavaScript so it runs cross-browser            |
| @testing-library/react             | Test React components                                            |
| @wojtekmaj/enzyme-adapter-react-17 | Configure Enzyme to work with React 17                           |
| babel-eslint                       | Lint modern JavaScript via ESLint                                |
| babel-loader                       | Add Babel support to Webpack                                     |
| babel-preset-react-app             | Babel preset for working in React. Used by create-react-app too. |
| css-loader                         | Read CSS files via Webpack                                       |
| cssnano                            | Minify CSS                                                       |
| enzyme                             | Simplified JavaScript Testing utilities for React                |
| eslint                             | Lints JavaScript                                                 |
| eslint-loader                      | Run ESLint via Webpack                                           |
| eslint-plugin-import               | Advanced linting of ES6 imports                                  |
| eslint-plugin-react                | Adds React-related rules to ESLint                               |
| fetch-mock                         | Mock fetch calls                                                 |
| html-webpack-plugin                | Generate HTML file via webpack                                   |
| http-server                        | Lightweight HTTP server to serve the production build locally    |
| jest                               | Automated testing framework                                      |
| json-server                        | Create mock API that simulates create, update, delete            |
| mini-css-extract-plugin            | Extract imported CSS to a separate file via Webpack              |
| node-fetch                         | Make HTTP calls via fetch using Node - Used by fetch-mock        |
| npm-run-all                        | Display results of multiple commands on single command line      |
| postcss                            | Post-process CSS                                                 |
| postcss-loader                     | Post-process CSS via Webpack                                     |
| react-test-renderer                | Render React components for testing                              |
| rimraf                             | Delete files and folders                                         |
| style-loader                       | Insert imported CSS into app via Webpack                         |
| webpack                            | Bundler with plugin ecosystem and integrated dev server          |
| webpack-bundle-analyzer            | Generate report of what's in the app's production bundle         |
| webpack-cli                        | Run Webpack via the command line                                 |
| webpack-dev-server                 | Serve app via Webpack                                            |
