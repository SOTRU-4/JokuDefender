using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerScript;
    public CanvasGroup deathScreen;
    public CanvasGroup EndScreen;
    public Button spawnButton;
    public TextMeshProUGUI respawnText;

    private bool respawnReady;
    private bool fade;
    private GameObject bed;

    private void Start()
    {
        bed = GameObject.Find("Bed");
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
                if (bed == null)
                {
                    EndScreen.alpha += Time.deltaTime;
                }
            }
        }
        if (false)
        {
            StopAllCoroutines();
        }
    }

    public void SpawnCheck()
    {
        player.SetActive(false);
        respawnReady = false;
        StartCoroutine(StartCountdown());
        ShowDeathScreen();
    }

    private void ShowDeathScreen()
    {
        fade = true;
        Debug.Log(bed);
        if (bed == null)
        {
            Debug.Log("tuhoutu");
            EndScreen.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("ei tuohoutunu");
            deathScreen.gameObject.SetActive(true);
        }
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
            StopCoroutine(StartCountdown());
            player.transform.position = transform.position;
            playerScript.HealthPoints = playerScript.MaxHealth;
            playerScript.HealthBar.value = playerScript.MaxHealth;
            player.SetActive(true);
            HideDeathScreen();
            respawnText.text = "5";
        }
    }

    public IEnumerator StartCountdown(int countdown = 5)
    {

        while (true)
        {
            yield return new WaitForSeconds(1);
            countdown--;
            if (countdown <= 0)
            {
                respawnText.text = "Respawn";
                spawnButton.interactable = true;
                respawnReady = true;
                break;
            }
            respawnText.text = countdown.ToString();
        }
    }
}
