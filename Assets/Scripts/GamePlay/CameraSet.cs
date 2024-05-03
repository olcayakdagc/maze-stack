using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class CameraSet : MonoBehaviour
    {
        private void Start()
        {
            EventManager.setCamera += SetCamera;
        }
        private void OnDestroy()
        {
            EventManager.setCamera -= SetCamera;
        }
        private void SetCamera(int z)
        {
            transform.position = new Vector3(0, 0, -z * 2);
        }
    }

}
