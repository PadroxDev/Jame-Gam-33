using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class CinemachineExtensionPosition : CinemachineExtension {
        [BoxGroup("References"), SerializeField] private Transform Orientation;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
            if(vcam.Follow) {
                if(stage == CinemachineCore.Stage.Aim) {
                    state.RawOrientation = Orientation.rotation;
                }
            }
        }
    }
}
