import React from 'react';
import VoteList from './VoteList';
import VoteComposer from './VoteComposer';
import {fetchJson, sendJson} from '../backend/Backend';

export default class VoteController extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      allVotes: []
    };

    this.setCurrentVote = this.setCurrentVote.bind(this);
    this.registerVote = this.registerVote.bind(this);
    this.activateVoteComposer = this.activateVoteComposer.bind(this);
    this.deactivateVoteComposer = this.deactivateVoteComposer.bind(this);
    this.addVote = this.addVote.bind(this);
  }

  componentDidMount() {
    fetchJson('/api/votes').then(allVotes => {
      this.setState({
        allVotes
      });
    });
  }
  addVote(newVote) {
    sendJson('post', '/api/votes', newVote)
    .then(receivedVote => {
      this.setState({
        allVotes: [...this.state.allVotes, receivedVote]
      });
    });
  }

  setCurrentVote(vote) {
    const { composerActive } = this.state;
    this.setState(
      {currentVoteId: vote && !composerActive ? vote.id : null});
  }

  registerChoice(vote, choice) {
    const newVote = {
      ...vote,
      choices: vote.choice.map(
        (c) => c.id !== choice.id ? c : { ...c, count: c.count+1}
      )
    };
    return newVote;
  }

  registerVote(vote, choice) {
    const { allVotes } = this.state;
    const newVotes = allVotes.map(
      (v) => v.id !== vote.id ? v : this.registerChoice(v, choice)
    );
    this.setState({
      allVotes: newVotes
    });
  }

  activateVoteComposer() {
    this.setState({
      currentVoteId: null,
      composerActive: true
    });
  }

  deactivateVoteComposer() {
    this.setState({
      composerActive: false
    });
  }

  render() {
    const { allVotes, currentVoteId, composerActive } = this.state;

    return (
      <div>
        <VoteList allVotes={allVotes}
          currentVoteId={currentVoteId}
          onSelectVote={this.setCurrentVote}
          onDismissVote={() => {this.setCurrentVote(null)}}
          onRegisterVote={this.registerVote}
          />
        <VoteComposer active={composerActive}
            onDeactivate={this.deactivateVoteComposer}
            onActivate={this.activateVoteComposer}
            onSave={this.addVote}
          />
      </div>
    );
  }
}
