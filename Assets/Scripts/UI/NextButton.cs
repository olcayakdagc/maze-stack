using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class NextButton : MonoBehaviour
    {
        public void NextLevel()
        {
            Managers.LevelManager.instance.LoadNextLevel();
        }
    }

}

