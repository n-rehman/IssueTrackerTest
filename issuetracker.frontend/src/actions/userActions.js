import dispatcher from "../appDispatcher";
import * as userApi from "../api/userApi";
import actionTypes from "./actionTypes";

export function loadUsers() {
  return userApi.getUsers().then((users) => {
    dispatcher.dispatch({
      actionType: actionTypes.LOAD_USERS,
      users: users,
    });
  });
}
export function saveUser(user) {
  return userApi.saveUser(user).then((savedUser) => {
    // Hey dispatcher, go tell all the stores that a course was just created.
    dispatcher.dispatch({
      actionType: user.id ? actionTypes.UPDATE_USER : actionTypes.CREATE_USER,
      user: savedUser,
    });
  });
}
export function deactivateUser(id) {
  return userApi.deactivateUser(id).then(() => {
    dispatcher.dispatch({
      actionType: actionTypes.DELETE_USER,
      id: id,
    });
  });
}
