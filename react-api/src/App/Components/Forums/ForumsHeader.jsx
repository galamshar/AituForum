import '../../assets/css/style.css'
import React from 'react';
import { Grid, Cell } from 'react-foundation';
export default class ForumsHeader extends React.Component{
    render(){
      return(
              <Grid className="forum-head row" large={12}>
              <Cell large = {8} small = {8} className="column lpad">
                Forums
              </Cell>
              <Cell large = {1} className="column lpad">
                Threads
              </Cell>
              <Cell large = {1} className="column lpad">
                Post
              </Cell>
              <Cell large = {2} small = {4} className="column lpad">
                Freshness
              </Cell>
            </Grid>
      )
    }
}