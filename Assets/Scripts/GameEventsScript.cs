using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEventsScript
{
    public static UnityEvent shotCannon = new UnityEvent();
    public static UnityEvent gameIsOver = new UnityEvent();
    public static UnityEvent pauseGame = new UnityEvent();
    public static DummyHitEvent hitDummy = new DummyHitEvent();

}

public class DummyHitEvent: UnityEvent<DummyHitData>{}
public class DummyHitData
{
    public bool changeLevel;
    
    public DummyHitData(bool changeLevel)
    {
        this.changeLevel = changeLevel;
    }
}
