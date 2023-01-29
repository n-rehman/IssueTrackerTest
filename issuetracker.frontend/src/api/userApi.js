import {
  handleResponse,
  handleError,
  handleResponseObject,
  handleResponseObjectSave,
} from "./apiUtils";
import axios from "axios";
const baseUrl = process.env.REACT_APP_USERS_API_URL + "/users/";

export function getUsers() {
  return axios.get(baseUrl).then(handleResponseObject).catch(handleError);
}
export function getUserBySlug(slug) {
  return axios
    .get(baseUrl + "?id=" + slug)
    .then((response) => {
      if (!response.status === 200)
        throw new Error("Network response was not ok.");
      return response.json().then((users) => {
        if (users.length !== 1) throw new Error("User not found: " + slug);
        return users[0]; // should only find one user for a given slug, so return it.
      });
    })
    .catch(handleError);
}

export function saveUser(user) {
  if (user.id == null) {
    return axios
      .post(baseUrl + "user", {
        name: user.name,
      })
      .then(handleResponseObjectSave)
      .catch(handleError);
  }

  return axios
    .put(baseUrl + "user", {
      id: user.id,
      name: user.name,
    })
    .then(handleResponseObjectSave)
    .catch(handleError);
}
export function deactivateUser(userId) {
  return fetch(baseUrl + userId, { method: "DELETE" })
    .then(handleResponse)
    .catch(handleError);
}
