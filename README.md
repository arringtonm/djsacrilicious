# DJ Sacrilicious

Source for [djsacrilicious.com](https://djsacrilicious.com): a single static page, hosted on Cloudflare.

- `public/index.html` — the page
- `public/css/styles.css` — hand-written, mobile-first CSS (no build step)
- `public/img/` — compressed WebP images

## Preview locally

    python3 -m http.server 8000 -d public

## Deploy

    npx wrangler pages deploy public --project-name <cloudflare-project-name>

The original 2017 version of this repo (C# admin backend, Sass sources) is in the git history before the `mobile-refresh` branch.

## Changing the CSS

Cloudflare tells browsers to keep `styles.css` for 4 hours. After editing it, bump the `?v=` number on the stylesheet link in `public/index.html` so visitors get the new file straight away.
