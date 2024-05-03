using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Node
    {
        private Grid<Node> _grid;
        private float _xPos;
        public float xPos { get { return _xPos; } }
        private float _yPos;
        public float yPos { get { return _yPos; } }

        private int _x;
        public int x { get { return _x; } }
        private int _y;
        public int y { get { return _y; } }

        public bool isAvailble;

        public float gCost;
        public float hCost;
        public float fCost;

        public Node cameFromNode;
        public GridVisual gridVisual;

        public Node(Grid<Node> grid, float xPos, float yPos, int x, int y, GridVisual gridVisual)
        {
            this._grid = grid;
            this._xPos = xPos;
            this._yPos = yPos;
            this._x = x;
            this._y = y;
            isAvailble = true;
            this.gridVisual = gridVisual;
        }
        public List<Node> GetNeighbourList()
        {
            List<Node> list = new List<Node>();
            if (_x - 1 >= 0)
            {
                //Left
                list.Add(_grid.GetNodeWithoutCoord((int)(x - 1), y));
                //LeftDown
                if (y - 1 >= 0)
                {
                    list.Add(_grid.GetNodeWithoutCoord(x - 1, y - 1));
                }
                //LeftUp
                if (y + 1 < _grid.height)
                {
                    list.Add(_grid.GetNodeWithoutCoord(x - 1, y + 1));
                }
            }

            if (_x + 1 < _grid.width)
            {
                //Right
                list.Add(_grid.GetNodeWithoutCoord((int)(x + 1), y));
                //RightDown
                if (y - 1 >= 0)
                {
                    list.Add(_grid.GetNodeWithoutCoord(x + 1, y - 1));
                }
                //RightUp
                if (y + 1 < _grid.height)
                {
                    list.Add(_grid.GetNodeWithoutCoord(x + 1, y + 1));
                }
            }
            //Down
            if (y - 1 >= 0)
            {
                list.Add(_grid.GetNodeWithoutCoord(x, y - 1));
            }
            //Up
            if (y + 1 < _grid.height)
            {
                list.Add(_grid.GetNodeWithoutCoord(x, y + 1));
            }
            return list;
        }
        public void CalculateFCost()
        {
            fCost = gCost + fCost;
        }
    }
}