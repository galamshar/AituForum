import '../../assets/css/style.css'
import React from 'react';
import { Grid, Cell } from 'react-foundation';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowCircleUp } from '@fortawesome/free-solid-svg-icons';
import $ from 'jquery'
export default class ForumsCategory extends React.Component{
    constructor(props){
        super(props);
    }

    onClickToggle() {
        $('a[data-connect]').unbind().click( function() {
            var i = $(this).find('i');
            i.hasClass('fas fa-arrow-circle-up') ? i.removeClass('fas fa-arrow-circle-up').addClass('fas fa-arrow-circle-down') : i.removeClass('fas fa-arrow-circle-down').addClass('fas fa-arrow-circle-up');
            $(this).parent().parent().toggleClass('all').next().slideToggle();
          });
      };

    render(){
        return(
            <Grid large={12} className="forum-category rounded top">
                <Cell large={8} small={10} className="column lpad">
           {this.props.rtopic}
        </Cell>
                <Cell large={4} small={2} className="column lpad ar">
                    <a data-connect className = "toggler" onClick={() => this.onClickToggle()}>
                        <i className="fas fa-arrow-circle-up"></i>
                    </a>
                </Cell>
            </Grid>
        )
    }
}