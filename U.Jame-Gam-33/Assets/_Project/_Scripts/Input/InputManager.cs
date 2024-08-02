using Acelab.Core.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mini_Jame_Gam_3
{
    public class InputManager : Singleton<InputManager> {
        private IA_Player _controls;

        public delegate void JumpEvent();
        public event JumpEvent OnJump;

        public delegate void StartSprintingEvent();
        public event StartSprintingEvent OnStartSprinting;

        public delegate void StopSprintingEvent();
        public event StopSprintingEvent OnStopSprinting;

        protected override void Awake() {
            base.Awake();
            _controls = new IA_Player();
        }

        private void OnEnable() {
            _controls.Enable();

            _controls.Player.Jump.performed += Jump;
            _controls.Player.Sprint.started += StartSprinting;
            _controls.Player.Sprint.canceled += StopSprinting;
        }

        private void OnDisable() {
            _controls.Disable();

            _controls.Player.Jump.performed -= Jump;
            _controls.Player.Sprint.started -= StartSprinting;
            _controls.Player.Sprint.canceled -= StopSprinting;
        }

        public Vector2 GetPlayerMovement()
            => _controls.Player.Movement.ReadValue<Vector2>();

        public Vector2 GetMouseDelta()
            => _controls.Player.Look.ReadValue<Vector2>();

        public void Jump(InputAction.CallbackContext ctx) {
            OnJump?.Invoke();
        }

        private void StartSprinting(InputAction.CallbackContext context) {
            OnStartSprinting?.Invoke();
        }

        private void StopSprinting(InputAction.CallbackContext context) {
            OnStopSprinting?.Invoke();
        }
    }
}
