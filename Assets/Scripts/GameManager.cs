using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public event Action<int, int, int> onDataChanged;


    [Header("Game Data")]
    [SerializeField] private float gameDuration;
    [SerializeField] private int totalCoin;
    [SerializeField] private int totalEnemyActive;
    [SerializeField] private int totalEnemyInactive;
    [SerializeField] private int totalEnemyDead;

    [Header("UI")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        totalEnemyInactive = EnemySpawner.Instance.GetTotalEnemyThisLevel();
    }

    private void Update()
    {
        gameDuration -= Time.deltaTime;
        if (gameDuration <= 0)
        {
            gameDuration = 0;
            ShowWinCondition();
            Time.timeScale = 0f;
        }

        int minutes = Mathf.FloorToInt(gameDuration / 60);
        int seconds = Mathf.FloorToInt(gameDuration % 60);

        // Format dan tampilkan timer
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreaseActiveEnemy()
    {
        totalEnemyActive++;
        totalEnemyInactive--;
        UpdateData();
    }

    public void DecreaseActiveEnemy()
    {
        totalEnemyActive--;
        totalEnemyInactive++;
        UpdateData();
    }

    public void AddCoin(int coin)
    {
        totalCoin += coin;
        UpdateData();
    }

    private void UpdateData()
    {
        onDataChanged?.Invoke(totalEnemyActive, totalEnemyInactive, totalCoin);
    }

    public void ShowLoseCondition()
    {
        loseScreen.SetActive(true);
    }

    public void ShowWinCondition()
    {
        winScreen.SetActive(true);
    }
}
