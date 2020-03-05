using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{

    private float money;
    
    void buy(float amount)
    {
        if(money - amount > 0)
        {
            money -= amount;
        } else
        {
            Debug.Log("Error CoinSystem: Money is negative...");
        }
    }

    void increase(float amount)
    {
        money += amount;
    }
}

