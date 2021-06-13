import "../../assets/css/style.css";

import React from "react";
import { Grid, Cell } from "react-foundation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faNewspaper } from "@fortawesome/free-regular-svg-icons";
import UserService from "../../../Services/UserService";
import { Link } from "react-router-dom";
export default class AccountPosts extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    var content = [];
    this.props.posts.map((post) => {
      let datetime = new Date(post.writtenDate);
      content.push(
        <Grid
          large={12}
          className="forum-topic"
          key={parseInt(`${post.topicId}${post.id}`)}
        >
          <Cell large={1} className="column lpad">
            <FontAwesomeIcon icon={faNewspaper} />
          </Cell>
          <Cell large={7} small={8} className="column lpad">
            <span className="overflow-control">
              <Link to={`/thread/${post.topicId}`}>
                {post.text.substring(0, 15)}
              </Link>
            </span>
            <span className="overflow-control">
              {"... " + post.text.substring(15, 40)}
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
