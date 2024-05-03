using System.Collections;
using System.Collections.Generic;
using Array2DEditor;
using UnityEngine;
using GridSystem;
using GamePlay;

namespace LevelDesign
{
    public class Level : MonoBehaviour
    {
        [SerializeField] Array2DBool levelDesignArray;
        [SerializeField] int xStart;
        [SerializeField] int yStart;
        [SerializeField] LevelColor color;


        private void Start()
        {
            GameInit();
        }
        private async void GameInit()
        {
            await GridController.instance.Create(levelDesignArray.GridSize.x, levelDesignArray.GridSize.y);
            EventManager.setCamera?.Invoke(levelDesignArray.GridSize.x);
            int ballCount = 0;
            for (int x = 0; x < levelDesignArray.GridSize.x; x++)
            {
                for (int y = 0; y < levelDesignArray.GridSize.y; y++)
                {
                    if (levelDesignArray.GetCell(x, y))
                    {
                        ballCount++;
                        GridController.instance.grid.GetNodeWithoutCoord(x, (levelDesignArray.GridSize.y - 1) - y).gridVisual.Close();
                        GridController.instance.grid.GetNodeWithoutCoord(x, (levelDesignArray.GridSize.y - 1) - y).isAvailble = true;

                    }
                    else
                    {
                        GridController.instance.grid.GetNodeWithoutCoord(x, (levelDesignArray.GridSize.y - 1) - y).gridVisual.Open();

                        GridController.instance.grid.GetNodeWithoutCoord(x, (levelDesignArray.GridSize.y - 1) - y).isAvailble = false;
                    }
                }
            }
            EventManager.startPos?.Invoke(xStart, yStart);
            EventManager.getBallCount?.Invoke(ballCount);

            for (int x = 0; x < levelDesignArray.GridSize.x; x++)
            {
                for (int y = 0; y < levelDesignArray.GridSize.y; y++)
                {
                    if (levelDesignArray.GetCell(x, y))
                    {
                        var grid = GridController.instance.grid.GetNodeWithoutCoord(x, (levelDesignArray.GridSize.y - 1) - y);
                        if (grid.isAvailble)
                        {
                            var pos = new Vector3(grid.xPos, grid.yPos);
                            var ball = Utilities.ObjectPooler.instance.Spawn("TargetBall", pos, Quaternion.identity);
                            ball.GetComponent<TargetBall>().Set(color);
                        }
                    }
                }
            }
        }

    }

}
