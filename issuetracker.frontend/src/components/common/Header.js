import React from "react";
import { NavLink } from "react-router-dom";

function Header() {
  const activeStyle = { color: "orange" };
  return (
    <nav class="navbar navbar-expand-sm bg-dark navbar-dark">
      <NavLink activeStyle={activeStyle} class="nav-item" exact to="/">
        Home
      </NavLink>
      {" | "}
      <NavLink activeStyle={activeStyle} to="/users">
        Users
      </NavLink>
      {" | "}
       <NavLink activeStyle={activeStyle} to="/tickets">
        Tickets
      </NavLink>
      {" | "}
      <NavLink activeStyle={activeStyle} to="/about">
        About
      </NavLink>
      
    </nav>

    
  );
}

export default Header;
