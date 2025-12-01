const API_BASE_URL = import.meta.env.API_BASE_URL ?? "http://localhost:5111";

export async function uploadFile(file) {
  const formData = new FormData();
  formData.append("file", file);

  const response = await fetch(`${API_BASE_URL}/api/upload`, {
    method: "POST",
    body: formData,
  });

  if (!response.ok) {
    const text = await response.text();
    throw new Error(`Upload failed: ${response.status} ${text}`);
  }

  // Adjust depending on what your API returns
  const data = await response.json().catch(() => null);
  return data;
}
