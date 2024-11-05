using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    private float raise = 1;

    private void Start()
    {
        transform.localPosition = new Vector3(100, 0, 0);
    }
    void Update()
    {
        transform.Translate(0, raise, 0);
        gameObject.GetComponent<Text>().color = new Color(219, 255, 111, raise);

        if (raise < 0)
        {
            Destroy(gameObject);
        }
        raise -= 0.01f;
    }
}