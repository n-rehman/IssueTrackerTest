import React from "react";
import PropTypes from "prop-types";
import { TicketPriority } from "./ProjectEnums.js";

function TicketStatusDisplay(props) {
  
  var displayValue = "";
  if (props.ticket.status == TicketPriority.None)
    displayValue = "None";
  else if (props.ticket.status == TicketPriority.Low)
      displayValue = "Low";
else if (props.ticket.status == TicketPriority.Medium)
    displayValue = "Medium";
  else if (props.ticket.status == TicketPriority.High)
    displayValue = "High";
  
  return (
   <div>
      {displayValue}
       </div>
 
   
  );
}


export default TicketStatusDisplay;
