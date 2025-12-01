import { useState } from "react";
import { uploadFile } from "../../api/api";
import "../../css/App.css";

function App() {
  const [selectedFile, setSelectedFile] = useState(null);
  const [previewUrl, setPreviewUrl] = useState(null);
  const [status, setStatus] = useState("");

  const handleFileChange = (e) => {
    const file = e.target.files?.[0] || null;
    setSelectedFile(file);
    setStatus("");

    if (file) {
      setPreviewUrl(URL.createObjectURL(file));
    } else {
      setPreviewUrl(null);
    }
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
    <div className="app-container">
      <h1>Upload an Image</h1>

      <div className="card">
        <input type="file" accept="image/*" onChange={handleFileChange} />

        {previewUrl && (
          <img
            src={previewUrl}
            alt="Preview"
            style={{
              marginTop: "20px",
              maxWidth: "300px",
              borderRadius: "8px",
              boxShadow: "0 0 8px rgba(0,0,0,0.2)",
            }}
          />
        )}

        <button style={{ marginTop: "10px" }} onClick={handleSubmit}>
          Submit
        </button>

        {status && <p style={{ marginTop: "10px" }}>{status}</p>}
      </div>

      <p className="read-the-docs">Select a file and click submit to upload.</p>
    </div>
  );
}

export default App;
