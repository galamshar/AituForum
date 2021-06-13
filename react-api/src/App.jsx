import "./App/assets/css/foundation.min.css";
import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Header from "./App/Components/Header.jsx";
import Breadcrumbs from "./App/Components/Breadcrumbs.jsx";
import Forums from "./App/Components/Forums/Forums.jsx";
import LoginPage from "./App/Components/Login/LoginPage";
import SubForums from "./App/Components/Forums/SubForums";
import Thread from "./App/Components/Thread/Thread";
import { Grid } from "react-foundation";
import Account from "./App/Components/Account/Account";
import CreateThread from "./App/Components/Thread/CreateThread";
export default class App extends React.Component {
  render() {
    return (
      <div>
        <Switch>
          <Route exact path="/">
            <Header />
            <Breadcrumbs />
            <Forums />
          </Route>
          <Route
            exact
            path="/forum/:subTopicId"
            render={(props) => (
              <div>
                <Header />
                <Breadcrumbs />
                <SubForums {...props} />
              </div>
            )}
          />
          <Route
            path="/thread/:postId"
            render={(props) => (
              <div>
                <Header />
                <Breadcrumbs />
                <Thread {...props} />
              </div>
            )}
          ></Route>
          <Route path="/login" component={LoginPage} />
          <Route
            path="/profile/:accountId"
            render={(props) => (
              <div>
                <Header />
                <Breadcrumbs />
                <Account {...props} />
              </div>
            )}
          ></Route>
          <Route
            path="/forum/create/:parentTopicId"
            render={(props) => (
              <div>
                <Header />
                <Breadcrumbs />
                <CreateThread {...props} />
              </div>
            )}
          ></Route>
        </Switch>
      </div>
    );
  }
}
