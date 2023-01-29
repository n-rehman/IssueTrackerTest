import React, { useState, useEffect } from "react";
import { Chart } from "react-google-charts";
import ticketStore from "../stores/ticketStore";
import { loadTickets, closeTicket } from "../actions/ticketActions";
import axios from "axios";

function TicketsByPriorityChart() {
  const [tickets, setTickets] = useState(ticketStore.getTickets());

  useEffect(() => {
    ticketStore.addChangeListener(onChange);
    if (tickets.length === 0) loadTickets();
    return () => ticketStore.removeChangeListener(onChange); // cleanup on unmount
  }, [tickets.length]);

  function onChange() {
    setTickets(ticketStore.getTickets());
  }

  var nonePriorityCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.priority === 0).length;

  var lowPriorityCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.priority === 1).length;

  var medPriorityCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.priority === 2).length;

  var highPriorityCount =
    tickets.length === 0 ? 0 : tickets.filter((t) => t.priority === 3).length;

  const data = [
    ["Task", "Number of Tickets"],
    ["None", nonePriorityCount],
    ["Low", lowPriorityCount],
    ["Medium", medPriorityCount],
    ["High", highPriorityCount],
  ];

  const options = {
    title: "Support Tickets by Priority",
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

export default TicketsByPriorityChart;
