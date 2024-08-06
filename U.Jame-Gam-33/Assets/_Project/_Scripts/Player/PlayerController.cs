using Acelab.Core;
using Acelab.Modules.Audio;
using Sirenix.OdinInspector;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class PlayerController : MonoBehaviour {
        [BoxGroup("Movement"), SerializeField] private float _walkSpeed = 1.0f;
        [BoxGroup("Movement"), SerializeField] private float _sprintSpeed = 2.0f;
        [BoxGroup("Movement"), SerializeField] private float _groundDrag = 3f;
        [BoxGroup("Movement"), SerializeField] private float _airModifier = 0.4f;
        private Vector2 _inputDir;
        private Vector3 _moveDir;
        private Vector3 _flatVel;
        private Vector3 _limitedVel;
        private float _moveSpeed;
        private bool _sprinting;

        [BoxGroup("Jump"), SerializeField] private float _jumpForce = 10f;
        [BoxGroup("Jump"), SerializeField] private float _jumpCooldown = 0.2f;
        private bool _canJump;

        [BoxGroup("Ground Check"), SerializeField] private Transform _groundCheck;
        [BoxGroup("Ground Check"), SerializeField] private float _groundCheckDistance = 0.2f;
        [BoxGroup("Ground Check"), SerializeField] private LayerMask _whatIsGround;
        private bool _isGrounded;

        [BoxGroup("Slope Handling"), SerializeField] private Transform _slopeCheck;
        [BoxGroup("Slope Handling"), SerializeField] private float _slopeCheckDistance = 0.3f;
        [BoxGroup("Slope Handling"), SerializeField] private float _maxSlopeAngle = 45f;
        private RaycastHit _slopeHit;
        private bool _isOnSlope;
        private bool _exitingSlope;

        [BoxGroup("Audio"), SerializeField] private float _landSFXVol;
        private AudioController _audioController;

        [BoxGroup("References")]
        [SceneObjectsOnly, SerializeField] private CinemachineCamera _mainCam;
        [SceneObjectsOnly, SerializeField] private CinemachineImpulseSource _landingSource;
        private InputManager _inputManager;
        private Rigidbody _rb;

        public MovementState State { get; private set; }

        public delegate void MovementStateChangedEvent(MovementState previousState, MovementState newState);
        public event MovementStateChangedEvent OnMovementStateChanged;

        private void Awake() {
            _inputDir = Vector2.zero;
            _moveDir = Vector3.zero;
            _flatVel = Vector3.zero;
            _limitedVel = Vector3.zero;
            _moveSpeed = _walkSpeed;
            _isGrounded = false;
            _isOnSlope = false;
            _exitingSlope = false;
            _sprinting = false;
            _canJump = true;
            _rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            _audioController = AudioController.Instance;

            _inputManager = InputManager.Instance;
            _inputManager.OnJump += JumpAttempt;
            _inputManager.OnStartSprinting += StartSprinting;
            _inputManager.OnStopSprinting += StopSprinting;
        }

        private void OnDisable() {
            _inputManager.OnJump -= JumpAttempt;
            _inputManager.OnStartSprinting -= StartSprinting;
            _inputManager.OnStopSprinting -= StopSprinting;
        }

        private void Update() {
            GetMovementInput();

            MovementState previousState = State;
            bool stateChanged = HandleStateChange();
            if (stateChanged)
                OnMovementStateChanged?.Invoke(previousState, State);

            GroundCheck();
            SlopeCheck();
            ApplyDrag();
        }

        private void FixedUpdate() {
            HandleMovement();
            ApplyGravity();
            RestrictSpeed();
        }

        private void ApplyGravity() {
            _rb.useGravity = !_isOnSlope;
        }

        private bool HandleStateChange() {
            if(!_isGrounded) {
                if (State == MovementState.Air) return false;
                State = MovementState.Air;
                return true;
            }

            if (_inputDir == Vector2.zero) {
                if (State == MovementState.Idle) return false;
                State = MovementState.Idle;
                return true;
            }

            if (_sprinting) {
                if (State == MovementState.Sprinting) return false;
                State = MovementState.Sprinting;
                _moveSpeed = _sprintSpeed;
                return true;
            }

            if (State == MovementState.Walking) return false;
            State = MovementState.Walking;
            _moveSpeed = _walkSpeed;
            return true;
        }

        private void GetMovementInput() {
            _inputDir = _inputManager.GetPlayerMovement();
        }

        private void GroundCheck() {
            if (_exitingSlope) {
                _isGrounded = false;
                return;
            }
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
            if(!wasGrounded && _isGrounded) {
                _audioController.PlayAudio(Acelab.Modules.Audio.AudioType.SFX_FeetLand, transform.position, _landSFXVol);
                _landingSource.GenerateImpulse();
            }
        }

        private void SlopeCheck() {
            _isOnSlope = false;

            if (!Physics.Raycast(_slopeCheck.position, Vector3.down, out _slopeHit, _slopeCheckDistance, _whatIsGround)) return;

            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            _isOnSlope = angle < _maxSlopeAngle && angle != 0f;
        }

        private void ApplyDrag() {
            _rb.drag = _isGrounded ? _groundDrag : 0f;
        }

        private void HandleMovement() {
            if (_inputDir == Vector2.zero) return;

            _moveDir = _mainCam.transform.forward * _inputDir.y + _mainCam.transform.right * _inputDir.x;
            _moveDir.y = 0f;
            _moveDir.Normalize();

            if(_isOnSlope) {
                MoveOnSlope();
            } else {
                MoveOnFlatOrAir();
            }
        }

        private void MoveOnSlope() {
            Vector3 slopeMoveDir = GetSlopeMoveDir() * _moveSpeed * 10f;
            _rb.AddForce(slopeMoveDir, ForceMode.Force);

            if (_rb.velocity.y <= 0f) return;
            _rb.AddForce(Vector3.down * 20f, ForceMode.Force);
        }

        private void MoveOnFlatOrAir() {
            float speed = _moveSpeed * (_isGrounded ? 1f : _airModifier) * 10f;
            _rb.AddForce(_moveDir * speed, ForceMode.Force);
        }

        private void RestrictSpeed() {
            if(_isOnSlope && !_exitingSlope) {
                if(_rb.velocity.magnitude > _moveSpeed) {
                    _rb.velocity = _rb.velocity.normalized * _moveSpeed;
                }
            } else {
                _flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

                if (_flatVel.magnitude <= _moveSpeed) return;

                _limitedVel = _flatVel.normalized * _moveSpeed;
                _rb.velocity = new Vector3(_limitedVel.x, _rb.velocity.y, _limitedVel.z);
            }
        }

        private Vector3 GetSlopeMoveDir()
            => Vector3.ProjectOnPlane(_moveDir, _slopeHit.normal).normalized;

        private void JumpAttempt() {
            if (!_isGrounded) return;
            if (!_canJump) return;
            _isGrounded = false;
            _exitingSlope = true;

            // Reset y velocity
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            StartCoroutine(ResetJump());
        }

        private IEnumerator ResetJump() {
            yield return Acer.GetWait(_jumpCooldown);
            _canJump = true;
            _exitingSlope = false;
        }

        private void StartSprinting() {
            _sprinting = true;
        }

        private void StopSprinting() {
            _sprinting = false;
        }

        private void OnDrawGizmosSelected() {
            if (_groundCheck == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + Vector3.down * _groundCheckDistance);
        }
    }

    public enum MovementState {
        Idle,
        Walking,
        Sprinting,
        Air
    }
}
