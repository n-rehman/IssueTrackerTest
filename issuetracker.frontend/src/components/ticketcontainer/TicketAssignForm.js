import TextInput from "../common/TextInput";
import TextAreaInput from "../common/TextAreaInput";
import PropTypes from "prop-types";
import { TicketPriority, TicketStatus } from "../common/ProjectEnums.js";

function TicketAssignForm(props) {
  return (
    <div class="w-75 p-3">
      <form onSubmit={props.onSubmit}>
        <div class="form-horizontal">
          <label htmlFor="title" class="fw-bold">
            Ticket Title
          </label>
          <div className="field">
            <label id="title" label="Title" name="title">
              {" "}
              {props.ticket.title}
            </label>
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="description" class="fw-bold">
            Ticket Description
          </label>
          <div className="field">
            <label id="description" label="Description" name="description">
              {" "}
              {props.ticket.description}
            </label>
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="assigned" class="fw-bold">
            Assigned To
          </label>
          <div className="field">
            <select
              id="assigned"
              name="assignedToId"
              onChange={props.onChange}
              value={props.ticket.assignedToId || ""}
              className="form-control"
            >
              <option value="">Please Select</option>
              {props.users.map((user) => {
                return <option value={user.id}>{user.name}</option>;
              })}
            </select>
          </div>
          <TextAreaInput
            id="comment"
            label="Comments"
            name="comment"
            onChange={props.onChange}
            value={props.ticket.comment}
            error={props.errors.comment}
          ></TextAreaInput>
          {props.errors.assignedToId && (
            <div className="alert alert-danger">
              {props.errors.assignedToId}
            </div>
          )}
        </div>
        <br></br>
        <input type="submit" value="Save" className="btn btn-primary" />
      </form>
    </div>
  );
}

TicketAssignForm.propTypes = {
  ticket: PropTypes.object.isRequired,
  onSubmit: PropTypes.func.isRequired,
  onChange: PropTypes.func.isRequired,
  errors: PropTypes.object.isRequired,
};

export default TicketAssignForm;
