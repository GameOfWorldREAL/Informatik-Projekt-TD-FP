using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class testScript : MonoBehaviour
{
    public GameObject test;
    Thread t1;
    // Start is called before the first frame update
    void Start()
    {
        t1 = new Thread(Method);
        t1.Start();
    }

    void Method()
    {
          Transform.Instantiate(test, new Vector3(0, 1, 2), Quaternion.Euler(0, 1, 2));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
