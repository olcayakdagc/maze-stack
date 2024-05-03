using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    private int ballCount;

    private int currentBallCount;
    private void Awake()
    {
        EventManager.getBallCount += BallCount;
        EventManager.getBall += CheckForWin;

    }
    private void OnDestroy()
    {
        EventManager.getBallCount -= BallCount;

        EventManager.getBall -= CheckForWin;
    }
    private async void CheckForWin()
    {
        currentBallCount++;
        if (ballCount == currentBallCount)
        {
            var confeeti = Utilities.ObjectPooler.instance.Spawn("Confetti",new Vector3(0,0,-3), Quaternion.identity);
            confeeti.transform.localScale = new Vector3(5, 5, 5);
            await Task.Delay(1000);
            Managers.LevelManager.instance.Win();
        }
    }
    private void BallCount(int count)
    {
        currentBallCount = 0;
        ballCount = count - 1;

    }
}
