using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

namespace Managers
{
    public class HapticManager : MonoSingleton<HapticManager>
    {
        public void PlayPreset(HapticPatterns.PresetType pattern)
        {
            HapticPatterns.PlayPreset(pattern);

        }
    }
}
