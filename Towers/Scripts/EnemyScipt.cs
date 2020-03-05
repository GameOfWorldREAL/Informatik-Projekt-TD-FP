using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScipt : MonoBehaviour
{
    public int health;
    public int damage;
    public Vector3 nextPoint;
    public float speed;
    public int lifeTime;
    public float radius = 2;
    public LayerMask lm;
    public string tagName = "Temple";


    public void SubEnemyHealth()                //für Projektile, die den Enemy treffen; zieht Lebenspunkte ab
    {
        health -= health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ja");
        if (health == 0)                          //Absicherung, dass Enemy zerstört wird
        {
            Destroy(gameObject);
            Debug.Log("Enemy hat kein Leben mehr");
        }
        Destroy(gameObject, lifeTime);          //das GameObjekt wird nach einer gewissen Zeit zerstört (falls es in der Map festhängt)
        this.transform.position += nextPoint * speed * Time.deltaTime; //Bewegung des Enemys
    }
}
