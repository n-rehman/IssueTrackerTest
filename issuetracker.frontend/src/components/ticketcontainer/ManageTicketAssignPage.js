import React, { useState, useEffect } from "react";
import TicketAssignForm from "./TicketAssignForm";
import ticketStore from "../../stores/ticketStore";
import { toast } from "react-toastify";
import * as ticketActions from "../../actions/ticketActions";
import { loadUsers } from "../../actions/userActions";
import userStore from "../../stores/userStore";

const ManageTicketAssignPage = (props) => {
  const [errors, setErrors] = useState({});
  const [tickets, setTickets] = useState(ticketStore.getTickets());
  const [ticket, setTicket] = useState({
    id: null,
    //slug: "",
    title: "",
    assignedToId: null,
    description: "",
    comment: "",
  });

  useEffect(() => {
    ticketStore.addChangeListener(onChange);
    const ticketId = props.match.params.slug; // from the path `/tickets/:slug`
    if (tickets.length === 0) {
      ticketActions.loadTickets();
    } else if (ticketId) {
      setTicket(ticketStore.getTicketBySlug(ticketId));
    }
    return () => ticketStore.removeChangeListener(onChange);
  }, [tickets.length, props.match.params.slug]);

  function onChange() {
    setTickets(ticketStore.getTickets());
  }

  function handleChange({ target }) {
    setTicket({
      ...ticket,
      [target.name]: target.value,
    });
  }

  function formIsValid() {
    const _errors = {};

    if (!ticket.assignedToId) _errors.assignedToId = "User ID is required";

    setErrors(_errors);
    // Form is valid if the errors object has no properties
    return Object.keys(_errors).length === 0;
  }

  function handleSubmit(event) {
    event.preventDefault();
    if (!formIsValid()) return;
    ticketActions.assignTicket(ticket).then(() => {
      props.history.push("/tickets");
      toast.success("Ticket " + ticket.title + " assigned.");
    });
  }

  const [users, setUsers] = useState(userStore.getUsers());

  useEffect(() => {
    userStore.addChangeListener(onChange);
    if (users.length === 0) loadUsers();
    return () => userStore.removeChangeListener(onChange); // cleanup on unmount
  }, [users.length]);

  function onChange() {
    setUsers(userStore.getUsers());
  }

  return (
    <>
      <h2>Manage Ticket Assignment</h2>
      <TicketAssignForm
        errors={errors}
        ticket={ticket}
        users={users}
        onChange={handleChange}
        onSubmit={handleSubmit}
      />
    </>
  );
};

export default ManageTicketAssignPage;
