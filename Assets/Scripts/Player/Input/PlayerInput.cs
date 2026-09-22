using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Game _game;
    private Player _player;

    public float mobileRotationSensetivity = 0.5f;
    public float maxFirstMoveDelta = 50f;
    public float deadZoneThreshold = 10f;
    private int _jumpFingerId = -1;
    private int _movementFingerId = -1;

    public float sensetivity = 1;

    public bool fireIsPressed = false;
    public bool fireWasPressedThisFrame = false;
    public bool jumpIsPressed = false;
    public bool focusIsPressed;
    public bool runButtonIsPressed;
    public bool runButtonWasPressedThisFrame;

    public bool ability1IsPressed;
    public bool ability2IsPressed;
    public bool ability3IsPressed;

    public Vector2 moveBuffer;
    public Vector2 lookBuffer;
    private bool wasShiftingLastFrame = false;

    private Dictionary<int, bool> _rotationTouchInitialized = new Dictionary<int, bool>();
    private Dictionary<int, Vector2> _initialTouchPositions = new Dictionary<int, Vector2>();
    private bool _isMobile = false;

    public void Init(Game game, Player player) {
        _game = game;
        _player = player;
    }

    public void AsyncInit() {
        _isMobile = false;// _game.Root.sdkAdapter.IsMobile();
        //_game.Root.sdkAdapter.LoadFloat("sens", out sensetivity); 
        if (sensetivity == 0) sensetivity = 1;
    }

    public void ManualUpdate(float deltaTime) {
        //if (_game && _game.session.isOutOfGameplay) return;
        if (_isMobile)
            MobileInputUpdate(deltaTime);
        else
            DesktopInputUpdate(deltaTime);
    }

    public void SwitchFocusMobile() {
        focusIsPressed = !focusIsPressed;
    }

    public void DeathReset() {
        focusIsPressed = false;
    }

    private void DesktopInputUpdate(float deltaTime) {
        /*if (Keyboard.current.tabKey.wasPressedThisFrame) 
            _game.Root.menuBoot.ui.hud.SettingsButtonPressDesktop();*/

        /*if (_game.ui.hud.settingsBlockInput) {            
            DropBuffers();
            return;
        }*/
        moveBuffer += new Vector2(
               Keyboard.current.dKey.IsPressed() ? 1 : 0 + (Keyboard.current.aKey.IsPressed() ? -1 : 0),
               Keyboard.current.wKey.IsPressed() ? 1 : 0 + (Keyboard.current.sKey.IsPressed() ? -1 : 0)
               ) * deltaTime;

        lookBuffer += (sensetivity + 0.01f) * deltaTime * Mouse.current.delta.value;

        fireIsPressed = Mouse.current.leftButton.isPressed;
        fireWasPressedThisFrame = Mouse.current.leftButton.wasPressedThisFrame;
        jumpIsPressed = Keyboard.current.spaceKey.isPressed;
        focusIsPressed = Mouse.current.rightButton.isPressed;
        runButtonIsPressed = Keyboard.current.shiftKey.isPressed;
        runButtonWasPressedThisFrame = Keyboard.current.shiftKey.wasPressedThisFrame;

        ability1IsPressed = Keyboard.current.digit1Key.isPressed;
        ability2IsPressed = Keyboard.current.digit2Key.isPressed;
        ability3IsPressed = Keyboard.current.digit3Key.isPressed;
    }

    public void DropBuffers() {
        lookBuffer = Vector2.zero;
        moveBuffer = Vector2.zero;
    }

    private void MobileInputUpdate(float deltaTime) {
        /*if (_game.ui.hud.settingsBlockInput) {            
            DropBuffers();
            return;
        }*/
        ResetFrameInputs();
        UpdateMovement(deltaTime);
        ProcessTouches(deltaTime);
    }

    private void ResetFrameInputs() {
        fireIsPressed = false;
        fireWasPressedThisFrame = false;
        runButtonWasPressedThisFrame = false;
        jumpIsPressed = false; 
    }

    private void UpdateMovement(float deltaTime) {
        /*moveBuffer += _game.ui.hud.movementJoystick.Direction * deltaTime;
        bool isShifting = _game.ui.hud.movementJoystick.Direction.magnitude >= 0.5f;
        runButtonIsPressed = isShifting;
        wasShiftingLastFrame = isShifting;*/
    }

    private void ProcessTouches(float deltaTime) {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == UnityEngine.TouchPhase.Began) {
                HandleTouchBegan(touch);
            } else if (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled) {
                HandleTouchEnded(touch);
            } else {
                HandleOtherTouchPhases(touch, deltaTime);
            }
        }
        runButtonWasPressedThisFrame = !fireIsPressed;   
    }

    private void HandleTouchBegan(Touch touch) { /*
        if (RectTransformUtility.RectangleContainsScreenPoint(_game.ui.hud.mobileJumpButton.transform as RectTransform, touch.position)) {
            _jumpFingerId = touch.fingerId;
            jumpIsPressed = true;
            return;
        }
         
        if (RectTransformUtility.RectangleContainsScreenPoint(_game.ui.hud.movementJoystick.transform as RectTransform, touch.position)) {
            _movementFingerId = touch.fingerId;
            return;
        }
         */
        fireWasPressedThisFrame = true;
        _rotationTouchInitialized[touch.fingerId] = false;
        _initialTouchPositions[touch.fingerId] = touch.position;
    }

    private void HandleTouchEnded(Touch touch) {
        if (touch.fingerId == _jumpFingerId)
            _jumpFingerId = -1;

        if (touch.fingerId == _movementFingerId)
            _movementFingerId = -1;

        if (_rotationTouchInitialized.ContainsKey(touch.fingerId))
            _rotationTouchInitialized.Remove(touch.fingerId);

        if (_initialTouchPositions.ContainsKey(touch.fingerId))
            _initialTouchPositions.Remove(touch.fingerId);
    }

    private void HandleOtherTouchPhases(Touch touch, float deltaTime) { 
        if (touch.fingerId == _jumpFingerId) return; 
        if (touch.fingerId == _movementFingerId) return;

        if (!_rotationTouchInitialized.TryGetValue(touch.fingerId, out bool isInitialized)) {
            isInitialized = false;
            _rotationTouchInitialized[touch.fingerId] = false;
        }

        if (!isInitialized) {
            _rotationTouchInitialized[touch.fingerId] = true;
        } else {
            float deltaMagnitude = touch.deltaPosition.magnitude;
            
            if (touch.phase != UnityEngine.TouchPhase.Stationary &&
                (touch.position - _initialTouchPositions[touch.fingerId]).magnitude > deadZoneThreshold 
                && deltaMagnitude <= maxFirstMoveDelta
                && deltaMagnitude > 0.1f) {
                lookBuffer += touch.deltaPosition * mobileRotationSensetivity * sensetivity;
            }
        }
/*
        if (RectTransformUtility.RectangleContainsScreenPoint(_game.ui.hud.fireTouchZone, touch.position)) {
            fireIsPressed = true;
        }*/
    }
}