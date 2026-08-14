import { Link } from "react-router-dom";
import { TypeBadge, StatusBadge } from "./Badges";

export default function ItemCard({ item }) {
  return (
    <Link
      to={`/items/${item.id}`}
      className="flex flex-col gap-2 rounded-lg border border-slate-200 bg-white p-4 shadow-sm transition-shadow hover:shadow-md"
    >
      <div className="flex items-center justify-between gap-2">
        <h3 className="truncate text-base font-semibold text-slate-900">{item.title}</h3>
        <div className="flex shrink-0 gap-1">
          <TypeBadge type={item.type} />
          <StatusBadge status={item.status} />
        </div>
      </div>
      <p className="line-clamp-2 text-sm text-slate-600">{item.description}</p>
      <div className="mt-1 flex flex-wrap gap-x-4 gap-y-1 text-xs text-slate-500">
        <span>📍 {item.location}</span>
        <span>🏷️ {item.category}</span>
        <span>📅 {item.dateReported}</span>
      </div>
    </Link>
  );
}
