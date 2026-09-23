using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour, IInputSource
{
    private Game _game;
    private Player _player;
    private InputRouter _router;

    public float sensetivity = 1;

    public void Init(Game game, Player player, InputRouter router) {
        _game = game;
        _player = player;
        _router = router;
    }

    public void ManualUpdate(float deltaTime) {
        float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float y = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);

        _router.moveBuffer += new Vector2(x, y);
        
        _router.lookBuffer += (sensetivity + 0.01f) * deltaTime * Mouse.current.delta.value;

        if (Keyboard.current.eKey.wasPressedThisFrame) _router.PressE();
    }

    public void ManualFixedUpdate() { }
}