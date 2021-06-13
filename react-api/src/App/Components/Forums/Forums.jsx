import React from 'react';
import ForumsCategory from './ForumsCategory.jsx'
import ForumsHeader from './ForumsHeader.jsx';
import ForumsContent from './ForumsContent.jsx';
import { Grid, Cell, Row, Column } from 'react-foundation';

export default class Forums extends React.Component{

    constructor(props){
        super(props);
        this.state = {
            isLoadedRoot : false,
            error : null,
            rtopics : [],
            isLoadedSubtopics : false,
            subtopics : []
        };
    }

    

    componentDidMount(){
        fetch('https://localhost:44382/api/topics/root-topics/?pageNumber=1&pageSize=10')
        .then(res => res.json())
        .then(
            (resultR) => {
            this.setState({
                isLoadedRoot : true,
                rtopics : resultR.items
                })
            },
            (error) => {
                this.setState({
                    isLoadedRoot : true,
                    error
                })
            }
        )
        .then(resultR => {
        var urls = []
        this.state.rtopics.map(rtopic => urls.push('https://localhost:44382/api/topics/sub-topics/?topicId='+rtopic.id+'&pageNumber=1&pageSize=10'))
        var promises = urls.map(url => fetch(url))
        Promise.all(promises)
        .then(responses => Promise.all(responses.map(res => res.json())))
        .then(resultS => {
            var items = [];
            resultS.map(result => result.items.map(item => items.push(item)))
            this.setState({
            isLoadedSubtopics : true,
            subtopics : items
        })})})
    }

    render(){
        const { error, isLoadedRoot, rtopics, isLoadedSubtopics,subtopics } = this.state;
        if (error) {
            return <div>Ошибка: {error.message}</div>;
          } else if (!isLoadedRoot || !isLoadedSubtopics) {
            return <div>Загрузка...</div>;
          } else {
            var forums = [];
            rtopics.map(roottopic => {
                var forumsContent = [];
                subtopics.map(subtopic => {
                    if (subtopic.parentTopicId == roottopic.id) {
                        forumsContent.push(subtopic)
                    };
                });
                forums.push(
                    <div className = "row mb" key={roottopic.id}>
                        <ForumsCategory rtopic = {roottopic.name}/>
                        <Row className="toggleview">
                            <ForumsHeader />
                            <ForumsContent subtopics = {forumsContent}/>
                        </Row>
                    </div> 
                     )
            })
            return forums;
        }
    }
}