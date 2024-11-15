using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    private float raise = 0.5f;

    void Update()
    {
        transform.Translate(0, raise, 0);
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(247, 255, 0, raise * 2);

        if (raise < 0)
        {
            Destroy(gameObject);
        }
        raise -= 0.005f;
    }
}