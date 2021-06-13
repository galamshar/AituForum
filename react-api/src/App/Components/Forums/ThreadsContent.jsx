import "../../assets/css/style.css";

import React from "react";
import { Grid, Cell } from "react-foundation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faNewspaper } from "@fortawesome/free-regular-svg-icons";
import TopicService from "../../../Services/TopicService";
import { Link } from "react-router-dom";
export default class ForumsContent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      countOfPosts: {},
    };
  }
  componentDidMount() {
    this.props.subtopics.map((subtopic) => {
        TopicService.getCountOfPosts(subtopic.id).then((res) => {
            this.setState((prevState) => {
              let countOfPosts = Object.assign(
                {},
                prevState.countOfPosts
              );
              countOfPosts[subtopic.id] = res;
              return { countOfPosts };
            });
          })
    });
  }
  render() {
    var content = [];
    this.props.subtopics.map((subtopic) => {
      content.push(
        <Grid large={12} className="forum-topic" key={parseInt(`${subtopic.parentTopicId}${subtopic.id}`)}>
          <Cell large={1} className="column lpad">
            <FontAwesomeIcon icon={faNewspaper} />
          </Cell>
          <Cell large={7} small={8} className="column lpad">
            <span className="overflow-control">
              <Link to={`/thread/${subtopic.id}`}>{subtopic.name}</Link>
            </span>
            <span className="overflow-control">
              {subtopic.description}
            </span>
          </Cell>
          <Cell large={2} className="column lpad">
            <span className="center">
            {this.state.countOfPosts[subtopic.id]}
            </span>
          </Cell>
          <Cell large={2} small={4} className="column pad">
            <span>
              <a href="#">Some sub-topic</a>
            </span>
            <span>08-29-2013 7:29PM</span>
            <span>
              by <a to="#">Some user</a>
            </span>
          </Cell>
        </Grid>
      );
    });
    return content;
  }
}
