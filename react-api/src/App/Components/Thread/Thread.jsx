import React from "react";
import { Grid, Cell, Row, Column } from "react-foundation";
import PostContent from "./PostContent";
import PostingField from "./PostingField";
import ThreadPostHeader from "./ThreadPostHeader";
import PostService from "../../../Services/PostService"
import TopicService from "../../../Services/TopicService"
import { Redirect } from "react-router-dom";
export default class Forums extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      posts : [],
      isLoaded: false,
      error: null,
      isLogged : true
    }
  }

  componentDidMount(){
    PostService.getPostById(this.props.match.params.postId).then(
      (res) => {
        this.setState({
          isLoaded: true,
          posts : res.items
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
    }
    else if(!this.state.isLoaded){
      return <div>Загрузка...</div>;
    }
    return (
      <div>
        <Grid className="row">
          <ThreadPostHeader threadInfo = {this.props.location.topicInfo}/>
          <PostContent posts={this.state.posts}/>
          <PostingField topicId={this.props.match.params.postId} />
        </Grid>
      </div>
    );
  }
}
