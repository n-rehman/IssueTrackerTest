import "bootstrap/dist/css/bootstrap.min.css";
import "./AppTheme.css";
import HeaderNavigation from "./components/common/template/HeaderNavigation";
import SideNavigation from "./components/common/template/SideNavigation";
import { Col, Row } from "reactstrap";
import UsersPage from "./components/usercontainer/UsersPage";
import TicketsPage from "./components/ticketcontainer/TicketsPage";
import ManageTicketPage from "./components/ticketcontainer/ManageTicketPage";
import ManageTicketAssignPage from "./components/ticketcontainer/ManageTicketAssignPage";
import ManageUserPage from "./components/usercontainer/ManageUserPage";
import { Route, Switch, Redirect } from "react-router-dom";
import NotFoundPage from "./components/NotFoundPage";
import DashboardPage from "./components/DashboardPage";
import { ToastContainer } from "react-toastify";

function AppTheme() {
  const styles = {
    contentDiv: {
      display: "flex",
    },
    contentMargin: {
      marginLeft: "10px",
      width: "100%",
    },
  };
  return (
    <>
      <Row>
        <Col>
          <HeaderNavigation></HeaderNavigation>
        </Col>
      </Row>

      <div style={styles.contentDiv}>
        <SideNavigation></SideNavigation>
        <div style={styles.contentMargin}>
          <div style={styles.contentMargin} class="headerimage"></div>
          <h1 style={{ padding: "1%" }}></h1>
          <ToastContainer autoClose={4000} hideProgressBar />
          <Switch>
            <Route path="/users" component={UsersPage} />
            <Route path="/tickets" component={TicketsPage} />
            <Route path="/user/:slug" component={ManageUserPage} />
            <Route path="/user" component={ManageUserPage} />
            <Route path="/ticket/:slug" component={ManageTicketPage} />
            <Route path="/ticket" component={ManageTicketPage} />
            <Route
              path="/assignticket/:slug"
              component={ManageTicketAssignPage}
            />
            <Route path="/" component={DashboardPage} />
            <Route component={NotFoundPage} />
          </Switch>
        </div>
      </div>
    </>
  );
}

export default AppTheme;
