using Acelab.Core;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class PlayerController : MonoBehaviour {
        [BoxGroup("Movement"), SerializeField] private float _moveSpeed = 1.0f;
        [BoxGroup("Movement"), SerializeField] private float _groundDrag = 3f;
        [BoxGroup("Movement"), SerializeField] private float _airModifier = 0.4f;
        private Vector2 _inputDir;
        private Vector3 _moveDir;
        private Vector3 _flatVel;
        private Vector3 _limitedVel;

        [BoxGroup("Jump"), SerializeField] private float _jumpForce = 10f;
        [BoxGroup("Jump"), SerializeField] private float _jumpCooldown = 0.2f;
        private bool _canJump;

        [BoxGroup("Ground Check"), SerializeField] private Transform _groundCheck;
        [BoxGroup("Ground Check"), SerializeField] private float _groundCheckDistance = 0.2f;
        [BoxGroup("Ground Check"), SerializeField] private LayerMask _whatIsGround;
        private bool _isGrounded;

        [BoxGroup("References")]
        [SceneObjectsOnly, SerializeField] private CinemachineCamera _mainCam;
        private InputManager _inputManager;
        private Rigidbody _rb;

        private void Awake() {
            _inputDir = Vector2.zero;
            _moveDir = Vector3.zero;
            _flatVel = Vector3.zero;
            _limitedVel = Vector3.zero;
            _isGrounded = false;
            _canJump = true;
            _rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            _inputManager = InputManager.Instance;
            _inputManager.OnJump += JumpAttempt;
        }

        private void OnDisable() {
            _inputManager.OnJump -= JumpAttempt;
        }

        private void Update() {
            GetMovementInput();
            GroundCheck();
            ApplyDrag();
        }

        private void FixedUpdate() {
            HandleMovement();
            RestrictSpeed();
        }

        private void GetMovementInput() {
            _inputDir = _inputManager.GetPlayerMovement();
        }

        private void GroundCheck() {
            _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        }

        private void ApplyDrag() {
            _rb.drag = _isGrounded ? _groundDrag : 0f;
        }

        private void HandleMovement() {
            _moveDir = _mainCam.transform.forward * _inputDir.y + _mainCam.transform.right * _inputDir.x;
            _moveDir.y = 0f;
            _moveDir.Normalize();

            float speed = _moveSpeed * (_isGrounded ? 1f : _airModifier) * 10f;
            _rb.AddForce(_moveDir * speed, ForceMode.Force);
        }

        private void RestrictSpeed() {
            _flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            if (_flatVel.magnitude < _moveSpeed) return;

            _limitedVel = _flatVel.normalized * _moveSpeed;
            _rb.velocity = new Vector3(_limitedVel.x, _rb.velocity.y, _limitedVel.z);
        }

        private void JumpAttempt() {
            if (!_isGrounded) return;
            if (!_canJump) return;
            _isGrounded = false;

            // Reset y velocity
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            StartCoroutine(ResetJump());
        }

        private IEnumerator ResetJump() {
            yield return Acer.GetWait(_jumpCooldown);
            _canJump = true;
        }

        private void OnDrawGizmosSelected() {
            if (_groundCheck == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + Vector3.down * _groundCheckDistance);
        }
    }
}
