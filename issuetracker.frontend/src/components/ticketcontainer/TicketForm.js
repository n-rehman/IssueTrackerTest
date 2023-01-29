import TextInput from "../common/TextInput";
import TextAreaInput from "../common/TextAreaInput";
import PropTypes from "prop-types";
import { TicketPriority, TicketStatus } from "../common/ProjectEnums.js";

function TicketForm(props) {
  return (
    <div class="w-75 p-3">
      <form onSubmit={props.onSubmit}>
        <TextInput
          id="title"
          label="Title"
          onChange={props.onChange}
          name="title"
          value={props.ticket.title}
          error={props.errors.title}
        />

        <div className="form-group">
          <label htmlFor="tickettype" class="fw-bold">
            Ticket Type
          </label>
          <div className="field">
            <select
              id="tickettype"
              name="ticketTypeId"
              onChange={props.onChange}
              value={props.ticket.ticketTypeId || ""}
              className="form-control"
            >
              <option value="">Please Select</option>
              <option value="1">Bug</option>
              <option value="2">Change Request</option>
              <option value="3">Data Issue</option>
            </select>
          </div>
          {props.errors.ticketTypeId && (
            <div className="alert alert-danger">
              {props.errors.ticketTypeId}
            </div>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="project" class="fw-bold">
            Project/Client
          </label>
          <div className="field">
            <select
              id="project"
              name="projectId"
              onChange={props.onChange}
              value={props.ticket.projectId || ""}
              className="form-control"
            >
              <option value="">Please Select</option>
              <option value={1}>Project 1</option>
              <option value={2}>Project 2</option>
            </select>
          </div>
          {props.errors.projectId && (
            <div className="alert alert-danger">{props.errors.projectId}</div>
          )}
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
          {props.errors.assignedToId && (
            <div className="alert alert-danger">
              {props.errors.assignedToId}
            </div>
          )}
        </div>
        <TextAreaInput
          id="description"
          label="Description"
          name="description"
          onChange={props.onChange}
          value={props.ticket.description}
          error={props.errors.description}
        />
        <div className="form-group">
          <label htmlFor="priorityField" class="fw-bold">
            Priority
          </label>
          <div className="field">
            <select
              id="priorityField"
              name="priority"
              onChange={props.onChange}
              value={parseInt(props.ticket.priority)}
              className="form-control"
            >
              <option value="">Please Select</option>
              <option value={TicketPriority.None}>None</option>
              <option value={TicketPriority.Low}>Low</option>
              <option value={TicketPriority.Medium}>Medium</option>
              <option value={TicketPriority.High}>High</option>
            </select>
          </div>
          {props.errors.priority && (
            <div className="alert alert-danger">{props.errors.priority}</div>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="statusField" class="fw-bold">
            Status
          </label>
          <div className="field">
            <select
              id="statusField"
              name="status"
              onChange={props.onChange}
              value={props.ticket.status}
              className="form-control"
            >
              <option value="">Please Select</option>
              <option value={TicketStatus.Open}>Open</option>
              <option value={TicketStatus.InProgress}>InProgress</option>
              <option value={TicketStatus.Resolved}>Resolved</option>
            </select>
          </div>
          {props.errors.status && (
            <div className="alert alert-danger">{props.errors.status}</div>
          )}
        </div>
        <br></br>
        <input type="submit" value="Save" className="btn btn-primary" />
      </form>
    </div>
  );
}

TicketForm.propTypes = {
  ticket: PropTypes.object.isRequired,
  onSubmit: PropTypes.func.isRequired,
  onChange: PropTypes.func.isRequired,
  errors: PropTypes.object.isRequired,
};

export default TicketForm;
