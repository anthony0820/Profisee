import React, { useState } from 'react';

const SaleForm = () => {
  const [formData, setFormData] = useState({
    product: '',
    customer: '',
    salesperson: ''
  });

  const handleChange = (event) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const response = await fetch('https://localhost:7243/Sales/addReadableSale', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData)
      });

      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      // Handle success
      console.log('Form data successfully submitted:', formData);

    } catch (error) {
      // Handle error
      console.error('Error submitting form data:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Product:
        <input type="text" name="product" value={formData.product} onChange={handleChange} />
      </label>

      <label>
        Customer:
        <input type="text" name="customer" value={formData.customer} onChange={handleChange} />
      </label>

      <label>
        Salesperson:
        <input type="text" name="salesperson" value={formData.salesperson} onChange={handleChange} />
      </label>

      <button type="submit">Submit</button>
    </form>
  );
};

export default SaleForm;