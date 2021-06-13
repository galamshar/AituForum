import "../assets/css/style.css";
import React from "react";
import { Grid, Cell, Alignments } from "react-foundation";
import { Link } from "react-router-dom";
import AuthService from "../../Services/AuthService";
export default class Header extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      currentUser: AuthService.getCurrentUser(),
    };
  }
  render() {
    const { currentUser } = this.state;
    return (
      <header id="#top">
        <Grid className="row">
          <Cell large={4} className="column lpad">
            <Cell className="logo">
              <a href="/">
                <img
                  src="https://astanait.edu.kz/wp-content/uploads/2020/05/aitu-logo-white-2-300x154.png"
                  alt="aitu"
                  width="100px"
                  height=""
                />
              </a>
            </Cell>
          </Cell>
          <Cell
            alignment={Alignments.RIGHT}
            large={8}
            className="column ar lpad"
          >
            <nav className="menu">
              <Link to="#">Members</Link>
              <Link to="#">Forum Actions</Link>
              <Link to="#">FAQ</Link>
              <Link to="#">Contact</Link>
              {currentUser != null ? <Link to={'/profile/' + currentUser.userId}>{currentUser.login}</Link> : <a href='/login'>Login</a>}
              <Link to="#" onClick={AuthService.logout}>Logout</Link>
            </nav>
          </Cell>
        </Grid>
      </header>
    );
  }
}
