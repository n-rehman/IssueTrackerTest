import React from "react";
import TextInput from "../common/TextInput";
import PropTypes from "prop-types";

function UserForm(props) {
  return (
    <div class="w-75 p-3">
      <form onSubmit={props.onSubmit} className="jumbotron">
        <TextInput
          id="name"
          label="Name"
          onChange={props.onChange}
          name="name"
          value={props.user.name}
          error={props.errors.name}
        />
        <br></br>
        <input type="submit" value="Save" className="btn btn-primary" />
      </form>
    </div>
  );
}

UserForm.propTypes = {
  user: PropTypes.object.isRequired,
  onSubmit: PropTypes.func.isRequired,
  onChange: PropTypes.func.isRequired,
  errors: PropTypes.object.isRequired,
};

export default UserForm;
