import React from "react";

export const RefreshButton = ({ handleRefreshFunc }) => {
    return (
        <button onClick={handleRefreshFunc}>Refresh</button>
    );
};