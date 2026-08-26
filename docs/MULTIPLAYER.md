# Multiplayer and proximity voice trial

## Candidate: Ubiq

Ubiq is the first networking/voice candidate because it already provides rooms, WebSocket/browser support, avatar patterns, and voice without requiring a hosted per-seat service.

The package is pinned in `Packages/manifest.json`.

## Run a room server

For local testing:

```bash
npx @ucl-vr/ubiq-server
```

For browser clients, expose the WebSocket endpoint through TLS / secure WebSockets. Ubiq's RoomClient can use TCP or WebSockets, but a deployed browser build must use the browser-compatible secure path.

## First integration pass

Do this in the Unity Editor after Package Manager finishes resolving Ubiq:

1. Open Package Manager -> Ubiq -> Samples.
2. Import the non-XR / desktop-capable demo and local-loopback samples.
3. Verify two Ubiq peers can join one room before touching the cozy scene.
4. Verify Ubiq voice between the sample peers.
5. Run the Ubiq browser/WebGL path against the same room server.
6. Only after voice works, replace the sample avatar visual/controller with the Cozy Clubhouse player visual and movement state.

This separation is deliberate. If microphone/WebRTC behavior is bad on iPhone Safari, we want to know whether the problem is Ubiq's browser voice path rather than our room code.

## Required device matrix

Test these combinations, with at least two clients joined simultaneously:

| Client | Render | Input | Mic | Voice receive | Lock/background recovery |
| --- | --- | --- | --- | --- | --- |
| macOS Chrome | | | | | n/a |
| macOS Safari | | | | | n/a |
| iPhone Safari | | | | | |
| Android Chrome | | | | | |

## Voice behavior we want

The UX must eventually be:

- entering the room makes you present, not automatically noisy
- microphone state is obvious
- nearby people are audible without joining a separate call
- distance attenuates audio
- focus areas can reduce or suppress incoming voice
- social areas can widen the useful voice radius

Do not build custom acoustic zones until plain mobile WebGL voice is proven.

## Escape hatch

If Ubiq's WebGL voice path is the weak link but Unity rendering/input performs well, retain Unity for the world and move voice into the browser shell using a WebRTC service/library. Avatar position can be bridged from Unity to JavaScript for spatial attenuation.

That is a valid success for the Unity trial. The trial is testing Unity as the experience layer, not Ubiq loyalty.
