# Campus Lost & Found — Frontend

React + Vite single-page app that consumes the [backend](../backend) REST API. Students can
browse/search lost & found reports, view a single item, report a new item, edit an item, and
mark an item as returned.

## Tech Stack

- React 19 + Vite
- React Router (client-side routing)
- Tailwind CSS v4 (`@tailwindcss/vite` plugin, zero-config)

## Configuring the API URL

The frontend reads the backend's base URL from the `VITE_API_URL` environment variable.

```bash
cp .env.example .env
```

By default (`.env` absent) it targets `http://localhost:5158/api`, matching the backend's
default `dotnet run` port. Change `.env` if your backend runs elsewhere.

## Running Locally

```bash
cd frontend
npm install
npm run dev
```

Opens on `http://localhost:5173` by default — already in the backend's allowed CORS origins.
Make sure the backend is running first (`cd backend && dotnet run`).

## Project Structure

```
src/
  api/client.js        fetch wrapper + itemsApi (list/get/create/update/remove)
  components/
    Navbar.jsx
    ItemCard.jsx        item summary card used in the browse grid
    Badges.jsx          TypeBadge (Lost/Found), StatusBadge (Active/Returned)
    FilterBar.jsx       search + type/status/category filters
    ItemForm.jsx         shared form for report & edit pages
  pages/
    ItemsListPage.jsx    browse/search/filter (/)
    ItemDetailPage.jsx   view one item, mark returned, edit, delete (/items/:id)
    ReportItemPage.jsx   create an item (/report)
    EditItemPage.jsx     edit an item (/items/:id/edit)
    NotFoundPage.jsx
```

## Build

```bash
npm run build
```
