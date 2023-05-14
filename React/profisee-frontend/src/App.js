
import './App.css';
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link
}
from 'react-router-dom';
import React from 'react';
import Products from './Products';
import Salesperson from './Salesperson';
import Customers from './customers';
import Sales from './sales';
import ProductUpdate from './ProductUpdate';
import SalespersonUpdate from './SalespersonUpdate';
import AddSale from './AddSale';
import CommissionReport from './CommissionReport';

function App() {
  return (
    <Router>
      <ul>
        <li>
          <Link to="/">Home</Link>
        </li>
        <li>
          <Link to="/products">Products</Link>
        </li>
        <li>
          <Link to="/productupdate">Product Update</Link>
        </li>
        <li>
          <Link to="/salespeople">Sales People</Link>
        </li>
        <li>
          <Link to="/salespersonUpdate">Salesperson Update</Link>
        </li>
        <li>
          <Link to="/customers">Customers</Link>
        </li>
        <li>
          <Link to="/sales">Sales</Link>
        </li>
        <li>
          <Link to="/addSale">Add Sale</Link>
        </li>
        <li>
          <Link to="/commissionReport">Commission Report</Link>
        </li>
      </ul>
      
      <Routes>
        <Route path="/products" element={<Products />} />
        <Route path="/productupdate" element={<ProductUpdate />} />
        <Route path="/salespeople" element={<Salesperson />} />
        <Route path="/salespersonUpdate" element={<SalespersonUpdate />} />
        <Route path="/customers" element={<Customers />} />
        <Route path="/sales" element={<Sales />} />
        <Route path="/addSale" element={<AddSale />} />
        <Route path="/commissionReport" element={<CommissionReport />} />
      </Routes>
    </Router>
  );
}

export default App;
