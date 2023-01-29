import React, { useState, useEffect } from "react";
import TicketForm from "./TicketForm";
import ticketStore from "../../stores/ticketStore";
import { toast } from "react-toastify";
import * as ticketActions from "../../actions/ticketActions";
import { loadUsers } from "../../actions/userActions";
import userStore from "../../stores/userStore";

const ManageTicketPage = (props) => {
  const [errors, setErrors] = useState({});
  const [tickets, setTickets] = useState(ticketStore.getTickets());
  const [ticket, setTicket] = useState({
    id: null,
    //slug: "",
    title: "",
    assignedToId: null,
    description: "",
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

    if (!ticket.title) _errors.title = "Title is required";
    if (!ticket.assignedToId) _errors.assignedToId = "User ID is required";
    if (!ticket.description) _errors.description = "Description is required";
    if (!ticket.projectId) _errors.projectId = "Project/Client is required";
    if (!ticket.ticketTypeId) _errors.ticketTypeId = "Ticket Type is required";
    if (!ticket.status) _errors.status = "Status is required";
    if (!ticket.priority) _errors.priority = "Priority is required";

    setErrors(_errors);
    // Form is valid if the errors object has no properties
    return Object.keys(_errors).length === 0;
  }

  function handleSubmit(event) {
    event.preventDefault();
    if (!formIsValid()) return;
    ticketActions.saveTicket(ticket).then(() => {
      props.history.push("/tickets");
      toast.success("Ticket " + ticket.title + " saved.");
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
      <h2>Manage Ticket</h2>
      <TicketForm
        errors={errors}
        ticket={ticket}
        users={users}
        onChange={handleChange}
        onSubmit={handleSubmit}
      />
    </>
  );
};

export default ManageTicketPage;
