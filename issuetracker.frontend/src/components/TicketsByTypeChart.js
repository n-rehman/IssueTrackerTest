import React, { useState, useEffect } from "react";
import { Chart } from "react-google-charts";
import ticketStore from "../stores/ticketStore";
import { loadTickets, closeTicket } from "../actions/ticketActions";
import axios from "axios";

function TicketsByTypeChart() {
  const [tickets, setTickets] = useState(ticketStore.getTickets());

  useEffect(() => {
    ticketStore.addChangeListener(onChange);
    if (tickets.length === 0) loadTickets();
    return () => ticketStore.removeChangeListener(onChange); // cleanup on unmount
  }, [tickets.length]);

  function onChange() {
    setTickets(ticketStore.getTickets());
  }

  var bugsCount =
    tickets.length === 0
      ? 0
      : tickets.filter((t) => t.ticketTypeId === 1).length;

  var changeCount =
    tickets.length === 0
      ? 0
      : tickets.filter((t) => t.ticketTypeId === 2).length;

  var dataIssueCount =
    tickets.length === 0
      ? 0
      : tickets.filter((t) => t.ticketTypeId === 3).length;

  const data = [
    ["Task", "Number of Tickets"],
    ["Bug", bugsCount],
    ["Change", changeCount],
    ["DataIssue", dataIssueCount],
  ];

  const options = {
    title: "Support Tickets by Type",
    pieHole: 0.4,
    is3D: false,
    pieSliceText: "value",
  };

  return (
    <>
      <div>
        <Chart
          chartType="PieChart"
          data={data}
          options={options}
          width={"100%"}
          height={"400px"}
        />
      </div>
    </>
  );
}

export default TicketsByTypeChart;
