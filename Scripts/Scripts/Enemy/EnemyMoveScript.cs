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

        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);       //Richtung und Geschwingigkeit werden festgelegt
        GetComponent<Rigidbody>().velocity = transform.forward * speed;  
        
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);

            
    }
}
