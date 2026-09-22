using System.ComponentModel;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Game _game;
    private Player _player;
    public float maxSpeed = 1f;
    private float _verticalRotation = 0;
    private float _horizontalRotation = 0;
    public float senetivity = 1f;
    public Transform cameraHolderTransform;
    public Rigidbody playerRigidbody;
    public bool isGrounded; 
    private Vector3 surfaceNormal; 
    public float jumpForce = 10f;

    private float groundedBlockTimestamp;
    public float groundedBlockTime = 0.2f;

    public float unfocusedFov = 60f;
    private float focusRotationModifier = 1f;
    public float airAcceleration = 2f;
    private bool wasFocused = false;

    public float shakeFrequency = 6f;
    public float shakeMaxAmplitude = 0.10f;
    public float walkLowerOffset = -0.15f;
    public float runLowerOffset = -0.3f;
    public float lerpSharpness = 12f;

    private bool runBlock = false;

    public float stepLegnth = 0.5f;
    private float stepDistCounter = 0;
    private Vector3 lastPosition;
    public float groundCheckRadius = 0.25f;
    public float groundCheckDistance = 0.2f;
    public Vector3 groundCheckOffset = new Vector3(0, 0.1f, 0);

    public void Init(Game game, Player player) {
        _game = game;
        _player = player;
        lastPosition = transform.position;
    }

    public void ManualFixedUpdate() {
        UpdateGroundedState();
        UpdateMove(_player.input.moveBuffer);
        UpdateRotation(_player.input.lookBuffer);
        /*if (_player.input.jumpIsPressed)
            TryJump();*/
        UpdateFocus();
        UpdateWalkShake();
        _player.input.DropBuffers();
        if (isGrounded) StepCount();
    }

    public void SoundOnlyUpdate() {
        StepCount();
    }

    public void ManualUpdate() {
       // if (_player.input.runButtonWasPressedThisFrame) runBlock = false;
    }

    public void UpdateGroundedState() {
        RaycastHit hit;
        bool wasGrounded = isGrounded;
        if (Time.time < groundedBlockTimestamp + groundedBlockTime) return;
        Vector3 origin = transform.position + groundCheckOffset;
        if (Physics.SphereCast(origin, groundCheckRadius, Vector3.down, out hit, groundCheckDistance)) {
            isGrounded = true;
            surfaceNormal = hit.normal;
        } else {
            isGrounded = false;
            surfaceNormal = Vector3.up;
        }

//        if (isGrounded && !wasGrounded && _player) _player.sound.OnLand();
    }

    public void UpdateMove(Vector2 input) {
        Vector3 horizontalMovement;
        if (input != Vector2.zero) {
            Vector3 desiredDirection = (_player.transform.forward * input.y + _player.transform.right * input.x).normalized * maxSpeed;
            if (isGrounded) {
                horizontalMovement = desiredDirection - Vector3.Dot(desiredDirection, surfaceNormal) * surfaceNormal;
            } else {
                horizontalMovement = new Vector3(desiredDirection.x, 0, desiredDirection.z);
            }
        } else {
            horizontalMovement = Vector3.zero;
        }        
        /*if (
            (!_player.input.runButtonIsPressed)
            || _player.input.focusIsPressed
            || runBlock
            )
            horizontalMovement *= 0.5f;        */
        if (playerRigidbody.isKinematic) return;
        if (isGrounded) {
            playerRigidbody.linearVelocity = horizontalMovement;
        } else {
            playerRigidbody.linearVelocity += horizontalMovement * Time.fixedDeltaTime * airAcceleration;
        }
    }

    public void TryJump() {
        if (isGrounded) {
            playerRigidbody.linearVelocity += Vector3.up * jumpForce;
            isGrounded = false;
            groundedBlockTimestamp = Time.time;
            _player.sound.OnJump();
        }
    }

    public void UpdateRotation(Vector2 deltaRotation) {
        _horizontalRotation = (_horizontalRotation + deltaRotation.x * senetivity * focusRotationModifier) % 360f;
        _verticalRotation = Mathf.Clamp(_verticalRotation - deltaRotation.y * senetivity * focusRotationModifier, -89f, 89f);
        _player.transform.localRotation = Quaternion.Euler(0, _horizontalRotation, 0);
        cameraHolderTransform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }

    public void UpdateFocus() {

    }

    public void ResetRotation() {
        _horizontalRotation = NormalizeAngle(_player.transform.localEulerAngles.y);
    }

    public void ResetScope() {
        _game.mainCamera.fieldOfView = unfocusedFov;
        /*if (_player.weaponCamera != null)
            _player.weaponCamera.fieldOfView = unfocusedFov;
        _game.ui.hud.SetScopeAlpha(0);        */
        focusRotationModifier = 1f;
        wasFocused = false;
    }

    private float NormalizeAngle(float angle) {
        angle %= 360f;
        if (angle > 180f)
            angle -= 360f;
        return angle;
    }

    public void UpdateWalkShake() {
        Vector3 horizontalVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0, playerRigidbody.linearVelocity.z);
        float currentSpeed = horizontalVelocity.magnitude;
        float normalizedSpeed = Mathf.Clamp01(currentSpeed / maxSpeed);

        Vector3 targetOffset = Vector3.zero;

        if (_player.input.moveBuffer != Vector2.zero) {
            float time = Time.fixedTime * shakeFrequency;
            float amplitude = normalizedSpeed * shakeMaxAmplitude;

            float x = Mathf.Sin(time) * amplitude;
            float y = Mathf.Sin(2f * time) * amplitude * 0.5f;
            targetOffset = new Vector3(x, y, 0);

            /*float lowerOffset = _player.input.runButtonIsPressed ? runLowerOffset : walkLowerOffset;
            targetOffset.y += lowerOffset;*/
        }

    }

    public void BlockRunByShot() => runBlock = true;

    private void StepCount() {
        if (!_player) return;
        Vector3 delta = _player.transform.position - lastPosition;
        lastPosition = _player.transform.position;
        stepDistCounter += delta.magnitude;
        if (stepDistCounter >= stepLegnth) {
            stepDistCounter %= stepLegnth;
            _player.sound.OnFootstep();
        }
    }
}
