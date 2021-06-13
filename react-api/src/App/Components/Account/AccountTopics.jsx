import "../../assets/css/style.css";

import React from "react";
import { Grid, Cell } from "react-foundation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faNewspaper } from "@fortawesome/free-regular-svg-icons";
import UserService from "../../../Services/UserService";
import { Link } from "react-router-dom";
export default class AccountTopics extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    var content = [];
    this.props.topics.map((topic) => {
      let datetime = new Date(topic.updatedDate);
      content.push(
        <Grid
          large={12}
          className="forum-topic"
          key={parseInt(`${topic.id}${topic.creatorId}`)}
        >
          <Cell large={1} className="column lpad">
            <FontAwesomeIcon icon={faNewspaper} />
          </Cell>
          <Cell large={7} small={8} className="column lpad">
            <span className="overflow-control">
              <Link to={`/thread/${topic.id}`}>
                {topic.name}
              </Link>
            </span>
            <span className="overflow-control">
              {topic.description}
            </span>
          </Cell>
          <Cell large={4} className="column lpad">
            <span className="center">
              {datetime.getDate() +
                "/" +
                datetime.getMonth() +
                "/" +
                datetime.getFullYear() +
                " " +
                datetime.getHours() +
                ":" +
                datetime.getMinutes() +
                ":" +
                datetime.getSeconds()}
            </span>
          </Cell>
        </Grid>
      );
    });
    return content;
  }
}
