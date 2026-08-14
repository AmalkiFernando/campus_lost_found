import { NavLink } from "react-router-dom";

const linkClasses = ({ isActive }) =>
  `rounded-md px-3 py-2 text-sm font-medium transition-colors ${
    isActive ? "bg-indigo-600 text-white" : "text-slate-600 hover:bg-slate-100"
  }`;

export default function Navbar() {
  return (
    <header className="border-b border-slate-200 bg-white">
      <nav className="mx-auto flex max-w-5xl items-center justify-between px-4 py-3">
        <NavLink to="/" className="flex items-center gap-2 text-lg font-bold text-slate-900">
          <span className="text-xl">🎒</span>
          Campus Lost &amp; Found
        </NavLink>
        <div className="flex items-center gap-2">
          <NavLink to="/" end className={linkClasses}>
            Browse
          </NavLink>
          <NavLink to="/report" className={linkClasses}>
            Report an Item
          </NavLink>
        </div>
      </nav>
    </header>
  );
}
