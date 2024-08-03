using Sirenix.OdinInspector;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public abstract class SO_GlassesBase : ScriptableObject
    {
        [AssetsOnly] public GameObject GlassesPrefab;

        public virtual void Initialize(object player) { }

        public abstract void Equip();
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public abstract void Unequip();
    }
}