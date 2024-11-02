using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTextUI;
    [SerializeField] private TextMeshProUGUI enemyActiveTextUI;
    [SerializeField] private TextMeshProUGUI enemyInactiveTextUI;

    private int totalCoin;
    private int enemyActive;
    private int enemyInactive;

    private void Start()
    {
        EnemySpawner.Instance.onEnemySpawned += Instance_onEnemySpawned;
        EnemySpawner.Instance.onEnemyDespawned += Instance_onEnemyDespawned;
    }

    private void Instance_onEnemyDespawned()
    {
        enemyInactive++;
        UpdaeUI();
    }

    private void Instance_onEnemySpawned()
    {
        enemyActive++;
        enemyInactive--;
        UpdaeUI();
    }

    private void UpdaeUI()
    {
        enemyInactiveTextUI.text = "Total Enemy Inactive = " + enemyInactive.ToString();
        enemyActiveTextUI.text = "Total Enemy Active = " + enemyActive.ToString();
    }
}
