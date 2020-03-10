using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fire;
    public GameObject temple;
    public int aktiveAtHealth = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (temple.GetComponent<Temple>().health <= aktiveAtHealth)
        {
            fire.SetActive(true);
        }
    }
}
