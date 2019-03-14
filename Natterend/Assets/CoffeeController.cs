using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeController : MonoBehaviour
{
    public float maxCoffeeTime = 100;
    float curCoffeeTime;

    public GameObject[] coffeeBar;

    public float maxTimeBeforeLoss = 10;
    float curTimeBeforeLoss;

    void Awake()
    {
        curTimeBeforeLoss = maxTimeBeforeLoss;
        curCoffeeTime = maxCoffeeTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(curCoffeeTime);
        if (curTimeBeforeLoss <= 0)
        {
            curCoffeeTime -= Time.deltaTime;
        }
        else
        {
            curTimeBeforeLoss -= Time.deltaTime;
        }

        if (curCoffeeTime <= 0)
        {
            CoffeeEmpty();
        }

    }

    public void RefreshCoffee()
    {
        curCoffeeTime = maxCoffeeTime;
        curTimeBeforeLoss = maxTimeBeforeLoss;
        Debug.Log("Refreshed");
    }

    void CoffeeEmpty()
    {

    }
}
