import React, { useState } from 'react';

function Customers() {
    const [data, setData] = useState([]);

    const loadData = async () => {
      const response = await fetch("https://localhost:7243/Customer");
      const jsonData = await response.json();
      setData(jsonData);
    };
  
    return (
      <div>
        <button onClick={loadData}>Load data</button>
        <table>
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Address</th>
              <th>Phone Number</th>
              <th>Start Date</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.customerId}>
                <td>{item.firstName}</td>
                <td>{item.lastName}</td>
                <td>{item.address}</td>
                <td>{item.phone}</td>
                <td>{item.startDate}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
}

export default Customers;