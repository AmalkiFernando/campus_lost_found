import { Link } from "react-router-dom";

export default function NotFoundPage() {
  return (
    <div className="mx-auto max-w-2xl px-4 py-16 text-center">
      <h1 className="text-2xl font-bold text-slate-900">Page not found</h1>
      <p className="mt-2 text-sm text-slate-600">The page you're looking for doesn't exist.</p>
      <Link to="/" className="mt-4 inline-block text-sm text-indigo-600 hover:underline">
        ← Back to items
      </Link>
    </div>
  );
}
