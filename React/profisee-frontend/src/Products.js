import React, { useState } from 'react';

function Products() {
    const [data, setData] = useState([]);

    const loadData = async () => {
      const response = await fetch("https://localhost:7243/Products");
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
              <th>Manufacturer</th>
              <th>Style</th>
              <th>Purchase Price</th>
              <th>Sale Price</th>
              <th>Quantity in Stock</th>
              <th>Commission Percentage</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.productId}>
                <td>{item.name}</td>
                <td>{item.manufacturer}</td>
                <td>{item.style}</td>
                <td>{item.purchasePrice}</td>
                <td>{item.salePrice}</td>
                <td>{item.qtyOnHand}</td>
                <td>{item.commissionPercentage}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
}

export default Products;