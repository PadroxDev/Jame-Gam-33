using Acelab.Core.Utility;
using System;
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

        public delegate void ProjectEvent();
        public event ProjectEvent OnProject;

        protected override void Awake() {
            base.Awake();
            _controls = new IA_Player();
        }

        private void OnEnable() {
            _controls.Enable();

            _controls.Player.Jump.performed += Jump;
            _controls.Player.Sprint.started += StartSprinting;
            _controls.Player.Sprint.canceled += StopSprinting;

            _controls.Camera.Project.performed += Project;
        }



        private void OnDisable() {
            _controls.Disable();

            _controls.Player.Jump.performed -= Jump;
            _controls.Player.Sprint.started -= StartSprinting;
            _controls.Player.Sprint.canceled -= StopSprinting;

            _controls.Camera.Project.performed -= Project;
        }

        public void EnablePlayerInputs() {
            _controls.Player.Enable();
        }

        public void DisablePlayerInputs() {
            _controls.Player.Disable();
        }

        public Vector2 GetPlayerMovement()
            => _controls.Player.Movement.ReadValue<Vector2>();

        public Vector2 GetMouseDelta()
            => _controls.Player.Look.ReadValue<Vector2>();

        public Vector2 GetMousePos()
            => _controls.Mouse.Position.ReadValue<Vector2>();

        private void Jump(InputAction.CallbackContext ctx) {
            OnJump?.Invoke();
        }

        private void StartSprinting(InputAction.CallbackContext context) {
            OnStartSprinting?.Invoke();
        }

        private void StopSprinting(InputAction.CallbackContext context) {
            OnStopSprinting?.Invoke();
        }

        private void Project(InputAction.CallbackContext context) {
            OnProject?.Invoke();
        }
    }
}
