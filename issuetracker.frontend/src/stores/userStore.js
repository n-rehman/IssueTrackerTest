import { EventEmitter } from "events";
import Dispatcher from "../appDispatcher";
import actionTypes from "../actions/actionTypes";

const CHANGE_EVENT = "change";
let _users = [];

class UserStore extends EventEmitter {
  addChangeListener(callback) {
    this.on(CHANGE_EVENT, callback);
  }

  removeChangeListener(callback) {
    this.removeListener(CHANGE_EVENT, callback);
  }

  emitChange() {
    this.emit(CHANGE_EVENT);
  }

  getUsers() {
    return _users;
  }

  getUserBySlug(slug)
  {
    var foundElement = _users.find(user => user.id == slug);
    return foundElement;
  }
}

const userStore = new UserStore();

Dispatcher.register(action => {
  switch (action.actionType) {
    case actionTypes.LOAD_USERS:
      _users = action.users;
      userStore.emitChange();
      break;
    case actionTypes.CREATE_USER:
      _users.push(action.user);
      userStore.emitChange();
      break;
    case actionTypes.UPDATE_USER:
      _users = _users.map(user =>
        user.id === action.user.id ? action.user : user
      );
      userStore.emitChange();
      break;
    default:
    // nothing to do here
  }
});

export default userStore;
