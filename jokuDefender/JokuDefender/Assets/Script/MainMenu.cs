using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Text recordText;
    private void Awake()
    {
        recordText.text = "Record: " + PlayerPrefs.GetInt("BestWaveScore").ToString();
    }
    public void StartGame()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("MainScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
