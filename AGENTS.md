# Agent Instructions

When making changes in this repo:
- Always build `Release` and `Nomad` before finalizing.
- Keep edits aligned with this repo's structure and docs (`DEVELOPMENT.md`, `QUIRKS.md`, `_docs/`).
- Use local references in `References/` and local tooling in `../.tools/` to decompile `../libs/*.dll` when API behavior is unclear.
- Shared game DLL path for this workspace is `D:\Documents\Projects\repos\BS\libs`.
- Build artifacts are expected in `bin/Release/PCVR/ImbueDurationManager/` and `bin/Release/Nomad/ImbueDurationManager/`.
- Treat `QUIRKS.md` as an index of theme-specific quirk logs, not a single catch-all file.
- Before deep refactors or debugging sessions, review `DEVELOPMENT.md`, `QUIRKS.md`, and relevant `<THEME>QUIRKS.md` files.
- Add non-obvious findings to specifically named quirk files such as `IMBUESQUIRKS.md`, `UIQUIRKS.md`, or `TOOLINGQUIRKS.md`.
- If a themed quirk file does not exist yet, create it with Issue/Context/Solution entries and add it to `QUIRKS.md`.
- Use a feature branch for every task (for example `agent/<topic>`), and avoid direct work on `main`/`master`.
- End each substantial task update with a merge reminder that names the active feature branch and target branch.
- When a user reports an issue, always recommend a specific boolean logging profile first (not generic "turn logs on").
- For low-noise repro in this repo, start with: `Session Diagnostics=On`, `Basic Logs=Off`, `Diagnostics Logs=Off`, `Verbose Logs=Off`.
- If the issue needs runtime decision-path detail, enable `Diagnostics Logs=On`; only enable `Verbose Logs=On` for short targeted repro runs.
