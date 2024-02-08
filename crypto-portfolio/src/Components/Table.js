import React from 'react';
import { Row } from './Row';
import '../App.css';

export const Table = ({ assets, sort, handleSorting }) => {
  const handleSort = (column, dataType) => {
    if (sort.column === column) {
      handleSorting({ column, dataType, order: sort.order === 'asc' ? 'desc' : 'asc' });
    } else {
      handleSorting({ column, dataType, order: "asc" });
    }
  };

  return (
    <table className="table-container">
      <thead>
        <tr>
          <th className='sortable' onClick={() => handleSort('ticker', "string")}>Ticker</th>
          <th className='sortable' onClick={() => handleSort('amount', "number")}>Amount</th>
          <th className='sortable' onClick={() => handleSort('currentCoinPrice', "number")}>Current Coin Price($)</th>
          <th className='sortable' onClick={() => handleSort('totalInitialPrice', "number")}>Total Initial Price($)</th>
          <th onClick={() => handleSort('totalCurrentPrice', "number")}>Total Current Price($)</th>
          <th onClick={() => handleSort('totalPriceChange', "number")}>Price Change($)</th>
          <th onClick={() => handleSort('totalPriceChangePercentages', "number")}>Price Change(%)</th>
        </tr>
      </thead>
      <tbody>
        {Object.values(assets) && Object.values(assets).map((asset, index) =>
          (<Row key={index} data={asset} />)
        )}
      </tbody>
    </table>
  );
};
