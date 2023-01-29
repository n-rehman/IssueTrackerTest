import React from "react";
import DashboardPage from "./DashboardPage";
import AboutPage from "./AboutPage";
import Header from "./common/Header";
import UsersPage from "./usercontainer/UsersPage";
import TicketsPage from "./ticketcontainer/TicketsPage";
import ManageTicketPage from "./ticketcontainer/ManageTicketPage";
import ManageUserPage from "./usercontainer/ManageUserPage";
import { Route, Switch, Redirect } from "react-router-dom";
import NotFoundPage from "./NotFoundPage";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <div className="container-fluid">
      <div class="jumbotron text-center">
        <h1>Issue Tracker</h1>
        <p></p>
      </div>
      <ToastContainer autoClose={3000} hideProgressBar />
      <Header />
      <div className="container-fluid">
        <div class="row">
          <div class="col-md-2 jumbotron">
            <h3></h3>
            <p></p>
            <p></p>
          </div>
          <div class="col-md-10">
            {" "}
            <Switch>
              <Route path="/" exact component={DashboardPage} />
              <Route path="/users" component={UsersPage} />
              <Route path="/tickets" component={TicketsPage} />
              <Route path="/about" component={AboutPage} />
              <Route path="/user/:slug" component={ManageUserPage} />
              <Route path="/user" component={ManageUserPage} />
              <Route path="/ticket/:slug" component={ManageTicketPage} />
              <Route path="/ticket" component={ManageTicketPage} />
              <Redirect from="/about-page" to="about" />
              <Route component={NotFoundPage} />
            </Switch>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
