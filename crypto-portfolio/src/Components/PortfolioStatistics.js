import React from 'react';
import '../App.css';

export const PortfolioStatistics = ({ title, value }) => {
  return (
    <div className="portfolio-statistics">
      <h3>{title}</h3>
      <p>{value}</p>
    </div>
  );
};
