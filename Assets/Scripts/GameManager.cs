using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Tooltip("In Minutes")]
    [SerializeField] private int gameLength;

    [Header("Game Data")]
    [SerializeField] private int totalCoin;
    [SerializeField] private int totalEnemyActive;
    [SerializeField] private int totalEnemyNotActive;
    [SerializeField] private int totalEnemyDead;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin(int coin)
    {
        totalCoin += coin;
        Debug.Log(totalCoin);
    }

    public int GetGameLength()
    {
        return gameLength;
    }
}
