import React, { useState, useEffect } from "react";
import { Chart } from "react-google-charts";
import ticketStore from "../stores/ticketStore";
import { loadTickets, closeTicket } from "../actions/ticketActions";
import axios from "axios";

function TicketsByStatusChart() {
  const [tickets, setTickets] = useState(ticketStore.getTickets());

  useEffect(() => {
    ticketStore.addChangeListener(onChange);
    if (tickets.length === 0) loadTickets();
    return () => ticketStore.removeChangeListener(onChange); // cleanup on unmount
  }, [tickets.length]);

  function onChange() {
    setTickets(ticketStore.getTickets());
  }

  var openStatusCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.status === 0).length;

  var activeStatusCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.status === 1).length;

  var resolvedStatusCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.status === 2).length;

  const data = [
    ["Task", "Number of Tickets"],
    ["Resolved", resolvedStatusCount],
    ["Open", openStatusCount],
    ["InProgress", activeStatusCount],
  ];

  const options = {
    title: "Support Tickets by Status",
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

export default TicketsByStatusChart;
