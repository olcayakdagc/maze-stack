using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GridSystem
{
    public class GridController : MonoSingleton<GridController>
    {
        //[SerializeField] int width;
        //[SerializeField] int height;
        [SerializeField] float cellSize;
        private Grid<Node> _grid;
        public Grid<Node> grid { get { return _grid; } }

        
        public async UniTask Create(int width, int height)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            Vector3 point = new Vector3(-width / 2 * cellSize, -height / 2 * cellSize);
            _grid = new Grid<Node>(width, height, cellSize, point, (Grid<Node> g, float xPos, float yPos, int x, int y, GridVisual gridVisual) => new Node(g, xPos, yPos, x, y, gridVisual));
            EventManager.gridCompleted?.Invoke();
            await UniTask.CompletedTask;
        }
       
    }
}