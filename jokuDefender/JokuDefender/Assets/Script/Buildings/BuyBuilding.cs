using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class BuyBuilding : MonoBehaviour
{
    public Vector2 position;
    [SerializeField] GameObject prefab;
    public BuyPoint buyPoint;
    public int buildingCost;
    NavMeshSurface navigation;
    private void Awake()
    {
        navigation = FindAnyObjectByType<NavMeshSurface>();
    }
    public void Buy()
    {
        if(PlayerController.instance.PlayerGold >= buildingCost)
        {
            var building = Instantiate(prefab, position, buyPoint.offset);
            gameObject.SetActive(false);
            buyPoint.currentBuilding = building.GetComponent<Building>();
            PlayerController.instance.AddGold(-buildingCost);
            buyPoint.currentSign.SetActive(false);
            navigation.BuildNavMesh();
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Setup(GameObject building,  Vector2 position, BuyPoint point)
    {
        prefab = building; this.position = position; buyPoint = point;
    }
    
}
