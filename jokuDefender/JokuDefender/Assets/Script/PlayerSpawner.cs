using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerScript;
    public CanvasGroup deathScreen;
    public Button spawnButton;
    public TextMeshProUGUI respawnText;

    private bool respawnReady;
    private bool fade;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (fade)
        {
            if (deathScreen.alpha < 1.0f)
            {
                deathScreen.alpha += Time.deltaTime;
            }
        }
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
        fade = true;
        deathScreen.gameObject.SetActive(true);
    }

    public void HideDeathScreen()
    {
        fade = false;
        deathScreen.alpha = 0;
        deathScreen.gameObject.SetActive(false);
        spawnButton.interactable = false;
    }

    public void Respawn()
    {
        if (respawnReady)
        {
            player.transform.position = transform.position;
            playerScript.HealthPoints = playerScript.MaxHealth;
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
                spawnButton.interactable = true;
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
