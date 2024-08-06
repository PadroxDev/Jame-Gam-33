using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class ProjectionManager : MonoBehaviour
    {
        [BoxGroup("Virtual Cameras"), SerializeField, SceneObjectsOnly]
        private CinemachineCamera _mainVC;
        [BoxGroup("Virtual Cameras"), SerializeField, SceneObjectsOnly]
        private CinemachineCamera _projectionVC;
        [BoxGroup("Feedbacks"), ChildGameObjectsOnly, SerializeField]
        private MMF_Player _projectFeedbacks;
        [BoxGroup("Feedbacks"), ChildGameObjectsOnly, SerializeField]
        private MMF_Player _returnFeedbacks;
        
        private InputManager _inputManager;
        private bool _projection;

        public bool Projection { get { return _projection; } }

        public delegate void ProjectionToggledEvent(bool toggled);
        public event ProjectionToggledEvent OnProjectionToggled;

        private void Start() {
            _inputManager = InputManager.Instance;
            _inputManager.OnProject += ProjectToggle;
        }

        private void OnDisable() {
            _inputManager.OnProject -= ProjectToggle;
        }

        private void ProjectToggle() {
            _projection = !_projection;

            if(_projection) {
                _inputManager.DisablePlayerInputs();
                _inputManager.DisableGlassesWheel();
                _projectFeedbacks.PlayFeedbacks();
            } else {
                _inputManager.EnablePlayerInputs();
                _inputManager.EnableGlassesWheel();
                _returnFeedbacks.PlayFeedbacks();
            }
        }
    }
}
