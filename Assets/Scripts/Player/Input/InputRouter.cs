using UnityEngine;

public class InputRouter : MonoBehaviour
{
    private Game _game;
    private Player _player;

    public KeyboardInput keyboard;
    //public JoystickInput joystick;// TODO
    //public TapToMoveInput tapToMove;// TODO
    //public GamepadInput gamepad;// TODO
    
    public Vector2 moveBuffer;
    public Vector2 lookBuffer;

    private IInputSource _activeSource;

    public void Init(Game game, Player player) {
        _game = game;
        _player = player;

        _activeSource = keyboard; // TODO: platform selection, from SDK
        _activeSource.Init(game, player, this);
    }

    public void ManualUpdate(float deltaTime) {
        _activeSource?.ManualUpdate(deltaTime);
    }

    public void ManualFixedUpdate() {
        _activeSource?.ManualFixedUpdate();
    }

    public void DropBuffers() {
        moveBuffer = Vector2.zero;
        lookBuffer = Vector2.zero;
    }
}

public interface IInputSource
{
    void Init(Game game, Player player, InputRouter router);
    void ManualUpdate(float deltaTime);
    void ManualFixedUpdate();
}