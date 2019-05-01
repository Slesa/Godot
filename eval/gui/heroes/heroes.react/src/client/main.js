import React from 'react';
import ReactDOM from 'react-dom';

import Layout from './components/Layout';

import VoteController from './components/VoteController'
import SingleVoteController from './components/SingleVoteController';
import VoteComposerController from './components/VoteComposerController'
import LoginController from './components/LoginController';
import NoMatch from './components/NoMatch';

import { Router, Route, Redirect } from 'react-router';

import createHashHistory from 'history/lib/createHashHistory';

const history = createHashHistory({queryKey: false});

// <Route path="compose" component={VoteComposerController} onEnter={LoginController.requireAuth} />
const router = <Router history={history}>
  <Redirect from="/" to="/votes" />
  <Route path='/' component={Layout}>
    <Route path="votes" component={VoteController} />
    <Route path="votes/:id" component={SingleVoteController} />
    <Route path="login(/:redirect)" component={LoginController} />
    <Route path="compose" component={VoteComposerController} />
    <Route path="*" component={NoMatch} />
  </Route>
</Router>

ReactDOM.render(
  router,
  //<Layout>{mainComponent}</Layout>,
  document.getElementById('voteAppMountPoint')
);
