import { useEffect, useMemo, useState } from "react";
import { itemsApi } from "../api/client";
import FilterBar from "../components/FilterBar";
import ItemCard from "../components/ItemCard";

const DEFAULT_FILTERS = { search: "", type: "", status: "", category: "" };

export default function ItemsListPage() {
  const [filters, setFilters] = useState(DEFAULT_FILTERS);
  const [items, setItems] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    itemsApi
      .list()
      .then((all) => {
        const unique = [...new Set(all.map((item) => item.category))].sort();
        setCategories(unique);
      })
      .catch(() => {});
  }, []);

  useEffect(() => {
    const handle = setTimeout(() => {
      setLoading(true);
      setError(null);
      itemsApi
        .list(filters)
        .then(setItems)
        .catch((err) => setError(err.message))
        .finally(() => setLoading(false));
    }, 250);

    return () => clearTimeout(handle);
  }, [filters]);

  const hasActiveFilters = useMemo(
    () => Object.values(filters).some((v) => v !== ""),
    [filters]
  );

  return (
    <div className="mx-auto flex max-w-5xl flex-col gap-4 px-4 py-6">
      <div>
        <h1 className="text-2xl font-bold text-slate-900">Browse Lost &amp; Found Items</h1>
        <p className="text-sm text-slate-600">
          Search and filter reports from around campus.
        </p>
      </div>

      <FilterBar filters={filters} onChange={setFilters} categories={categories} />

      {error && (
        <div className="rounded-md border border-red-200 bg-red-50 p-3 text-sm text-red-700">
          {error}
        </div>
      )}

      {loading ? (
        <p className="py-10 text-center text-sm text-slate-500">Loading items...</p>
      ) : items.length === 0 ? (
        <div className="rounded-lg border border-dashed border-slate-300 py-14 text-center">
          <p className="text-sm text-slate-500">
            {hasActiveFilters ? "No items match your filters." : "No items reported yet."}
          </p>
        </div>
      ) : (
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
          {items.map((item) => (
            <ItemCard key={item.id} item={item} />
          ))}
        </div>
      )}
    </div>
  );
}
