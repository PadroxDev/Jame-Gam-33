using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class ProjectionCameraManager : MonoBehaviour
    {
        [BoxGroup("Variables"), SerializeField] private Vector3 _followOffset = new Vector3(0, 10, -5);
        [BoxGroup("Variables"), SerializeField] private float _followSpeed = 3f;
        [BoxGroup("Variables"), SerializeField] private float _blendAmount = 5f;
        [BoxGroup("Variables"), SerializeField] private float _blendSpeed = 5f;
        [BoxGroup("Variables"), SerializeField] private float _backtrackDistance = 0.5f;
        private Vector3 _targetPosition;


        [BoxGroup("Raycast"), SerializeField] private LayerMask _whatIsNotPlayer;
        private RaycastHit _hit;
        private Vector3 _raycastDir;
        private float _raycastLength;

        [BoxGroup("References"), SceneObjectsOnly, SerializeField]
        private ProjectionManager _projectionManager;
        [BoxGroup("References"), SceneObjectsOnly, SerializeField]
        private Transform _player;

        private void Awake() {
            _raycastLength = _followOffset.magnitude;
        }

        private void Start() {

        }

        private void Update() {
            _raycastDir = _followOffset;

            Vector3 flat = new Vector3(_followOffset.x, 0f, _followOffset.z);
            Quaternion rot = Quaternion.AngleAxis(Vector3.Angle(flat, Vector3.forward), Vector3.up);
            //_raycastDir = rot *

            //CalculateTargetPosition();


            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _followSpeed);
        }

        private void CalculateTargetPosition() {
            if (Physics.Raycast(_player.position, _raycastDir, out _hit, _raycastLength, _whatIsNotPlayer)) {
                _targetPosition = _hit.point - _raycastDir * _backtrackDistance;
            } else {
                _targetPosition = _player.position + _followOffset;
            }
        }

        private void OnDrawGizmos() {
            if (_player == null) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_player.position, _player.position + _followOffset);
        }
    }
}
