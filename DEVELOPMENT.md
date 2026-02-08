# Development Notes

## Runtime Model
- The manager samples each active imbue's natural energy delta over time.
- It scales observed drain by configured multipliers and applies correction deltas.
- This approach preserves base-game mechanics while allowing faster/slower decay.

## Menu Model
- Preset options only batch-write collapsible fields.
- Runtime logic only reads collapsible fields.

## Safety
- Native `Imbue.infiniteImbue` is toggled only when global drain is explicitly set to infinite and the option is enabled.
- Tracking state resets cleanly on disable to avoid stale corrections.
