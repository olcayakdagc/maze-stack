using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay
{
    public class TargetBall : MonoBehaviour
    {
        [SerializeField] MeshRenderer mesh;
        [SerializedDictionary("Color", "Material")]
        public SerializedDictionary<LevelColor, Material> materials = new SerializedDictionary<LevelColor, Material>();

        public void Set(LevelColor color)
        {
            mesh.material = materials[color];
        }
    }

}
