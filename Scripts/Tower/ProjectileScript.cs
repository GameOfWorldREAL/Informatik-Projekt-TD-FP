using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float damage;
    public float radius;
    public int lifeTime;
    public LayerMask lm;
    [HideInInspector]
    public GameObject target;

    void OnTriggerEnter(Collider col)             //On Collision
    {
        //Creat collision sphere -> detect all objects in this sphere
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, lm, QueryTriggerInteraction.UseGlobal);

        for (int i = 0; i < hitColliders.Length; i++)                 //for every Enemy gameObject
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                hitColliders[i].gameObject.GetComponent<Enemy>().SubEnemyHealth((int)damage);    //subHealth
                
            }
        }
        Destroy(this.gameObject);
    }
    
    void Update()
    {
        try
        {
            transform.LookAt(target.transform.position);
            this.GetComponent<Rigidbody>().velocity = transform.forward * Vector3.Magnitude(this.GetComponent<Rigidbody>().velocity);
        }
        catch { }
        Destroy(gameObject, lifeTime);
    }
}