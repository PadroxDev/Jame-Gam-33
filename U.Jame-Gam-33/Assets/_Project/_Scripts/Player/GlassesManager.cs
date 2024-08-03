using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class GlassesManager : MonoBehaviour
    {
        [BoxGroup("Glasses"), SerializeField] private SO_GlassesBase _defaultGlasses;
        [BoxGroup("Glasses"), SerializeField] private SO_GlassesBase _redGlasses;

        [BoxGroup("References"), SerializeField] private Transform _glassesPoint;
        private GameObject _currentGlassesModel;
        private SO_GlassesBase _currentGlasses;

        private void Start() {
            EquipGlasses(_defaultGlasses);
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.E)) {
                EquipGlasses(_currentGlasses == _defaultGlasses ? _redGlasses : _defaultGlasses);
            }

            if (_currentGlasses == null) return;

            _currentGlasses.Update();
        }

        private void FixedUpdate() {
            if (_currentGlasses == null) return;

            _currentGlasses.FixedUpdate();
        }

        private void EquipGlasses(SO_GlassesBase newGlasses) {
            if (newGlasses == _currentGlasses) return;

            if(_currentGlasses != null) {
                CleanupCurrentGlasses();
            }

            _currentGlasses = newGlasses;

            _currentGlasses.Initialize(gameObject);

            SpawnGlassesModel();

            _currentGlasses.Equip();
        }

        private void CleanupCurrentGlasses() {
            if (_currentGlassesModel != null)
            {
                Destroy(_currentGlassesModel);
            }

            _currentGlasses.Unequip();

            _currentGlasses = null;
        }

        private void SpawnGlassesModel() {
            _currentGlassesModel = Instantiate(_currentGlasses.GlassesPrefab, _glassesPoint);
        }
    }
}
