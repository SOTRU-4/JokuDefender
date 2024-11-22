using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyPoint : MonoBehaviour
{
    public Quaternion offset;
    [SerializeField] Image interactText;
    [SerializeField] GameObject currentPoint;
    [SerializeField] BuyBuilding buyPanel;
    public GameObject currentBuilding;
    public GameObject building;
    bool thisPoint = false;
    private void Update()
    {
        interactText.gameObject.transform.position = Camera.main.WorldToScreenPoint(currentPoint.transform.position);

        if (interactText.IsActive() && Input.GetKeyDown(KeyCode.E) && thisPoint)
        {
            
            buyPanel.gameObject.SetActive(true);
            buyPanel.position = currentPoint.transform.position;
            interactText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController player) && currentBuilding == null)
        {
            currentPoint.transform.position = transform.position;
            interactText.gameObject.SetActive(true);
            buyPanel.Setup(building, currentPoint.transform.position, this);
            thisPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            interactText.gameObject.SetActive(false);
            buyPanel.buyPoint = null;
            thisPoint = false;
        }
    }
}
