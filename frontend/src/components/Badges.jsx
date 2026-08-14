export function TypeBadge({ type }) {
  const isLost = type === "LOST";
  return (
    <span
      className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-semibold ${
        isLost ? "bg-amber-100 text-amber-800" : "bg-emerald-100 text-emerald-800"
      }`}
    >
      {isLost ? "Lost" : "Found"}
    </span>
  );
}

export function StatusBadge({ status }) {
  const isActive = status === "ACTIVE";
  return (
    <span
      className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-semibold ${
        isActive ? "bg-sky-100 text-sky-800" : "bg-slate-200 text-slate-600"
      }`}
    >
      {isActive ? "Active" : "Returned"}
    </span>
  );
}
