using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction gridCompleted;
    public static UnityAction<int,int> startPos;
    public static UnityAction<Direction> onSwipe;
    public static UnityAction getBall;
    public static UnityAction<int> getBallCount;
    public static UnityAction<int> setCamera;

}
