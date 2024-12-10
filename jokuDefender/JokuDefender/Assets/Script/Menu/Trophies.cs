using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class Trophies : MonoBehaviour
{
    [SerializeField] int needScoreToOpen;
    [SerializeField] Color color;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        int waveScore = PlayerPrefs.GetInt("BestWaveScore");
        if(waveScore >= needScoreToOpen)
        {
            sprite.color = color;
        }
    }
}
