using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }


    public float customerSpawnFreq = 4;
    public float customerMoveSpeed = 6;
    public GameObject gameCanvas;
    public GameObject customerPrefab;

    public void onFrameUpdate()
    {
        float rand = Random.Range(0, 100f);
        for (int i = 0; i < LaneUtils.Instance.laneList.Length; i++)
        {
            if (rand < (i + 1) * customerSpawnFreq / 3)
            {
                spawn(i);
            }
        }
    }

    void spawn(int laneIndex)
    {
        Vector3 position = LaneUtils.Instance.getLaneEndPosition(laneIndex);
        GameObject customer = Instantiate(customerPrefab, position, Quaternion.identity);
        customer.transform.SetParent(gameCanvas.transform, false);
        customer.GetComponent<CustomerInstance>().speed = customerMoveSpeed;
        customer.GetComponent<CustomerInstance>().onTouchFrontWallAction += BartenderGameManager.Instance.onCustomerTouchFrontWall;
        customer.GetComponent<CustomerInstance>().onTouchBeerAction += BartenderGameManager.Instance.onCustomerTouchBeer;
    }
}
