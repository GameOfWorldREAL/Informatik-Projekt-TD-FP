using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float damage = 10;
    public float radius = 20;
    public LayerMask lm;

    void OnCollisionEnter(Collision col)             //On Collision
    {
        //Creat collision sphere -> detect all objects in this sphere
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, lm, QueryTriggerInteraction.UseGlobal);

        for(int i = 0; i < hitColliders.Length; i++)                 //for every Enemy gameObject
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                hitColliders[i].gameObject.GetComponent<HealthScript>().health = damage;    //subHealth
                Debug.Log(hitColliders.Length);
            }
        }

        Destroy(this.gameObject);       //Destroy projectile
    }
}