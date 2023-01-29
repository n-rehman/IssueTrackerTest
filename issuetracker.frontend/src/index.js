import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import { render } from "react-dom";
import App from "./components/App";
import { BrowserRouter as Router } from "react-router-dom";
import AppTheme from "./AppTheme";
import { ProSidebarProvider } from 'react-pro-sidebar';
/*
render(
  <Router>
    <App />
  </Router>,
  document.getElementById("root")
);
*/


render(
 <Router>
    <AppTheme />
    </Router>
 ,
  document.getElementById("root")
);