const inputClasses =
  "rounded-md border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 focus:border-indigo-500 focus:outline-none focus:ring-1 focus:ring-indigo-500";

export default function FilterBar({ filters, onChange, categories }) {
  function update(key, value) {
    onChange({ ...filters, [key]: value });
  }

  return (
    <div className="flex flex-wrap gap-3 rounded-lg border border-slate-200 bg-white p-4">
      <input
        type="search"
        placeholder="Search title or description..."
        value={filters.search}
        onChange={(e) => update("search", e.target.value)}
        className={`${inputClasses} min-w-[220px] flex-1`}
      />
      <select
        value={filters.type}
        onChange={(e) => update("type", e.target.value)}
        className={inputClasses}
      >
        <option value="">All types</option>
        <option value="LOST">Lost</option>
        <option value="FOUND">Found</option>
      </select>
      <select
        value={filters.status}
        onChange={(e) => update("status", e.target.value)}
        className={inputClasses}
      >
        <option value="">All statuses</option>
        <option value="ACTIVE">Active</option>
        <option value="RETURNED">Returned</option>
      </select>
      <select
        value={filters.category}
        onChange={(e) => update("category", e.target.value)}
        className={inputClasses}
      >
        <option value="">All categories</option>
        {categories.map((category) => (
          <option key={category} value={category}>
            {category}
          </option>
        ))}
      </select>
    </div>
  );
}
