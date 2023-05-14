import React, { useState } from "react";
import axios from "axios";

const SalespersonForm = () => {
  const [formData, setFormData] = useState({
    salespersonId: 0,
    firstName: "",
    lastName: "",
    address: "",
    phone: "",
    startDate: "",
    terminationDate: "",
    manager: ""
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        "https://localhost:7243/Salesperson/updateSalesperson",
        formData
      );
      console.log(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        ID:
        <input
          type="number"
          name="salespersonId"
          value={formData.salespersonId}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        First Name:
        <input
          type="text"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        Last Name:
        <input
          type="text"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        Address:
        <input
          type="text"
          name="address"
          value={formData.address}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        Phone:
        <input
          type="text"
          name="phone"
          value={formData.phone}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        Start Date:
        <input
          type="text"
          name="startDate"
          value={formData.startDate}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        Termination Date:
        <input
          type="text"
          name="terminationDate"
          value={formData.terminationDate}
          onChange={handleChange}
        />
      </label>
      <br />
      <label>
        Manager:
        <input
          type="text"
          name="manager"
          value={formData.manager}
          onChange={handleChange}
        />
      </label>
      <br />
      <button type="submit">Submit</button>
    </form>
  );
};

export default SalespersonForm;