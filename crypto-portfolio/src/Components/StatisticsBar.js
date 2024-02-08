import React from "react";
import { PortfolioStatistics } from './PortfolioStatistics';
import { PortfolioChange } from './PortfolioChangePercentages';
import '../App.css';

export const StatisticsBar = ({ currentBalance, initialBalance }) => {
  return (
    <div className="statistics-bar">
      <PortfolioStatistics title={"Initial balance($)"} value={initialBalance.toFixed(4)}></PortfolioStatistics>
      <PortfolioStatistics title={"Current balance($)"} value={currentBalance.toFixed(4)}></PortfolioStatistics>
      <PortfolioStatistics title={"Change($)"} value={(currentBalance - initialBalance).toFixed(4)}></PortfolioStatistics>
      <PortfolioChange oldValue={initialBalance} newValue={currentBalance}></PortfolioChange>
    </div>
  );
};