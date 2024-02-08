import React from 'react';
import { PortfolioStatistics } from './PortfolioStatistics';

export const PortfolioChange = ({ oldValue, newValue }) => {
    const calculatePercentageChange = (oldValue, newValue) => {
        if (oldValue === 0) {
            return 0;
        }

        return ((newValue - oldValue) / oldValue) * 100;
    };

    const change = calculatePercentageChange(oldValue, newValue);
    const arrow = change > 0 ? '\u2191' : change < 0 ? '\u2193' : '';

    return (
        <PortfolioStatistics title="Change(%)" value={`${change.toFixed(2)}   ${arrow}`} />
    );
};