import React from "react";
import { Link } from "react-router-dom";
import TicketsByStatusChart from "./TicketsByStatusChart";
import TicketsByPriorityChart from "./TicketsByPriorityChart";
import TicketsByTypeChart from "./TicketsByTypeChart";

function DashboardPage() {
  return (
    <div className="jumbotron">
      <h2>Tickets Dashboard</h2>
      <div class="container">
        <div class="row">
          <div class="col-sm-4">
            <TicketsByStatusChart></TicketsByStatusChart>
          </div>
          <div class="col-sm-4">
            <TicketsByPriorityChart></TicketsByPriorityChart>
          </div>
          <div class="col-sm-4">
            <TicketsByTypeChart></TicketsByTypeChart>
          </div>
        </div>
      </div>
      <Link to="/tickets" className="btn btn-primary">
        View All tickets
      </Link>
    </div>
  );
}

export default DashboardPage;
