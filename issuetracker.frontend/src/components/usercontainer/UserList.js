import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import Tooltip from "@material-ui/core/Tooltip";
import Moment from "moment";

function UserList(props) {
  return (
    <table class="table table-striped" width="50%">
      <thead class="table-dark">
        <tr>
          <th>&nbsp;</th>
          <th>Name</th>
          <th>Created On</th>
          <th>Last Modified On</th>
        </tr>
      </thead>
      <tbody>
        {props.users.map((user) => {
          return (
            <tr key={user.id}>
              <td>
                <button
                  className="btn btn-outline-danger"
                  onClick={() => {
                    props.deactivateUser(user.id);
                  }}
                >
                  Deactivate
                </button>
              </td>
              <td>
                <Tooltip anchorId="modify-link" title="Click to view/modify">
                  <Link id="modify-link" to={"/user/" + user.id}>
                    {user.name}
                  </Link>
                </Tooltip>
              </td>
              <td>{Moment(user.dateCreated).format("llll")}</td>

              <td>{Moment(user.dateModified).format("llll")}</td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

UserList.propTypes = {
  deactivateUser: PropTypes.func.isRequired,
  users: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    })
  ).isRequired,
};

export default UserList;
