import "../../assets/css/style.css";
import React from "react";
import { Cell } from "react-foundation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCalendarAlt } from "@fortawesome/free-regular-svg-icons";
import { faUser } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
export default class ThreadHeader extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div large={12} className="thread-content">
        <Cell>
        <Cell large={12} className="forum-category rounded top">
          <Cell large={12} small={12} className="lpad">
            WINNERS OF AITU CHALLENGE!
          </Cell>
        </Cell>
        <Cell large={12} className="thread-info column" >
          <Cell large={6} className="column" >
            <ul>
              <li>
                <FontAwesomeIcon icon={faUser} />
                <Link to='profile/'>ADMIN</Link>
              </li>
              <li>
                <FontAwesomeIcon icon={faCalendarAlt} />
                <span>17.11.2020</span>
              </li>
            </ul>
          </Cell>
        </Cell>
      </Cell>
      </div>
    );
  }
}
