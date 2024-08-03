using Acelab.Core.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class LevelManager : Singleton<LevelManager>
    {
        [BoxGroup("Level"), SerializeField] private SO_GlassesBase[] _defaultGlasses;
        [BoxGroup("Level"), SerializeField] private RedObjects _redObjs;
        private GlassesWheel _glassesWheel;
        [SerializeField] private GlassesManager _TEMPORARYglassesManagerTEMPORARY;

        public RedObjects RedObjs { get { return _redObjs; } }
        
        private void Start() {
            _glassesWheel = GlassesWheel.Instance;
            
            foreach(SO_GlassesBase glasses in _defaultGlasses) {
                _glassesWheel.UnlockGlasses(glasses);
            }

            //_TEMPORARYglassesManagerTEMPORARY.EquipGlasses(_defaultGlasses[0]);
        }
    }
}
