import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { itemsApi } from "../api/client";
import { StatusBadge, TypeBadge } from "../components/Badges";

export default function ItemDetailPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [item, setItem] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [busy, setBusy] = useState(false);

  useEffect(() => {
    setLoading(true);
    setError(null);
    itemsApi
      .get(id)
      .then(setItem)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [id]);

  async function handleMarkReturned() {
    setBusy(true);
    try {
      const updated = await itemsApi.update(id, { status: "RETURNED" });
      setItem(updated);
    } catch (err) {
      setError(err.message);
    } finally {
      setBusy(false);
    }
  }

  async function handleDelete() {
    if (!window.confirm("Delete this item? This cannot be undone.")) return;
    setBusy(true);
    try {
      await itemsApi.remove(id);
      navigate("/");
    } catch (err) {
      setError(err.message);
      setBusy(false);
    }
  }

  if (loading) {
    return <p className="mx-auto max-w-3xl px-4 py-10 text-center text-sm text-slate-500">Loading...</p>;
  }

  if (error && !item) {
    return (
      <div className="mx-auto max-w-3xl px-4 py-10">
        <div className="rounded-md border border-red-200 bg-red-50 p-4 text-sm text-red-700">{error}</div>
        <Link to="/" className="mt-4 inline-block text-sm text-indigo-600 hover:underline">
          ← Back to items
        </Link>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-3xl px-4 py-6">
      <Link to="/" className="text-sm text-indigo-600 hover:underline">
        ← Back to items
      </Link>

      <div className="mt-4 rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
        <div className="flex flex-wrap items-start justify-between gap-2">
          <h1 className="text-2xl font-bold text-slate-900">{item.title}</h1>
          <div className="flex gap-2">
            <TypeBadge type={item.type} />
            <StatusBadge status={item.status} />
          </div>
        </div>

        <p className="mt-4 whitespace-pre-wrap text-sm text-slate-700">{item.description}</p>

        <dl className="mt-6 grid grid-cols-1 gap-4 border-t border-slate-100 pt-4 text-sm sm:grid-cols-2">
          <div>
            <dt className="font-medium text-slate-500">Category</dt>
            <dd className="text-slate-900">{item.category}</dd>
          </div>
          <div>
            <dt className="font-medium text-slate-500">Location</dt>
            <dd className="text-slate-900">{item.location}</dd>
          </div>
          <div>
            <dt className="font-medium text-slate-500">Date Reported</dt>
            <dd className="text-slate-900">{item.dateReported}</dd>
          </div>
          <div>
            <dt className="font-medium text-slate-500">Contact</dt>
            <dd className="text-slate-900">
              {item.contactName} &middot;{" "}
              <a href={`mailto:${item.contactEmail}`} className="text-indigo-600 hover:underline">
                {item.contactEmail}
              </a>
            </dd>
          </div>
        </dl>

        {error && (
          <div className="mt-4 rounded-md border border-red-200 bg-red-50 p-3 text-sm text-red-700">
            {error}
          </div>
        )}

        <div className="mt-6 flex flex-wrap gap-2 border-t border-slate-100 pt-4">
          {item.status === "ACTIVE" && (
            <button
              onClick={handleMarkReturned}
              disabled={busy}
              className="rounded-md bg-emerald-600 px-4 py-2 text-sm font-semibold text-white hover:bg-emerald-700 disabled:opacity-60"
            >
              Mark as Returned
            </button>
          )}
          <Link
            to={`/items/${item.id}/edit`}
            className="rounded-md border border-slate-300 px-4 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
          >
            Edit
          </Link>
          <button
            onClick={handleDelete}
            disabled={busy}
            className="rounded-md border border-red-300 px-4 py-2 text-sm font-semibold text-red-600 hover:bg-red-50 disabled:opacity-60"
          >
            Delete
          </button>
        </div>
      </div>
    </div>
  );
}
