using Mini_Jame_Gam_3;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;

namespace Jame_Gam_33
{
    public class PlayerCamera : MonoBehaviour
    {
        [BoxGroup("Parameters"), SerializeField] private float _horizontalSensibility;
        [BoxGroup("Parameters"), SerializeField] private float _verticalSensibility;
        [BoxGroup("Parameters"), SerializeField] private float _maxRotationAngle = 90f;

        [BoxGroup("References")]
        [SerializeField, SceneObjectsOnly] private Transform _orientation;
        [Space]
        public bool ShowHelpers;
        [ShowIfGroup("ShowHelpers")]
        [BoxGroup("ShowHelpers/Helpers")] public bool ShowCursor;

        private InputManager _inputManager;
        private Vector2 _mouseDelta;
        private float _xRotation;
        private float _yRotation;

        private void Start() {
            _inputManager = InputManager.Instance;

            if(!ShowCursor) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update() {
            _mouseDelta = _inputManager.GetMouseDelta();
            if (_mouseDelta == Vector2.zero) return;

            _xRotation -= _mouseDelta.y * Time.deltaTime * _horizontalSensibility;
            _yRotation += _mouseDelta.x * Time.deltaTime * _verticalSensibility;

            _xRotation = Mathf.Clamp(_xRotation, -_maxRotationAngle, _maxRotationAngle);

            _orientation.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            transform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
        }

        private void OnDrawGizmosSelected() {
            Quaternion rightAngle = Quaternion.AngleAxis(_maxRotationAngle, Vector3.up);
            Quaternion leftAngle = Quaternion.AngleAxis(-_maxRotationAngle, Vector3.up);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + rightAngle * transform.forward * 5f);
            Gizmos.DrawLine(transform.position, transform.position + leftAngle * transform.forward * 5f);
        }
    }
}
