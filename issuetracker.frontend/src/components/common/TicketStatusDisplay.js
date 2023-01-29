import React from "react";
import PropTypes from "prop-types";
import { TicketStatus } from "./ProjectEnums.js";

function TicketStatusDisplay(props) {
  
  var displayValue = "";
  if (props.ticket.status == TicketStatus.Open)
    displayValue = "Open";
  else if (props.ticket.status == TicketStatus.InProgress)
    displayValue = "InProgress" ;
  else if (props.ticket.status == TicketStatus.Resolved)
    displayValue = "Resolved";
  
  return (
   <div>
      {displayValue}
       </div>
 
   
  );
}


export default TicketStatusDisplay;
