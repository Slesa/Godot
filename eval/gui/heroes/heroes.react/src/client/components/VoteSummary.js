import React from 'react';

import { Link } from 'react-router';

export default function VoteSummary({ vote }) {
  const totalVotes = vote.choices.reduce((prev, curr) => prev + curr.count, 0);

  return (
    <div className="Row VotesRow Selectable">
      <Link to={`/votes/${vote.id}`}>
        <h1 className="Title">{vote.title}
          <div className="Badge">{totalVotes} Votes</div>
        </h1>

        <p className="Emphasis">{vote.description}</p>
      </Link>
    </div>
  );
}

VoteSummary.propTypes = {
  vote:       React.PropTypes.object.isRequired
};
