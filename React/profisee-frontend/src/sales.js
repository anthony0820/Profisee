import React, { useState } from 'react';

function Sales() {
    const [data, setData] = useState([]);

    const loadData = async () => {
      const response = await fetch("https://localhost:7243/sales");
      const jsonData = await response.json();
      setData(jsonData);
    };
  
    return (
      <div>
        <button onClick={loadData}>Load data</button>
        <table>
          <thead>
            <tr>
              <th>Product</th>
              <th>Customer</th>
              <th>Sales Person</th>
              <th>Sales Date</th>
              <th>Sale Price</th>
              <th>Sale Commission</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.saleId}>
                <td>{item.product}</td>
                <td>{item.customer}</td>
                <td>{item.salesperson}</td>
                <td>{item.date}</td>
                <td>${item.price}</td>
                <td>${item.commission}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
}

export default Sales;