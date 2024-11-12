using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void spawn()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        player.SetActive(false);

        yield return new WaitForSeconds(5);

        player.transform.position = transform.position;
        player.SetActive(true);
    }
}
