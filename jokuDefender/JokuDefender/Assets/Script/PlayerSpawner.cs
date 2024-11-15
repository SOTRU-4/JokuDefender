using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerScript;
    public GameObject DeathPanel;
    public Button SpawnButton;

    public TextMeshProUGUI respawnText;

    private bool respawnReady;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    public void SpawnCheck()
    {
        respawnReady = false;
        StartCoroutine(StartCountdown());
        StartCoroutine(RespawnReady());
        ShowDeathScreen();
    }

    private void ShowDeathScreen()
    {
        DeathPanel.SetActive(true);
        //DeathPanel.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
        //SpawnButton.GetComponent<Image>().CrossFadeAlpha(0, 1, false);

        //respawnText.CrossFadeAlpha(0, 1, false);
    }

    public void HideDeathScreen()
    {
        DeathPanel.SetActive(false);
        //DeathPanel.GetComponent<Image>().CrossFadeAlpha(1, 0,false);
        //SpawnButton.GetComponent<Image>().CrossFadeAlpha(1, 0, false);

        //respawnText.color = new Color(255, 255, 255, 1);
        SpawnButton.interactable = false;
    }

    public void Respawn()
    {
        if (respawnReady)
        {
            player.transform.position = transform.position;
            playerScript.HealthPoints = 20;
            player.SetActive(true);
            HideDeathScreen();
            respawnText.text = "5";
        }
    }

    int currentCountdown;
    public IEnumerator StartCountdown(int countdown = 5)
    {
        currentCountdown = countdown;

        while (true)
        {
            yield return new WaitForSeconds(1);
            currentCountdown--;
            if (currentCountdown == 0)
            {
                respawnText.text = "Respawn";
                SpawnButton.interactable = true;
                break;
            }
            respawnText.text = currentCountdown.ToString();
        }
    }

    IEnumerator RespawnReady()
    {
        player.SetActive(false);

        yield return new WaitForSeconds(5);

        respawnReady = true;
    }
}
