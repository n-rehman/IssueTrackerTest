import React, { useState, useEffect } from "react";
import UserForm from "./UserForm";
import userStore from "../../stores/userStore";
import { toast } from "react-toastify";
import * as userActions from "../../actions/userActions";

const ManageUserPage = (props) => {
  const [errors, setErrors] = useState({});
  const [users, setUsers] = useState(userStore.getUsers());
  const [user, setUser] = useState({
    id: null,
    slug: "",
    name: "",
  });

  useEffect(() => {
    userStore.addChangeListener(onChange);
    const slug = props.match.params.slug; // from the path `/users/:slug`
    if (users.length === 0) {
      userActions.loadUsers();
    } else if (slug) {
      setUser(userStore.getUserBySlug(slug));
    }
    return () => userStore.removeChangeListener(onChange);
  }, [users.length, props.match.params.slug]);

  function onChange() {
    setUsers(userStore.getUsers());
  }

  function handleChange({ target }) {
    setUser({
      ...user,
      [target.name]: target.value,
    });
  }

  function formIsValid() {
    const _errors = {};

    if (!user.name) _errors.name = "Name is required";

    setErrors(_errors);
    // Form is valid if the errors object has no properties
    return Object.keys(_errors).length === 0;
  }

  function handleSubmit(event) {
    event.preventDefault();
    if (!formIsValid()) return;
    userActions.saveUser(user).then(() => {
      props.history.push("/users");
      toast.success("User " + user.name + " saved.");
    });
  }

  return (
    <>
      <h2>Manage User</h2>
      <UserForm
        errors={errors}
        user={user}
        onChange={handleChange}
        onSubmit={handleSubmit}
      />
    </>
  );
};

export default ManageUserPage;
