import {
  handleResponse,
  handleError,
  handleResponseObject,
  handleResponseObjectSave,
} from "./apiUtils";
import axios from "axios";
const baseUrl = process.env.REACT_APP_TICKETS_API_URL + "/tickets/";

export function getTickets() {
  return axios.get(baseUrl).then(handleResponseObject).catch(handleError);
}

export function getTicketBySlug(slug) {
  return axios
    .get(baseUrl + "?id=" + slug)
    .then((response) => {
      if (!response.status === 200)
        throw new Error("Network response was not ok.");
      return response.json().then((tickets) => {
        if (tickets.length !== 1) throw new Error("Ticket not found: " + slug);
        return tickets[0]; // should only find one ticket for a given slug, so return it.
      });
    })
    .catch(handleError);
}

export function saveTicket(ticket) {
  if (ticket.id == null) {
    return axios
      .post(baseUrl + "ticket", {
        title: ticket.title,
        description: ticket.description,
        priority: parseInt(ticket.priority),
        status: parseInt(ticket.status, 0),
        typeId: ticket.ticketTypeId,
        projectId: ticket.projectId,
        assignedToId: ticket.assignedToId,
      })
      .then(handleResponseObjectSave)
      .catch(handleError);
  }

  return axios
    .put(baseUrl + "ticket", {
      id: ticket.id,
      title: ticket.title,
      description: ticket.description,
      priority: parseInt(ticket.priority),
      status: parseInt(ticket.status, 0),
      typeId: ticket.ticketTypeId,
      projectId: ticket.projectId,
      assignedToId: ticket.assignedToId,
    })
    .then(handleResponseObjectSave)
    .catch(handleError);
}

export function closeTicket(ticket) {
  return axios
    .put(baseUrl + "closeticket", {
      id: ticket.id,
    })
    .then(handleResponseObjectSave)
    .catch(handleError);
}

export function assignTicket(ticket) {
  return axios
    .put(baseUrl + "assignticket", {
      ticketId: ticket.id,
      assignedToUserId: ticket.assignedToId,
      comment: ticket.comment,
    })
    .then(handleResponseObjectSave)
    .catch(handleError);
}
