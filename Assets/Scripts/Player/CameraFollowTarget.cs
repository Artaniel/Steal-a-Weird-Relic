using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private Camera _camera;
    private Transform _target;
    private bool _isFollowing = false;

    public float lerpFactor = 0.1f;

    public void Init(Camera camera, Transform target) {
        _camera = camera;
        _target = target;
        _isFollowing = true;
    }

    public void Stop() {
        _isFollowing = false;
    }

    private void Update() {
        if (_isFollowing && _target != null && _camera != null) {
            Vector3 direction = _target.position + Vector3.up - _camera.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _camera.transform.rotation = Quaternion.Slerp(
                _camera.transform.rotation,
                targetRotation,
                lerpFactor
            );
        }
    }
}
