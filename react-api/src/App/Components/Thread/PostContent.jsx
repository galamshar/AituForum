import "../../assets/css/style.css";
import React from "react";
import { Link } from "react-router-dom";
import { Cell } from "react-foundation";
import UserService from "../../../Services/UserService"
export default class PostContent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isLogged: true,
      content: []
    };
  }

componentDidMount() {
      var tmpposts = []
      this.props.posts.map((post,index) => {
      UserService.getProfile(post.authorId).then(user => {
        let tmpcontent = <Cell large={12} className="row panel grid-x" key={post.id}>
        <Cell large={2} className="column profile-left">
          <Cell large={10} className="column prof-element">
            <img
              src="https://avatarfiles.alphacoders.com/495/49573.jpg"
              alt="avatar"
            />
            <Link to={`/profile/${post.authorId}`}>{user.login}</Link>
          </Cell>
          <Cell large={12} className="column prof-element extra-info">
            <dl className="pairs pairs-justified">
              <dt>Threads</dt>
              <dd>{user.topicCount}</dd>
            </dl>
            <dl className="pairs pairs-justified">
              <dt>Posts</dt>
              <dd>{user.postCount}</dd>
            </dl>
            <dl className="pairs pairs-justified">
              <dt>Score</dt>
              <dd>{user.score}</dd>
            </dl>
          </Cell>
        </Cell>
        <Cell large={10} className="column profile-right">
          <Cell large={12} className="column prof-element">
            <Cell large={12} className="row content-element">
              <span>{post.writtenDate.split("T")[0]}</span>
              <Link to={window.location.pathname + '#' + index} id={index} className="floatr">
                {`#${index}`}
              </Link>
            </Cell>
            <Cell large={12} className="row content-element">
              <span>
                {post.text}
              </span>
            </Cell>
            <Cell
              large={12}
              className="row content-element content-element-foot"
            >
              {/* <FontAwesomeIcon icon={faHeart} />
              <span>Like</span> */}
            </Cell>
          </Cell>
        </Cell>
      </Cell>
        tmpposts.push(tmpcontent)
      })
      .then(()=>{
        this.setState({
          content : tmpposts.sort((a, b) => (a.key > b.key) ? 1 : -1)
        })
      })
    });
    
  }

  render(){
    return (
      this.state.content
    )
  }
  }

