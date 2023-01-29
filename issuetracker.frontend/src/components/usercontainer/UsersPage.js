import React, { useState, useEffect } from "react";
import userStore from "../../stores/userStore";
import UserList from "./UserList";
import { Link } from "react-router-dom";
import { loadUsers, deactivateUser } from "../../actions/userActions";

function UsersPage() {
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
      <h2>User List</h2>
      <UserList users={users} deactivateUser={deactivateUser} />
    </>
  );
}

export default UsersPage;
