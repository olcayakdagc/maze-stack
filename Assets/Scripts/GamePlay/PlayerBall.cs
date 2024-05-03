using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using DG.Tweening;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] float firsUpOffset = 0.5f;
    [SerializeField] float upOffset = 0.3f;

    [SerializeField] float followSpeed = 10;

    [SerializeField] float speed = 10;
    private List<GameObject> followBalls = new List<GameObject>();

    private Node currentNode;
    private bool canGo = true;

    private void Awake()
    {
        EventManager.startPos += PlaceObject;
        EventManager.onSwipe += TryToGo;
        Managers.LevelManager.OnLevelUnload += ResetForNextLevel;
    }
    private void OnDestroy()
    {
        EventManager.startPos -= PlaceObject;
        EventManager.onSwipe -= TryToGo;
        Managers.LevelManager.OnLevelUnload -= ResetForNextLevel;
    }
    private void PlaceObject(int x, int y)
    {
        var grid = GridController.instance.grid.GetNodeWithoutCoord(x, y);
        transform.position = new Vector3(grid.xPos, grid.yPos);
        grid.isAvailble = false;
        currentNode = grid;
    }
    private void ResetForNextLevel()
    {
        followBalls.Clear();
    }
    private void FixedUpdate()
    {
        if (followBalls.Count > 0)
        {
            var first = followBalls[0];
            var pos = new Vector3(transform.position.x, transform.position.y, (transform.position.z - firsUpOffset));
            first.transform.position = Vector3.Lerp(first.transform.position, pos, Time.fixedDeltaTime * followSpeed);

            for (int i = 1; i < followBalls.Count; i++)
            {
                var item = followBalls[i];
                var before = followBalls[i - 1];
                pos = new Vector3(before.transform.position.x, before.transform.position.y, (before.transform.position.z - upOffset));
                item.transform.position = Vector3.Lerp(item.transform.position, pos, Time.fixedDeltaTime * followSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("targetBall"))
        {
            var particle = Utilities.ObjectPooler.instance.Spawn("Smoke", other.transform.position);
            particle.transform.localScale = particle.transform.localScale * 0.3f;
            Managers.HapticManager.instance.PlayPreset(Lofelt.NiceVibrations.HapticPatterns.PresetType.MediumImpact);
            EventManager.getBall?.Invoke();
            followBalls.Add(other.gameObject);
        }
    }

    private void TryToGo(Direction direction)
    {
        if (!canGo) return;
        Node targetNode = null;
        Vector3 rotateDirection = Vector3.zero;
        switch (direction)
        {
            case Direction.Up:

                rotateDirection = new Vector3(360, 0, 0);
                for (int i = currentNode.y + 1; i < GridController.instance.grid.height; i++)
                {
                    var grid = GridController.instance.grid.GetNodeWithoutCoord(currentNode.x, i);
                    if (grid.isAvailble)
                    {
                        targetNode = grid;
                    }
                    else
                    {
                        break;
                    }
                }

                break;
            case Direction.Down:
                for (int i = currentNode.y - 1; i > 0; i--)
                {
                    rotateDirection = new Vector3(-360, 0, 0);

                    var grid = GridController.instance.grid.GetNodeWithoutCoord(currentNode.x, i);
                    if (grid.isAvailble)
                    {
                        targetNode = grid;
                    }
                    else
                    {
                        break;
                    }
                }

                break;
            case Direction.Left:
                for (int i = currentNode.x - 1; i > 0; i--)
                {
                    rotateDirection = new Vector3(0, 360, 0);

                    var grid = GridController.instance.grid.GetNodeWithoutCoord(i, currentNode.y);
                    if (grid.isAvailble)
                    {
                        targetNode = grid;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case Direction.Right:
                for (int i = currentNode.x + 1; i < GridController.instance.grid.width; i++)
                {
                    rotateDirection = new Vector3(0, -360, 0);

                    var grid = GridController.instance.grid.GetNodeWithoutCoord(i, currentNode.y);
                    if (grid.isAvailble)
                    {
                        targetNode = grid;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
        }
        if (targetNode == null)
        {
            Managers.HapticManager.instance.PlayPreset(Lofelt.NiceVibrations.HapticPatterns.PresetType.Warning);

            Vector3 oldPos = transform.position;
            switch (direction)
            {
                case Direction.Up:
                    transform.DOMoveY(transform.position.y + 0.2f, 0.15f);
                    break;
                case Direction.Down:
                    transform.DOMoveY(transform.position.y - 0.2f, 0.15f);
                    break;
                case Direction.Left:
                    transform.DOMoveX(transform.position.x - 0.2f, 0.15f);
                    break;
                case Direction.Right:
                    transform.DOMoveX(transform.position.x + 0.2f, 0.15f);
                    break;
            }
            transform.DOLocalRotate(rotateDirection, 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(this).SetLoops(-1).SetRelative();

            transform.DOMove(oldPos, 0.15f).SetDelay(0.15f).OnComplete(() => DOTween.Kill(this));

            return;
        }
        Managers.HapticManager.instance.PlayPreset(Lofelt.NiceVibrations.HapticPatterns.PresetType.RigidImpact);

        canGo = false;
        transform.DOLocalRotate(rotateDirection, 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(this).SetLoops(-1).SetRelative();
        var pos = new Vector2(targetNode.xPos, targetNode.yPos);
        transform.DOMove(pos, speed).SetSpeedBased().OnComplete(() =>
        {
            DOTween.Kill(this);
            canGo = true;
            currentNode.isAvailble = true;

            currentNode = targetNode;
            currentNode.isAvailble = false;
        }).SetEase(Ease.Linear);
    }
}
