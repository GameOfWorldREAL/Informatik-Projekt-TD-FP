using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float lifeTime;
    public int profit;
    public float radius = 2;
    public LayerMask lm;
    public string tagName = "Temple";
    [HideInInspector]
    public Rigidbody rb;
    public GameObject player;


    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, lm, QueryTriggerInteraction.UseGlobal);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == tagName)
            {
                hitColliders[i].gameObject.GetComponent<Temple>().SubHealth(damage);
                Destroy(gameObject);
                player.GetComponent<Wave>().enemyKilled += 1;
            }
        }
    }
    public void SubEnemyHealth(int dmg)                //für Projektile, die den Enemy treffen; zieht Lebenspunkte ab
    {
        health -= dmg;
        if (health <= 0)
        {
            player.GetComponent<CoinSystem>().increase(profit);
            player.GetComponent<Wave>().enemyKilled += 1;
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifeTime += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ja");
        if (health == 0)                          //Absicherung, dass Enemy zerstört wird
        {
            Destroy(gameObject);
        }
        if (Time.time > lifeTime)
        {
            Destroy(gameObject);          //das GameObjekt wird nach einer gewissen Zeit zerstört (falls es in der Map festhängt)
            player.GetComponent<Wave>().enemyKilled += 1;
        }
    }
}