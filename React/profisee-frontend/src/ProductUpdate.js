import React, { useState } from "react";

function ProductForm() {
  const [formData, setFormData] = useState({
    productId: 0,
    name: "",
    manufacturer: "",
    style: "",
    purchasePrice: 0.0,
    salePrice: 0.0,
    qtyOnHand: 0,
    commissionPercentage: 0.0,
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    const response = await fetch("https://localhost:7243/Products/updateProduct", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    });

    if (response.ok) {
      // Success
    } else {
      // Error handling
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="productId">Product ID:</label>
        <input
          type="number"
          id="productId"
          name="productId"
          value={formData.productId}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="name">Name:</label>
        <input
          type="text"
          id="name"
          name="name"
          value={formData.name}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="manufacturer">Manufacturer:</label>
        <input
          type="text"
          id="manufacturer"
          name="manufacturer"
          value={formData.manufacturer}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="style">Style:</label>
        <input
          type="text"
          id="style"
          name="style"
          value={formData.style}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="purchasePrice">Purchase Price:</label>
        <input
          type="number"
          id="purchasePrice"
          name="purchasePrice"
          value={formData.purchasePrice}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="salePrice">Sale Price:</label>
        <input
          type="number"
          id="salePrice"
          name="salePrice"
          value={formData.salePrice}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="qtyOnHand">Quantity on Hand:</label>
        <input
          type="number"
          id="qtyOnHand"
          name="qtyOnHand"
          value={formData.qtyOnHand}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="commissionPercentage">Commission Percentage:</label>
        <input
          type="number"
          id="commissionPercentage"
          name="commissionPercentage"
          value={formData.commissionPercentage}
          onChange={handleInputChange}
        />
      </div>
      <button type="submit">Submit</button>
    </form>
  );
}

export default ProductForm;