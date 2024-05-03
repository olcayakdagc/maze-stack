using UnityEngine;

namespace GridSystem
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeField] GameObject box;
        public void Close()
        {
            box.SetActive(false);
        }
        public void Open()
        {
            box.SetActive(true);
        }
    }
}