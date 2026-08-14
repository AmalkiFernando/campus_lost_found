import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { itemsApi } from "../api/client";
import ItemForm from "../components/ItemForm";

const EMPTY_ITEM = {
  title: "",
  description: "",
  category: "",
  location: "",
  dateReported: new Date().toISOString().slice(0, 10),
  type: "LOST",
  contactName: "",
  contactEmail: "",
};

export default function ReportItemPage() {
  const navigate = useNavigate();
  const [error, setError] = useState(null);
  const [fieldErrors, setFieldErrors] = useState(null);

  async function handleSubmit(values) {
    setError(null);
    setFieldErrors(null);
    try {
      const created = await itemsApi.create(values);
      navigate(`/items/${created.id}`);
    } catch (err) {
      setError(err.message);
      setFieldErrors(err.fieldErrors);
    }
  }

  return (
    <div className="mx-auto max-w-2xl px-4 py-6">
      <h1 className="text-2xl font-bold text-slate-900">Report an Item</h1>
      <p className="mt-1 text-sm text-slate-600">
        Report something you lost, or something you found around campus.
      </p>

      {error && (
        <div className="mt-4 rounded-md border border-red-200 bg-red-50 p-3 text-sm text-red-700">
          {error}
        </div>
      )}

      <div className="mt-4 rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
        <ItemForm
          initialValues={EMPTY_ITEM}
          onSubmit={handleSubmit}
          submitLabel="Submit Report"
          showStatus={false}
          fieldErrors={fieldErrors}
        />
      </div>
    </div>
  );
}
