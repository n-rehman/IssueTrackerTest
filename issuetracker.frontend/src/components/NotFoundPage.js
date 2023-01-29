import React from "react";
import { Link } from "react-router-dom";
function NotFoundPage() {
  return (
    <div class="col-md-12 d-flex flex-column justify-content-center align-items-center text-black vh-100">
 




<h1>404</h1>
<h4>Page not found</h4>
<p>Oops! The page you are looking for does not exist. It might have been moved or deleted.</p>



      
      <p>
        <Link to="/">Back to Home</Link>
      </p>
    </div>
  );
}

export default NotFoundPage;
