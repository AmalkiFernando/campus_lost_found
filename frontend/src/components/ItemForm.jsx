import { useState } from "react";

const inputClasses =
  "w-full rounded-md border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 focus:border-indigo-500 focus:outline-none focus:ring-1 focus:ring-indigo-500";
const labelClasses = "mb-1 block text-sm font-medium text-slate-700";

function fieldError(fieldErrors, name) {
  if (!fieldErrors) return null;
  const key = Object.keys(fieldErrors).find((k) => k.toLowerCase() === name.toLowerCase());
  return key ? fieldErrors[key][0] : null;
}

export default function ItemForm({ initialValues, onSubmit, submitLabel, showStatus, fieldErrors }) {
  const [values, setValues] = useState(initialValues);
  const [submitting, setSubmitting] = useState(false);

  function update(key, value) {
    setValues((prev) => ({ ...prev, [key]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setSubmitting(true);
    try {
      await onSubmit(values);
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="flex flex-col gap-4">
      <div>
        <label className={labelClasses} htmlFor="title">
          Title
        </label>
        <input
          id="title"
          type="text"
          required
          value={values.title}
          onChange={(e) => update("title", e.target.value)}
          className={inputClasses}
          placeholder="e.g. Black Wallet"
        />
        {fieldError(fieldErrors, "title") && (
          <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "title")}</p>
        )}
      </div>

      <div>
        <label className={labelClasses} htmlFor="description">
          Description
        </label>
        <textarea
          id="description"
          required
          rows={3}
          value={values.description}
          onChange={(e) => update("description", e.target.value)}
          className={inputClasses}
          placeholder="Distinguishing details that help someone identify the item"
        />
        {fieldError(fieldErrors, "description") && (
          <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "description")}</p>
        )}
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
        <div>
          <label className={labelClasses} htmlFor="category">
            Category
          </label>
          <input
            id="category"
            type="text"
            required
            value={values.category}
            onChange={(e) => update("category", e.target.value)}
            className={inputClasses}
            placeholder="e.g. Electronics"
          />
          {fieldError(fieldErrors, "category") && (
            <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "category")}</p>
          )}
        </div>

        <div>
          <label className={labelClasses} htmlFor="location">
            Location
          </label>
          <input
            id="location"
            type="text"
            required
            value={values.location}
            onChange={(e) => update("location", e.target.value)}
            className={inputClasses}
            placeholder="e.g. Library"
          />
          {fieldError(fieldErrors, "location") && (
            <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "location")}</p>
          )}
        </div>

        <div>
          <label className={labelClasses} htmlFor="dateReported">
            Date Reported
          </label>
          <input
            id="dateReported"
            type="date"
            required
            value={values.dateReported}
            onChange={(e) => update("dateReported", e.target.value)}
            className={inputClasses}
          />
          {fieldError(fieldErrors, "dateReported") && (
            <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "dateReported")}</p>
          )}
        </div>

        <div>
          <label className={labelClasses} htmlFor="type">
            Type
          </label>
          <select
            id="type"
            required
            value={values.type}
            onChange={(e) => update("type", e.target.value)}
            className={inputClasses}
          >
            <option value="LOST">Lost</option>
            <option value="FOUND">Found</option>
          </select>
        </div>

        {showStatus && (
          <div>
            <label className={labelClasses} htmlFor="status">
              Status
            </label>
            <select
              id="status"
              required
              value={values.status}
              onChange={(e) => update("status", e.target.value)}
              className={inputClasses}
            >
              <option value="ACTIVE">Active</option>
              <option value="RETURNED">Returned</option>
            </select>
          </div>
        )}
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
        <div>
          <label className={labelClasses} htmlFor="contactName">
            Contact Name
          </label>
          <input
            id="contactName"
            type="text"
            required
            value={values.contactName}
            onChange={(e) => update("contactName", e.target.value)}
            className={inputClasses}
            placeholder="Your name"
          />
          {fieldError(fieldErrors, "contactName") && (
            <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "contactName")}</p>
          )}
        </div>

        <div>
          <label className={labelClasses} htmlFor="contactEmail">
            Contact Email
          </label>
          <input
            id="contactEmail"
            type="email"
            required
            value={values.contactEmail}
            onChange={(e) => update("contactEmail", e.target.value)}
            className={inputClasses}
            placeholder="you@example.edu"
          />
          {fieldError(fieldErrors, "contactEmail") && (
            <p className="mt-1 text-xs text-red-600">{fieldError(fieldErrors, "contactEmail")}</p>
          )}
        </div>
      </div>

      <button
        type="submit"
        disabled={submitting}
        className="mt-2 rounded-md bg-indigo-600 px-4 py-2 text-sm font-semibold text-white transition-colors hover:bg-indigo-700 disabled:cursor-not-allowed disabled:opacity-60"
      >
        {submitting ? "Saving..." : submitLabel}
      </button>
    </form>
  );
}
