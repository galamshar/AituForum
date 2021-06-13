import React from "react";
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import Textarea from "react-validation/build/textarea";
import TopicService from "../../../Services/TopicService";
import autosize from "autosize";

export default class CreateThread extends React.Component {
  constructor(props) {
    super(props);
    this.onChangeName = this.onChangeName.bind(this);
    this.onChangeDescription = this.onChangeDescription.bind(this);
    this.createHandler = this.createHandler.bind(this);
    this.state = {
      name: "",
      description: "",
      loading: false,
    };
  }

  componentDidMount() {
    this.input.focus();
    autosize(this.textarea);
  }

  onChangeName(e) {
    this.setState({
      name: e.target.value,
    });
  }

  onChangeDescription(e) {
    this.setState({
      description: e.target.value,
    });
  }
  createHandler(e) {
    e.preventDefault();
    this.setState({
      loading: true,
    });

    TopicService.createSubtopic(
      this.props.match.params.parentTopicId,
      this.state.name,
      this.state.description
    ).then((res) => {
      window.location.href = "/forum/" + this.props.match.params.parentTopicId;
    });
  }

  render() {
    return (
      <div className="row">
        <div className="form-container postform-container">
          <Form
            method="post"
            id="createThread"
            className="post-form"
            onSubmit={this.createHandler}
            ref={(c) => {
              this.form = c;
            }}
          >
            <input
              type="text"
              placeholder="Thread name"
              id="threadname"
              name="threadname"
              min="5"
              max="128"
              value={this.state.name}
              onChange={this.onChangeName}
              ref={(c) => (this.input = c)}
            />
            <textarea
              type="text"
              placeholder="Thread description"
              id="threaddescription"
              name="threaddescription"
              min="5"
              max="256"
              value={this.state.description}
              onChange={this.onChangeDescription}
              ref={(c) => (this.textarea = c)}
            />
            <button
              id="doPost"
              type="submit"
              name="doPost"
              disabled={this.state.loading}
            >
              Create thread
            </button>
          </Form>
        </div>
      </div>
    );
  }
}
