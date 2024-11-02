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
    private int totalEnemyActive;
    private int totalEnemyInactive;

    private void Start()
    {
        GameManager.Instance.onDataChanged += Instance_onDataChanged;
        UpdaeUI();
    }

    private void Instance_onDataChanged(int totalEnemyActive, int totalEnemyInactive, int totalCoin)
    {
        this.totalEnemyActive = totalEnemyActive;
        this.totalEnemyInactive = totalEnemyInactive;
        this.totalCoin = totalCoin;
        UpdaeUI();
    }


    private void UpdaeUI()
    {
        coinTextUI.text = "Total Coin = " + totalCoin.ToString();
        enemyActiveTextUI.text = "Total Enemy Active = " + totalEnemyActive.ToString();
        enemyInactiveTextUI.text = "Total Enemy Inactive = " + totalEnemyInactive.ToString();
    }
}
