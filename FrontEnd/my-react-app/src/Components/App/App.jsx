import { useState } from "react";
import "../../api/api.js";
import { uploadFile } from "../../api/api.js";

function App() {
  const [selectedFile, setSelectedFile] = useState(null);
  const [status, setStatus] = useState("");

  const handleFileChange = (e) => {
    const file = e.target.files?.[0] || null;
    setSelectedFile(file);
    setStatus("");
  };

  const handleSubmit = async () => {
    if (!selectedFile) {
      setStatus("Please choose a file first.");
      return;
    }

    try {
      setStatus("Uploading...");
      const result = await uploadFile(selectedFile);
      setStatus(`Upload successful ✅ ${JSON.stringify(result)}`);
    } catch (err) {
      console.error(err);
      setStatus("Error: " + err.message);
    }
  };

  return (
    <>
      <h1>Upload an Image</h1>

      <div className="card">
        <input type="file" onChange={handleFileChange} />

        <button style={{ marginTop: "10px" }} onClick={handleSubmit}>
          Submit
        </button>

        {status && <p style={{ marginTop: "10px" }}>{status}</p>}
      </div>

      <p className="read-the-docs">
        Select a file and click submit to upload.
      </p>
    </>
  );
}

export default App;
