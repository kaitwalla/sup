# Avatar pipeline

## Initial approach

Use Character Studio as the external avatar creator, export VRM 1.0, then import into Unity with UniVRM.

Character Studio: https://github.com/M3-org/CharacterStudio

UniVRM: https://github.com/vrm-c/UniVRM

This keeps avatar customization independent from the multiplayer/world client and avoids relying on a hosted avatar vendor.

## POC needs only

- 2-3 body presets
- several hairstyles
- several tops
- a small color palette
- glasses / hat optional

The important tests are readability at diorama-camera distance, clean humanoid animation, and acceptable cost with several simultaneous WebGL avatars.

## Later

If the stock Character Studio parts are not cute enough, retain the same VRM pipeline and replace the parts with a bespoke collection. The Unity world client should not care where the VRM was assembled.
