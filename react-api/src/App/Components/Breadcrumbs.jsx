import "../assets/css/style.css";
import React from "react";
import { Link, Grid, BreadcrumbItem, Breadcrumbs } from "react-foundation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHome } from "@fortawesome/free-solid-svg-icons";

export default class Breadcrumb extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    return (
      <Grid className="row">
        <Breadcrumbs large={12} className="column lpad top-msg">
          <BreadcrumbItem>
            <a href="/">
              <FontAwesomeIcon icon={faHome} />
            </a>
          </BreadcrumbItem>
        </Breadcrumbs>
      </Grid>
    );
  }
}
