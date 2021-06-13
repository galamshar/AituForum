import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFacebookF,
  faGooglePlusG,
  faLinkedinIn,
} from "@fortawesome/free-brands-svg-icons";
import "../../assets/css/login/login.css";
import AuthService from "../../../Services/AuthService";
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";

const required = (value) => {
  if (!value) {
    return (
      <div className="alert alert-danger" role="alert">
        <span>This field is required!</span>
      </div>
    );
  }
};

export default class LoginPage extends React.Component {
  constructor(props) {
    super(props);
    this.handleLogin = this.handleLogin.bind(this);
    this.onChangeUsername = this.onChangeUsername.bind(this);
    this.onChangePassword = this.onChangePassword.bind(this);
    this.rightActive = this.rightActive.bind(this);
    this.rightInActive = this.rightInActive.bind(this);
    this.onClickLoginBtn = this.onClickLoginBtn.bind(this);
    this.onClickRegBtn = this.onClickRegBtn.bind(this);
    this.state = {
      username: "",
      password: "",
      loading: false,
      message: "",
      isRightActive: false,
      action: null,
    };
  }
  onChangeUsername(e) {
    this.setState({
      username: e.target.value,
    });
  }

  onChangePassword(e) {
    this.setState({
      password: e.target.value,
    });
  }

  onClickLoginBtn(e) {
    this.setState({
      action: "login",
    });
  }

  onClickRegBtn(e) {
    this.setState({
      action: "register",
    });
  }

  rightActive(e) {
    this.setState(state => ({
      isRightActive: true,
      username : "",
      password : "",
      message: "",
      action: "register"
    }))
  }

  rightInActive(e) {
    this.setState({
      isRightActive: false,
      username: "",
      password: "",
      message: "",
      action: "login"
    });
  }

  handleLogin(e) {
    e.preventDefault();

    this.setState({
      message: "",
      loading: true,
    });

    this.form.validateAll();
    if (this.checkBtn.context._errors.length === 0 || this.checkBtn2.context._errors.length === 0) {
      if (this.state.action == "login") {
        AuthService.login(this.state.username, this.state.password).then(
          (res) => {
            if (res) {
              this.props.history.push("/");
              window.location.reload();
            } else {
              this.setState({
                loading: false,
                message: "Incorrect login or password!",
                action: null,
              });
            }
          },
          (error) => {
            const resMessage =
              (error.response &&
                error.response.data &&
                error.response.data.message) ||
              error.message ||
              error.toString();

            this.setState({
              loading: false,
              message: resMessage,
              action: null,
            });
          }
        );
      } else {
        AuthService.register(this.state.username, this.state.password).then(
          (res) => {
            if (res) {
              this.props.history.push("/");
              window.location.reload();
            } else {
              this.setState({
                loading: false,
                message: "This account already exist!",
                action: null,
              });
            }
          },
          (error) => {
            const resMessage =
              (error.response &&
                error.response.data &&
                error.response.data.message) ||
              error.message ||
              error.toString();

            this.setState({
              loading: false,
              action: null,
              message: resMessage,
            });
          }
        );
      }
    } else {
      this.setState({
        loading: false,
      });
    }
  }

  render() {
    return (
      <div className="login-page">
        <a href="/">
          <h2>AITU FORUM</h2>
        </a>
        <div
          className={`container ${
            this.state.isRightActive ? "right-panel-active" : ""
          }`}
          id="container"
        >
          <div className="form-container sign-up-container">
            <Form
              method="post"
              id="registerform"
              onSubmit={this.handleLogin}
              ref={(c) => {
                this.form = c;
              }}
            >
              <h1>Create Account</h1>
              <div className="social-container">
                <a href="#" className="social">
                  <FontAwesomeIcon icon={faFacebookF} />
                </a>
                <a href="#" className="social">
                  <FontAwesomeIcon icon={faGooglePlusG} />
                </a>
                <a href="#" className="social">
                  <FontAwesomeIcon icon={faLinkedinIn} />
                </a>
              </div>
              <span>or use your email for registration</span>
              <Input
                type="text"
                placeholder="Username"
                id="username"
                name="username"
                min="3"
                max="16"
                value={this.state.username}
                onChange={this.onChangeUsername}
                validations={[required]}
              />
              <Input
                type="password"
                placeholder="Password"
                id="password"
                name="password"
                min="3"
                max="16"
                value={this.state.password}
                onChange={this.onChangePassword}
                validations={[required]}
              />
              <button
                id="signup"
                type="submit"
                name="signup"
                disabled={this.state.loading}
                onClick={this.onClickRegBtn}
              >
                Sign Up
              </button>
              <br />
              <span id="register-errormsg"></span>
              <CheckButton
                style={{ display: "none" }}
                ref={(c) => {
                  this.checkBtn2 = c;
                }}
              />
            </Form>
          </div>
          <div className="form-container sign-in-container">
            <Form
              id="login-form"
              onSubmit={this.handleLogin}
              ref={(c) => {
                this.form = c;
              }}
            >
              <h1>Sign in</h1>
              <div className="social-container">
                <a href="#" className="social">
                  <FontAwesomeIcon icon={faFacebookF} />
                </a>
                <a href="#" className="social">
                  <FontAwesomeIcon icon={faGooglePlusG} />
                </a>
                <a href="#" className="social">
                  <FontAwesomeIcon icon={faLinkedinIn} />
                </a>
              </div>
              <span>or use your account</span>
              <Input
                type="text"
                placeholder="Username"
                id="login"
                name="login"
                min="3"
                max="32"
                value={this.state.username}
                onChange={this.onChangeUsername}
                validations={[required]}
              />
              <Input
                type="password"
                placeholder="Password"
                id="password"
                name="password"
                min="3"
                max="16"
                value={this.state.password}
                onChange={this.onChangePassword}
                validations={[required]}
              />
              <a href="#">Forgot your password?</a>
              <button
                id="signin"
                type="submit"
                name="signin"
                disabled={this.state.loading}
                onClick={this.onClickLoginBtn}
              >
                Sign In
              </button>
              <br />
              {this.state.message && (
                <span id="login-errormsg">{this.state.message}</span>
              )}
              <CheckButton
                style={{ display: "none" }}
                ref={(c) => {
                  this.checkBtn = c;
                }}
              />
            </Form>
          </div>
          <div className="overlay-container">
            <div className="overlay">
              <div className="overlay-panel overlay-left">
                <h1>Welcome Back!</h1>
                <p>
                  To keep connected with us please login with your personal info
                </p>
                <button
                  className="ghost"
                  id="signIn"
                  onClick={this.rightInActive}
                >
                  Sign In
                </button>
              </div>
              <div className="overlay-panel overlay-right">
                <h1>Hello, Friend!</h1>
                <p>Enter your personal details and start journey with us</p>
                <button
                  className="ghost"
                  id="signUp"
                  onClick={this.rightActive}
                >
                  Sign Up
                </button>
              </div>
            </div>
          </div>
        </div>

        <footer>
          <p>© Copyrights 2020, AITU FORUM. All rights reserved.</p>
        </footer>
      </div>
    );
  }
}
