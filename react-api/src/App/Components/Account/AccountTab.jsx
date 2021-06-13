import React, { Component } from "react";
import { Tabs, TabItem, TabsContent, TabPanel } from "react-foundation";
import UserService from "../../../Services/UserService";
import AccountPosts from "./AccountPosts";
import AccountTopics from "./AccountTopics";

export default class AccountTab extends Component {
  constructor() {
    super();
    this.state = {
      activeIndex: 1,
      posts: [],
      isLoadedPosts: false,
      topics: [],
      isLoadedTopics: false,
    };
  }

  componentDidMount() {
    UserService.getProfilePosts(this.props.accountId).then((res) => {
      this.setState({
        posts: res,
        isLoadedPosts: true,
      });
    });
    UserService.getProfileTopics(this.props.accountId).then((res) => {
      this.setState({
        topics: res,
        isLoadedTopics: true,
      });
    });
  }

  render() {
    if (!this.state.isLoadedPosts || !this.state.isLoadedTopics) {
      return <div>Загрузка...</div>;
    }
    return (
      <div>
        <link
          rel="stylesheet"
          type="text/css"
          href="https://cdnjs.cloudflare.com/ajax/libs/foundation/6.5.1/css/foundation-float.min.css"
        />
        <Tabs>
          <TabItem
            isActive={this.state.activeIndex === 1}
            onClick={(e) => {
              this.setState({ activeIndex: 1 });
            }}
          >
            <a href={"#posts"}>Posts</a>
          </TabItem>
          <TabItem
            isActive={this.state.activeIndex === 2}
            onClick={(e) => {
              this.setState({ activeIndex: 2 });
            }}
          >
            <a href={"#topics"}>Topics</a>
          </TabItem>
        </Tabs>
        <TabsContent>
          <TabPanel id={"tab2"} isActive={this.state.activeIndex === 1}>
            <AccountPosts posts={this.state.posts.items} />
          </TabPanel>
          <TabPanel id={"tab2"} isActive={this.state.activeIndex === 2}>
            <AccountTopics topics={this.state.topics.items} />
          </TabPanel>
        </TabsContent>
      </div>
    );
  }
}
