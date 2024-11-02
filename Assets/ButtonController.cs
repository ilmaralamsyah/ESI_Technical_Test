using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;


    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
