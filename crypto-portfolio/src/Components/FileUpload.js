import React, { useState } from "react";
import axios from "axios";
import "../App.css";

export const FileUpload = ({ handleAssetsFunc, handleInitialBalanceFunc, handleCurrentBalanceFunc }) => {
  const [file, setFile] = useState();
  const [fileName, setFileName] = useState();

  const saveFile = (e) => {
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].name);
  };

  const uploadFile = async (e) => {

    const formData = new FormData();
    formData.append("formFile", file);
    formData.append("fileName", fileName);

    try {
      const res = await axios.post("http://localhost:5000/portfolio/upload", formData);

      handleAssetsFunc(res.data.assets);
      handleInitialBalanceFunc(res.data.initialBalance);
      handleCurrentBalanceFunc(res.data.currentBalance);
    } catch (ex) {
      console.log(ex);
    }
  };

  return (
    <div className="input-container">
      <input className="input" type="file" onChange={saveFile} />
      <input className="input" type="button" value="Upload" onClick={uploadFile} />
    </div>
  );
};