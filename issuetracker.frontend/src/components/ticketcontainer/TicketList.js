import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import TicketStatusDisplay from "../common/TicketStatusDisplay";
import TicketPriorityDisplay from "../common/TicketPriorityDisplay";
import { TicketPriority, TicketStatus } from "../common/ProjectEnums.js";
import { toast } from "react-toastify";
import Tooltip from "@material-ui/core/Tooltip";
import { format } from "date-fns";
import Moment from "moment";

function TicketList(props) {
  const [search, setSearch] = React.useState("");

  const handleSearch = (event) => {
    setSearch(event.target.value);
  };

  var filteredTickets =
    search === "-1" || search === ""
      ? props.tickets
      : props.tickets.filter((ticket) => {
          return ticket.status === parseInt(search);
        });

  return (
    <>
      <label htmlFor="search">
        Filter by Status:
        <select
          id="search"
          name="search"
          onChange={(event) => handleSearch(event)}
          className="form-control"
        >
          <option value="-1">All</option>
          <option value={TicketStatus.Open}>Open</option>
          <option value={TicketStatus.InProgress}>InProgress</option>
          <option value={TicketStatus.Resolved}>Resolved</option>
        </select>
      </label>

      <table class="table table-striped">
        <thead class="table-dark">
          <tr>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>Title</th>
            <th>Priority</th>
            <th>Status</th>
            <th>Created On</th>
            <th>Last Modified On</th>
          </tr>
        </thead>
        <tbody>
          {filteredTickets.map((ticket) => {
            return (
              <tr key={ticket.id}>
                <td>
                  <Link
                    to="/"
                    className="btn btn-outline-danger"
                    onClick={() => {
                      props.closeTicket(ticket);
                      toast.success("Ticket " + ticket.title + " Closed");
                    }}
                  >
                    Close
                  </Link>
                </td>
                <td>
                  <Link
                    to={"/assignticket/" + ticket.id}
                    className="btn btn-outline-primary"
                  >
                    Assign
                  </Link>
                </td>
                <td>
                  <Tooltip anchorId="modify-link" title="Click to view/modify">
                    <Link to={"/ticket/" + ticket.id}>{ticket.title}</Link>
                  </Tooltip>
                </td>
                <td>{<TicketPriorityDisplay ticket={ticket} />}</td>
                <td>{<TicketStatusDisplay ticket={ticket} />}</td>
                <td>{Moment(ticket.dateCreated).format("llll")}</td>

                <td>{Moment(ticket.dateModified).format("llll")}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
}

TicketList.propTypes = {
  closeTicket: PropTypes.func.isRequired,
  tickets: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      title: PropTypes.string.isRequired,
      assignedToId: PropTypes.number.isRequired,
      description: PropTypes.string.isRequired,
    })
  ).isRequired,
};

export default TicketList;
