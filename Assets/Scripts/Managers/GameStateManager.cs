using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    [HideInInspector] public int playerKillCount { get; private set; }
    [HideInInspector] public UnityEvent zombieKilledEvent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void ZombieKilled(Zombie zombie)
    {
        //maybe have zombie have a value with a score or something
        //or add a counter for each type of zombie for end game stats

        playerKillCount++;
        zombieKilledEvent.Invoke();
    }
}
