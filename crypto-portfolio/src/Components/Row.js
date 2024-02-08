import React from 'react';
import '../App.css';

export const Row = ({ data }) => {
  return (
    <tr>
      <td className="table-cell">{data.ticker}</td>
      <td className="table-cell">{data.amount.toFixed(4)}</td>
      <td className="table-cell">{data.currentCoinPrice.toFixed(4)}</td>
      <td className="table-cell">{data.totalInitialPrice.toFixed(4)}</td>
      <td className="table-cell">{(data.amount * data.currentCoinPrice).toFixed(4)}</td>
      <td className="table-cell">{(data.amount * data.currentCoinPrice - data.totalInitialPrice).toFixed(4)}</td>
      <td className="table-cell">{(((data.amount * data.currentCoinPrice - data.totalInitialPrice) / data.totalInitialPrice) * 100).toFixed(2)}</td>
    </tr>
  );
};
