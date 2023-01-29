import dispatcher from "../appDispatcher";
import * as ticketApi from "../api/ticketApi";
import actionTypes from "./actionTypes";

export function saveTicket(ticket) {
  return ticketApi.saveTicket(ticket).then((savedTicket) => {
    // Hey dispatcher, go tell all the stores that a ticket was just created/updated.
    dispatcher.dispatch({
      actionType: ticket.id
        ? actionTypes.UPDATE_TICKET
        : actionTypes.CREATE_TICKET,
      ticket: savedTicket,
    });
  });
}

export function loadTickets() {
  return ticketApi.getTickets().then((tickets) => {
    dispatcher.dispatch({
      actionType: actionTypes.LOAD_TICKETS,
      tickets: tickets,
    });
  });
}

export function closeTicket(ticket) {
  return ticketApi.closeTicket(ticket).then((savedTicket) => {
    dispatcher.dispatch({
      actionType: actionTypes.CLOSE_TICKET,
      ticket: savedTicket,
    });
  });
}

export function assignTicket(ticket) {
  return ticketApi.assignTicket(ticket).then((savedTicket) => {
    // Hey dispatcher, go tell all the stores that a ticket was just assigned.
    dispatcher.dispatch({
      actionType: actionTypes.ASSIGN_TICKET,
      ticket: savedTicket,
    });
  });
}
