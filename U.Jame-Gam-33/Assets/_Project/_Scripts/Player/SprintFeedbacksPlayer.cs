using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class SprintFeedbacksPlayer : MonoBehaviour
    {
        [BoxGroup("Feedbacks"), SceneObjectsOnly, SerializeField]
        private MMF_Player _startSprintingFeedbacks;
        [BoxGroup("Feedbacks"), SceneObjectsOnly, SerializeField]
        private MMF_Player _stopSprintingFeedbacks;

        [BoxGroup("References"), SceneObjectsOnly, SerializeField]
        private PlayerController _playerController;

        private void OnEnable() {
            _playerController.OnMovementStateChanged += MovementStateChanged;
        }

        private void OnDisable() {
            _playerController.OnMovementStateChanged -= MovementStateChanged;
        }

        private void MovementStateChanged(MovementState previousState, MovementState newState) {
            if (previousState == MovementState.Idle || previousState == MovementState.Walking) {
                if (newState == MovementState.Sprinting || newState == MovementState.Air) {
                    PlayStartSprintingFeedbacks();
                }
            } else { // previousState == SPRINTING || AIR
                if (newState == MovementState.Idle || newState == MovementState.Walking) {
                    PlayStopSprintingFeedbacks();
                }
            }
        }

        private void PlayStartSprintingFeedbacks() {
            _startSprintingFeedbacks.PlayFeedbacks();
        }

        private void PlayStopSprintingFeedbacks() {
            _stopSprintingFeedbacks.PlayFeedbacks();
        }
    }
}
