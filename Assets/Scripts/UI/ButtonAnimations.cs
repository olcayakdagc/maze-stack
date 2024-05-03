using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class ButtonAnimations : MonoBehaviour
    {
        [SerializeField] float speed = 0.15f;

        [SerializeField] float scaleFactor = 0.8f;

        public void OnPress()
        {
            if (DOTween.IsTweening(this)) return;
            Sequence sequence = DOTween.Sequence();
            Managers.HapticManager.instance.PlayPreset(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);


            var first = transform.DOScale(transform.lossyScale * scaleFactor, speed);
            var second = transform.DOScale(transform.lossyScale, speed / 2).SetEase(Ease.OutBack);

            sequence.Append(first);
            sequence.Append(second);
            sequence.Play().SetTarget(this);
        }
    }

}
