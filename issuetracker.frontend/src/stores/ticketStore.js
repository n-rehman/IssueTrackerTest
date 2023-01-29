import { EventEmitter } from "events";
import Dispatcher from "../appDispatcher";
import actionTypes from "../actions/actionTypes";

const CHANGE_EVENT = "change";
let _tickets = [];

class TicketStore extends EventEmitter {
  addChangeListener(callback) {
    this.on(CHANGE_EVENT, callback);
  }

  removeChangeListener(callback) {
    this.removeListener(CHANGE_EVENT, callback);
  }

  emitChange() {
    this.emit(CHANGE_EVENT);
  }

  getTickets() {
    return _tickets;
  }

  getTicketBySlug(slug) {
    var foundElement = _tickets.find((ticket) => ticket.id == slug);
    return foundElement;
  }
}

const ticketStore = new TicketStore();

Dispatcher.register((action) => {
  switch (action.actionType) {
    case actionTypes.CLOSE_TICKET:
      _tickets = _tickets.map((ticket) =>
        ticket.id === action.ticket.id ? action.ticket : ticket
      );
      break;
    case actionTypes.CREATE_TICKET:
      _tickets.push(action.ticket);
      ticketStore.emitChange();
      break;
    case actionTypes.UPDATE_TICKET:
      _tickets = _tickets.map((ticket) =>
        ticket.id === action.ticket.id ? action.ticket : ticket
      );
      ticketStore.emitChange();
      break;
    case actionTypes.LOAD_TICKETS:
      _tickets = action.tickets;
      ticketStore.emitChange();
      break;
    case actionTypes.ASSIGN_TICKET:
      _tickets = _tickets.map((ticket) =>
        ticket.id === action.ticket.id ? action.ticket : ticket
      );
      break;
    default:
    // nothing to do here
  }
});

export default ticketStore;
