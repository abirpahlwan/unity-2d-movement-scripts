# Unity 2D Movement Scripts

Extracted C# Scripts from ShootingDux Unity 2D Movement Tutorial (Video ID: 9hTnlp9_wX8).

## Included Scripts:
- **PlayerController.cs**: Primary 2D physics movement controller using Rigidbody2D, raw directional input normalization, sprint speed scaling, and dash impulse coroutines.
- **PlayerMovement.cs**: Modular 2D movement component featuring configurable acceleration curves, top-down 8-way movement, and side-scroller jump physics.
- **CameraController.cs**: Smooth 2D target tracking camera script using Vector3.SmoothDamp in LateUpdate with optional boundary constraints.
- **PlayerInputHandler.cs**: Decoupled input wrapper exposing clean getters for movement vector, sprint state, and dash triggers.
- **ItemCollector.cs**: 2D Trigger collision script detecting collectibles, coins, and temporary speed boost power-ups.
- **GameManager.cs**: Persistent Singleton GameManager controlling game state, score tracking, scene reloading, and pause states.