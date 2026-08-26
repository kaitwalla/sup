# sup

Unity/WebGL proof of concept for a cozy, drop-in friend space with lightweight multiplayer and proximity voice.

## Goal

Answer one question before we build anything larger:

> Can a cozy Unity 6 WebGL room feel good on desktop and mobile browsers while multiplayer presence and proximity voice are running?

## First vertical slice

- fixed 3/4 diorama camera
- desktop WASD + click/tap-to-move
- couch interaction for social mode
- desk interaction for focus mode
- Ubiq as the initial multiplayer/voice candidate
- VRM avatars via Character Studio + UniVRM
- CC0 room art from KayKit / Tiny Treats

## Unity

Target **Unity 6.0 LTS**.

After opening the project, run:

`Cozy Clubhouse -> Build Prototype Scene`

This creates a primitive-only scene so we can validate controls before importing art.

## Free art

See `docs/ASSETS.md`.

Recommended initial packs:

- KayKit Furniture Bits, CC0
- Tiny Treats Homely House, CC0
- Tiny Treats House Plants, CC0

## Avatars

Ready Player Me shut down its public avatar platform in January 2026, so this project does not depend on it.

The initial pipeline is:

`Character Studio -> VRM 1.0 -> UniVRM -> Unity Humanoid Animator`

See `docs/AVATARS.md`.

## Multiplayer

The first experiment uses Ubiq for rooms, synchronization and voice. The architecture is intentionally replaceable if browser/mobile audio is poor.

See `docs/MULTIPLAYER.md`.

## Success criteria

- Mac/Windows browser feels smooth
- iPhone Safari controls are comfortable
- Android Chrome behaves similarly
- 3-6 people can share a room reliably
- proximity voice requires no explicit call join
- couch / desk interactions feel natural
- visuals can plausibly reach a Gogh / Spirit City-ish cozy aesthetic

## Explicitly out of scope for this POC

- login / Fluxer OAuth
- persistent furniture
- inventory
- pets
- shared music
- screen sharing
- production deployment
- moderation
