using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class PlayerDirectionManager : MonoBehaviour
    {

        Direction direction;
        Vector2 startPos, endPos;
        public float swipeThreshold = 100f;
        bool draggingStarted;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnBeginDrag(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                OnDrag(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnEndDrag();
            }
        }

        public void OnBeginDrag(Vector3 pos)
        {
            if (Managers.GameManager.instance.gameState != GameStates.gameplay) return;
            draggingStarted = true;
            startPos = pos;
        }
        public void OnDrag(Vector3 pos)
        {
            if (Managers.GameManager.instance.gameState != GameStates.gameplay) return;

            if (draggingStarted)
            {
                endPos = pos;

                Vector2 difference = endPos - startPos; // difference vector between start and end positions.

                if (difference.magnitude > swipeThreshold)
                {
                    if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y)) // Do horizontal swipe
                    {
                        direction = difference.x > 0 ? Direction.Right : Direction.Left; // If greater than zero, then swipe to right.
                    }
                    else //Do vertical swipe
                    {
                        direction = difference.y > 0 ? Direction.Up : Direction.Down; // If greater than zero, then swipe to up.
                    }
                }
                else
                {
                    direction = Direction.None;
                }
            }
        }
        public void OnEndDrag()
        {
            if (Managers.GameManager.instance.gameState != GameStates.gameplay) return;

            if (draggingStarted && direction != Direction.None)
            {
                EventManager.onSwipe?.Invoke(direction);
            }

            startPos = Vector2.zero;
            endPos = Vector2.zero;
            draggingStarted = false;
        }
    }

}
