using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIAnimations : MonoBehaviour
    {

        public void OpenPanel(Image panel, float fade = 0.39f)
        {
            panel.DOFade(0.39f, 0.2f);
        }

        public void ClosePanel(Image panel)
        {
            panel.DOFade(0, 0.2f);
        }


        public void CloseCanvas(CanvasGroup group)
        {
            group.interactable = false;
            group.DOFade(0, 0.5f).OnComplete(() => group.gameObject.SetActive(false));
        }

        public void OpenCanvas(CanvasGroup group)
        {
            group.gameObject.SetActive(true);
            group.interactable = true;
            group.DOFade(1, 0.5f);
        }

        public void Open(CanvasGroup group)
        {
            group.gameObject.SetActive(true);
        }
        public void Close(CanvasGroup group)
        {
            group.gameObject.SetActive(false);
        }

        public void MenuOpening(CanvasGroup group, GameObject UI, Image panel)
        {
            group.DOFade(1, 0);

            panel.DOFade(0, 0);
            panel.DOFade(0.39f, 1);

            OpeningBumpAnim(UI);
        }

        public void OpeningBumpAnim(GameObject UI)
        {
            Sequence sequence = DOTween.Sequence();

            var first = UI.transform.DOScale(UI.transform.localScale * 1.07f, 0.05f).SetEase(Ease.InQuad);

            var second = UI.transform.DOScale(UI.transform.localScale * 0.93f, 0.05f).SetEase(Ease.OutQuad);
            var third = UI.transform.DOScale(UI.transform.localScale, 0.1f).SetEase(Ease.OutQuad);

            sequence.Append(first);
            sequence.Append(second);
            sequence.Append(third);

            sequence.Play();
        }
        public void MenuClosing(CanvasGroup group, Image panel)
        {
            panel.DOFade(0, 0.2f);

            group.DOFade(0, 0.2f).OnComplete(() => Close(group));
        }

        public void GoDownAndComeBack(Transform obj, Ease ease = Ease.OutBack)
        {
            var oldPos = obj.transform.position;

            obj.transform.position = new Vector3(oldPos.x, oldPos.y - (oldPos.y * 0.3f), oldPos.z);

            obj.DOMove(oldPos, 0.4f).SetEase(ease);
        }
        public void GoUpAndComeBack(Transform obj, Ease ease = Ease.OutBack)
        {
            var oldPos = obj.transform.position;

            obj.transform.position = new Vector3(oldPos.x, oldPos.y + (oldPos.y * 0.3f), oldPos.z);

            obj.DOMove(oldPos, 0.4f).SetEase(ease);
        }
        public void GoULeftAndComeBack(Transform obj, Ease ease = Ease.OutBack)
        {
            var oldPos = obj.transform.position;

            obj.transform.position = new Vector3(oldPos.x - (oldPos.x * 0.3f), oldPos.y, oldPos.z);

            obj.DOMove(oldPos, 0.4f).SetEase(ease);
        }
    }
}