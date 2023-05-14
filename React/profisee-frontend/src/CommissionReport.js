import React, { useState } from 'react';

function CommissionReport() {
    const [data, setData] = useState([]);

    const loadData = async () => {
      const response = await fetch("https://localhost:7243/Sales/getSalesReport");
      const jsonData = await response.json();
      setData(jsonData);
    };
  
    return (
      <div>
        <button onClick={loadData}>Load data</button>
        <table>
          <thead>
            <tr>
              <th>Sales Person</th>
              <th>Commission</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.reportId}>
                <td>{item.salesperson}</td>
                <td>${item.commission}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
}

export default CommissionReport;