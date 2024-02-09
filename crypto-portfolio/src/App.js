import React, { useEffect, useState } from 'react';
import { FileUpload } from './Components/FileUpload';
import { Table } from './Components/Table';
import { StatisticsBar } from './Components/StatisticsBar';
import { RefreshButton } from './Components/RefreshButton';
import {API_URL, REFRESH_ENDPOINT, REFRESH_INTERVAL_IN_MINUTES } from './config';
import axios from "axios";

function App() {
  const [assets, setAssets] = useState({});
  const [initialBalance, setInitialBalance] = useState(0);
  const [currentBalance, setCurrentBalance] = useState(0);
  const [sort, setSort] = useState({});
  const [intervalValueInMinutes, setIntervalValueInMinutes] = useState(REFRESH_INTERVAL_IN_MINUTES);
  const [sortedIds, setSortedIds] = useState([]);

  // useEffect(() => {
  //   if (currentBalance > 0) {
  //     setTimeout(handleRefresh, intervalValueInMinutes * 60 * 1000);
  //   }
  // }, [currentBalance])

  useEffect(() => {
    if (!assets) {
      return;
    }

    let sortedKeys = Object.keys(assets).slice().sort((a, b) => {
      const columnA = sort.dataType === 'string'
        ? assets[a][sort.column].toString().toUpperCase() || ''
        : assets[a][sort.column];
      const columnB = sort.dataType === 'string'
        ? assets[b][sort.column].toString().toUpperCase() || ''
        : assets[b][sort.column];

      if (columnA < columnB) {
        return sort.order === 'asc' ? -1 : 1;
      }

      if (columnA > columnB) {
        return sort.order === 'asc' ? 1 : -1;
      }

      return 0;
    });

    setSortedIds(sortedKeys);
  }, [sort])

  useEffect(() => {
    var currentTotalBalance = Object.values(assets).reduce((accumulator, currentAsset) => accumulator + currentAsset.currentCoinPrice * currentAsset.amount, 0);
    setCurrentBalance(() => currentTotalBalance);
    setSortedIds(Object.keys(assets));
  }, [assets])

  useEffect(()=>{
    const interval = setInterval(() => {
      handleRefresh();
      console.log("handleRefresh");
    }, intervalValueInMinutes * 60 * 1000);

    return () => clearInterval(interval);
  },[intervalValueInMinutes])

  const handleRefresh = async () => {
    try {
      const res = await axios.get(`${API_URL}${REFRESH_ENDPOINT}${sortedIds}`, {
        headers: {
          'Accept': 'application/json',
        },
      });

      console.log("Refreshing");
      let data = res.data.currentTickerStatus;
      // let data = {
      //     "80":125,
      //     "90":25000,
      //     "518":1125,
      //     "45088":70,
      // };

      console.log("The time interval has passed");

      let currentTotalPrice = 0;

      setAssets(prevAssets => {
        const updatedAssets = { ...prevAssets };
        
        for (const key of Object.keys(data)) {
          updatedAssets[key].currentCoinPrice = data[key];
          console.log(updatedAssets[key].amount * data[key]);
          currentTotalPrice += updatedAssets[key].amount * data[key];
        }
        
        return updatedAssets;
      });
    } catch (ex) {
      console.log("ERR");
      console.log(ex);
    } finally {
      // setTimeout(handleRefresh, intervalValueInMinutes * 60 * 1000);
    }
  };

  return (
    <div style={{ position: 'fixed', width: '80vw', height: '90vh' }} className="App">
      <FileUpload
        handleAssetsFunc={setAssets}
        handleInitialBalanceFunc={setInitialBalance}
        handleCurrentBalanceFunc={setCurrentBalance} />
      {Object.keys(assets).length > 0 &&
        <>
          <StatisticsBar currentBalance={currentBalance} initialBalance={initialBalance} />
          <RefreshButton handleRefreshFunc={handleRefresh} />
          <span className="description">frequency(min)</span>
          <input
            className='refreshFrequeny'
            type="number"
            value={intervalValueInMinutes}
            onChange={(e) => setIntervalValueInMinutes(e.target.value)}
          />
          <Table
            assets={assets}
            sortedIds={sortedIds}
            sort={sort}
            handleSorting={setSort} />
        </>
      }
    </div>
  );
}

export default App;