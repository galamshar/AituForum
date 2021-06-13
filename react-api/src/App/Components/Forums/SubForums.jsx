import React from "react";
import ForumsCategory from "./ForumsCategory.jsx";
import ForumsHeader from "./ForumsHeader.jsx";
import ForumsContent from "./ForumsContent.jsx";
import ThreadsHeader from "./ThreadsHeader.jsx";
import ThreadsContent from "./ThreadsContent.jsx"
import { Grid, Cell, Row, Column } from "react-foundation";
import TopicService from "../../../Services/TopicService";
import { Link } from "react-router-dom";

export default class SubForums extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      subtopics: [],
      threads : [],
      isLoaded: false,
      error: null
    };
  }

  componentDidUpdate(prevProps, prevState){
    if(prevProps.location != this.props.location){
      window.location.reload()
    }
}

  componentDidMount(){
    TopicService.getSubTopics(this.props.match.params.subTopicId, 1, 10).then(
      (res) => {
        var subtopicsWithoutPost = [];
        var subtopicsWithPost = [];
        this.setState({
          isLoaded: true,
        });
        res.items.map((topic) => {
          if (topic.rules.canOwnPosts) {
            subtopicsWithPost.push(topic);
          } else {
              subtopicsWithoutPost.push(topic)
          }
        });
        this.setState({
            subtopics : subtopicsWithoutPost,
            threads : subtopicsWithPost
        })
      },
      (error) => {
        this.setState({
          isLoaded: true,
          error,
        });
      }
    );
  }


  render() {
    const { subtopics, isLoaded, error,threads } = this.state;
    if (error) {
      return <div>Ошибка: {error.message}</div>;
    } else if (!isLoaded) {
      return <div>Загрузка...</div>;
    } else {
      var content = [];
      content.push(<div className="row mb forum"><Link to={"/forum/create/" + this.props.match.params.subTopicId}><button>Create Thread</button></Link></div>)
      if (subtopics.length > 0) {
        content.push(<div className="row mb forum" key={this.props.match.params.subTopicId}>
        <Row className="toggleview">
          <ForumsHeader />
          <ForumsContent subtopics={subtopics} />
        </Row>
      </div>)
      }
      if(threads.length > 0){
          content.push(<div className="row mb" key={this.props.match.params.subTopicId}>
          <Row className="toggleview">
            <ThreadsHeader />
            <ThreadsContent subtopics={threads} />
          </Row>
        </div>)
      }
      if(content.length > 0){
          return content
      }
      
      return(
        <div className="row mb forum">
        <Link to={"/forum/create/" + this.props.match.params.subTopicId}><button>Create Thread</button></Link>
        <Row className="toggleview">
          <span>Error, the topic is empty</span>
        </Row>
      </div>
      )
    }
  }
}
