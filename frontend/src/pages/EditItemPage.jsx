import { useEffect, useState } from "react";
import { useNavigate, useParams, Link } from "react-router-dom";
import { itemsApi } from "../api/client";
import ItemForm from "../components/ItemForm";

export default function EditItemPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [initialValues, setInitialValues] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [fieldErrors, setFieldErrors] = useState(null);

  useEffect(() => {
    itemsApi
      .get(id)
      .then((item) =>
        setInitialValues({
          title: item.title,
          description: item.description,
          category: item.category,
          location: item.location,
          dateReported: item.dateReported,
          type: item.type,
          status: item.status,
          contactName: item.contactName,
          contactEmail: item.contactEmail,
        })
      )
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [id]);

  async function handleSubmit(values) {
    setError(null);
    setFieldErrors(null);
    try {
      await itemsApi.update(id, values);
      navigate(`/items/${id}`);
    } catch (err) {
      setError(err.message);
      setFieldErrors(err.fieldErrors);
    }
  }

  if (loading) {
    return <p className="mx-auto max-w-2xl px-4 py-10 text-center text-sm text-slate-500">Loading...</p>;
  }

  if (!initialValues) {
    return (
      <div className="mx-auto max-w-2xl px-4 py-10">
        <div className="rounded-md border border-red-200 bg-red-50 p-4 text-sm text-red-700">
          {error || "Item not found."}
        </div>
        <Link to="/" className="mt-4 inline-block text-sm text-indigo-600 hover:underline">
          ← Back to items
        </Link>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-2xl px-4 py-6">
      <h1 className="text-2xl font-bold text-slate-900">Edit Item</h1>

      {error && (
        <div className="mt-4 rounded-md border border-red-200 bg-red-50 p-3 text-sm text-red-700">
          {error}
        </div>
      )}

      <div className="mt-4 rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
        <ItemForm
          initialValues={initialValues}
          onSubmit={handleSubmit}
          submitLabel="Save Changes"
          showStatus
          fieldErrors={fieldErrors}
        />
      </div>
    </div>
  );
}
