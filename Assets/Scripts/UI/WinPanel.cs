using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinPanel : UIAnimations
    {
        [SerializeField] CanvasGroup group;

        private void Start()
        {
            Managers.LevelManager.OnLevelWin += CanvasOpen;
            Managers.LevelManager.OnLevelUnload += CloseCanvas;

        }

        private void OnDestroy()
        {
            Managers.LevelManager.OnLevelWin -= CanvasOpen;
            Managers.LevelManager.OnLevelUnload -= CloseCanvas;
        }
        private void CanvasOpen()
        {
            OpenCanvas(group);
        }
        private void CloseCanvas()
        {
            CloseCanvas(group);
        }
    }

}
