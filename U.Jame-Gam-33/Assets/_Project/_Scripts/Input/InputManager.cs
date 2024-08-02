using Acelab.Core.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mini_Jame_Gam_3
{
    public class InputManager : Singleton<InputManager> {
        private IA_Player _controls;

        public delegate void JumpEvent();
        public event JumpEvent OnJump;

        protected override void Awake() {
            base.Awake();
            _controls = new IA_Player();
        }

        private void OnEnable() {
            _controls.Enable();

            _controls.Player.Jump.performed += Jump;
        }

        private void OnDisable() {
            _controls.Disable();

            _controls.Player.Jump.performed -= Jump;
        }

        public Vector2 GetPlayerMovement()
            => _controls.Player.Movement.ReadValue<Vector2>();

        public Vector2 GetMouseDelta()
            => _controls.Player.Look.ReadValue<Vector2>();

        public void Jump(InputAction.CallbackContext ctx) {
            OnJump?.Invoke();
        }
    }
}
