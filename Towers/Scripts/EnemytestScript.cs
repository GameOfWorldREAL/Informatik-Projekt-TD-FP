using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytestScript : MonoBehaviour
{

    public float lifeTime;
    public Rigidbody rb;
    public float thrust;

    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust * 50f);
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= .01f;
    }
}
