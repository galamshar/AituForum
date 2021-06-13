import React from "react";
import { Grid, Cell, Row, Column } from "react-foundation";
import UserService from "../../../Services/UserService";
import { Redirect } from "react-router-dom";
import AccountTab from "./AccountTab";

export default class Account extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      accInfo: [],
      isLoaded: false,
      error: null,
      isLogged: true,
    };
  }

  componentDidMount() {
    UserService.getProfile(this.props.match.params.accountId).then(
      (res) => {
        this.setState({
          isLoaded: true,
          accInfo: res,
        });
      },
      (error) => {
        this.setState({
          isLogged: false,
        });
      }
    );
  }

  render() {
    if (!this.state.isLogged) {
      return <Redirect to="/login" />;
    } else if (!this.state.isLoaded) {
      return <div>Загрузка...</div>;
    }
    var createDate = new Date(this.state.accInfo.createOn);
    var lastActivity = new Date(this.state.accInfo.lastTime);
    return (
      <Cell className="row panel grid-x account">
        <Cell large={3} className="column profile-left">
          <Cell large={12} className="column prof-element">
            <img
              src="https://avatarfiles.alphacoders.com/495/49573.jpg"
              alt="avatar"
            />
          </Cell>
          <Cell large={12} className="column prof-element">
            <div className="info">
              <span className="column">
                Reg.date :{" "}
                {createDate.getDate() +
                  "/" +
                  createDate.getMonth() +
                  "/" +
                  createDate.getFullYear()}
              </span>
              <span className="column">
                Roles :{" "}
                {this.state.accInfo.roles.map((role) => role.name + " /")}
              </span>
              <span className="column">Score : {this.state.accInfo.score}</span>
            </div>
          </Cell>
        </Cell>
        <Cell large={9} className="column profile-right">
          <Cell large={12} className="column prof-element">
            <Cell className="column prof-head">
              <span>{this.state.accInfo.login}</span>
              <span className="floatr">
                {lastActivity.getDate() +
                  "/" +
                  lastActivity.getMonth() +
                  "/" +
                  lastActivity.getFullYear() +
                  " " +
                  lastActivity.getHours() +
                  ":" +
                  lastActivity.getMinutes()}
              </span>
            </Cell>
            <Cell className="column statistic">
              <a href="" className="page-counter">
                <div className="count">{this.state.accInfo.postCount}</div>
                <div className="muted">posts</div>
              </a>
              <a href="" className="page-counter">
                <div className="count">{this.state.accInfo.topicCount}</div>
                <div className="muted">topics</div>
              </a>
            </Cell>
          </Cell>
          <Cell large={12} className="column prof-element">
            <AccountTab accountId={this.props.match.params.accountId} />
          </Cell>
        </Cell>
      </Cell>
    );
  }
}
