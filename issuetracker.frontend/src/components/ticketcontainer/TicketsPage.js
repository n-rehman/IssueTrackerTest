import React, { useState, useEffect } from "react";
import ticketStore from "../../stores/ticketStore";
import TicketList from "./TicketList";
import { Link } from "react-router-dom";
import { loadTickets, closeTicket } from "../../actions/ticketActions";
import axios from "axios";

function TicketsPage() {
  const [tickets, setTickets] = useState(ticketStore.getTickets());

  useEffect(() => {
    ticketStore.addChangeListener(onChange);
    if (tickets.length === 0) loadTickets();
    return () => ticketStore.removeChangeListener(onChange); // cleanup on unmount
  }, [tickets.length]);

  function onChange() {
    setTickets(ticketStore.getTickets());
  }

  return (
    <>
      <h2>Support Ticket List</h2>

      <TicketList tickets={tickets} closeTicket={closeTicket} />
    </>
  );
}

export default TicketsPage;
