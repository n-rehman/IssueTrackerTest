import { useState } from "react";
import { AiOutlineMenu } from "react-icons/ai";
import {
  FaGem,
  FaHeart,
  FaUsers,
  FaUser,
  FaUserPlus,
  FaEarlybirds,
  FaBug,
  FaMailBulk,
} from "react-icons/fa";
import {
  Menu,
  MenuItem,
  ProSidebar,
  SidebarHeader,
  SubMenu,
} from "react-pro-sidebar";
import "react-pro-sidebar/dist/css/styles.css";
import { Link, Route, Switch, Redirect } from "react-router-dom";
import UsersPage from "../../usercontainer/UsersPage";

function SideNavigation() {
  const [collapsed, setCollapsed] = useState(false);

  // added styles
  const styles = {
    sideBarHeight: {
      height: "100vh",
    },
    menuIcon: {
      float: "right",
      margin: "10px",
    },
  };
  const onClickMenuIcon = () => {
    setCollapsed(!collapsed);
  };
  return (
    <ProSidebar style={styles.sideBarHeight} collapsed={collapsed}>
      <SidebarHeader>
        <div style={styles.menuIcon} onClick={onClickMenuIcon}>
          <AiOutlineMenu />
        </div>
      </SidebarHeader>
      <Menu iconShape="square">
        <MenuItem icon={<FaGem />}>{<Link to="/">Dashboard</Link>}</MenuItem>
        <SubMenu title="Users" icon={<FaUsers />}>
          <MenuItem icon={<FaUser />}>
            {<Link to="/users">View Users</Link>}
          </MenuItem>
          <MenuItem icon={<FaUserPlus />}>
            {<Link to="/user">Add User</Link>}
          </MenuItem>
        </SubMenu>
        <SubMenu title="Support Tickets" icon={<FaEarlybirds />}>
          <MenuItem icon={<FaEarlybirds />}>
            {" "}
            {<Link to="/tickets">View Support Tickets</Link>}
          </MenuItem>
          <MenuItem icon={<FaBug />}>
            {" "}
            {<Link to="/ticket">Raise Support Ticket</Link>}
          </MenuItem>
        </SubMenu>
        <MenuItem icon={<FaMailBulk />}>
          {
            <Link
              to="javascript:void(0)"
              onClick={() => (window.location = "mailto:nabeel304@gmail.com")}
            >
              Contact Us
            </Link>
          }
        </MenuItem>
      </Menu>
    </ProSidebar>
  );
}

export default SideNavigation;
