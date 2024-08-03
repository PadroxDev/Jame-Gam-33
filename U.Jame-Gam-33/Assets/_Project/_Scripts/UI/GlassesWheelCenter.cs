using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mini_Jame_Gam_3
{
    public class GlassesWheelCenter : MonoBehaviour
    {
        [SerializeField, BoxGroup("Parameters")] private float _blendDuration = 0.3f;

        [SerializeField, BoxGroup("UI")] private TextMeshProUGUI _txt;
        [SerializeField, BoxGroup("UI")] private Image _img;

        public void UpdateGlassesData(SO_GlassesBase glasses) {
            _txt.SetText(glasses.Name);
            _img.color = glasses.Color;
        }

        public void FadeUpdateGlassesData(SO_GlassesBase glasses) {
            _txt.SetText(glasses.Name);
            _img.DOColor(glasses.Color, _blendDuration);
        }
    }
}
