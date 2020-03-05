using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    public GameObject moveTo;
    public float speed = 2;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(moveTo.transform.position);
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
        
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
