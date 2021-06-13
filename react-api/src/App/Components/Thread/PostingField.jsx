import React from "react";
import Form from "react-validation/build/form";
import PostService from "../../../Services/PostService"
import autosize from 'autosize'

export default class PostingField extends React.Component {
    constructor(props) {
      super(props);
      this.onChangeText = this.onChangeText.bind(this);
      this.postHandler = this.postHandler.bind(this);
      this.state = {
          text : "",
          loading : false
      }
    }

    componentDidMount() {
        this.textarea.focus();
        autosize(this.textarea)
    }

    onChangeText(e) {
        this.setState({
          text: e.target.value,
        });
      }

    postHandler(e) {
        e.preventDefault();
        this.setState({
            loading : true
        })
        
        PostService.post(this.props.topicId, this.state.text).then((res) => {
            window.location.reload();
        })
    }

    render() {
        return (
            <div className="form-container postform-container">
                <Form
              method="post"
              id="postForm"
              className="post-form"
              onSubmit={this.postHandler}
              ref={(c) => {
                this.form = c;
              }}
            >
                <textarea
                type="text"
                placeholder="Your text here"
                id="postcontent"
                name="postcontent"
                min="3"
                max="256"
                value={this.state.text}
                onChange={this.onChangeText}
                ref={c=>this.textarea=c}
              />
              <button
                id="doPost"
                type="submit"
                name="doPost"
                disabled={this.state.loading}
              >
                Post
              </button>
            </Form>
            </div>
        )
    }
}