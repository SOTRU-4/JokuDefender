using UnityEngine;

public class FlashScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.03f);
    }
}
