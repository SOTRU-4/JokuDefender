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
    public Building currentBuilding;
    public GameObject building;

    bool thisPoint = false;
    [SerializeField] GameObject sign;
    [HideInInspector] public GameObject currentSign;

    [SerializeField] LevelUpBuilding levelUpPanel;

    private void Start()
    {
        currentSign = Instantiate(sign, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        interactText.gameObject.transform.position = Camera.main.WorldToScreenPoint(currentPoint.transform.position);

        if (interactText.IsActive() && Input.GetKeyDown(KeyCode.E) && thisPoint)
        {
            if(currentBuilding != null)
            {
                levelUpPanel.gameObject.SetActive(true);
                levelUpPanel.buyPoint = this;
            }
            else
            {
                buyPanel.gameObject.SetActive(true);
                buyPanel.position = currentPoint.transform.position;
                interactText.gameObject.SetActive(false);
            }
        }
        if(currentBuilding == null)
        {
            currentSign.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController player))
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
