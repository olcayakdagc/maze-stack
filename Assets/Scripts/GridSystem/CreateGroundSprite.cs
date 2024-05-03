using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class CreateGroundSprite : MonoSingleton<CreateGroundSprite>
    {
        [SerializeField] GridVisual groundSprite;

       
        public GridVisual CreateGridSprite(Vector3 pos, float cellSize)
        {
            var obj = Instantiate(groundSprite, pos, Quaternion.identity, transform);
            obj.transform.localScale = new Vector3 (cellSize, cellSize, cellSize);
            return obj;
        }
    }
}