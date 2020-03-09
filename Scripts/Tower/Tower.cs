using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{

    public GameObject target;
    public GameObject projectile;
    public float shootingTime;
    public float range;
    public float projectileVelocity;
    public LayerMask lm;
    public string enemyTagName = "Enemy";

    private float timeSinceLastShot = 0f;


    // Update is called once per frame
    void Update()
    {
        
        // rotate to target
        if (target != null)
        {
            //  Vector3 target_velocity = (target.GetComponent<Rigidbody>().velocity.magnitude / projectileVelocity) * target.GetComponent<Rigidbody>().velocity; // velocity of enemy target
            Vector3 relativePosition = target.transform.position - transform.position;      // vector between target enemy and tower
            relativePosition.y = 0.0f;          // not rotation on y-axis


            Quaternion look = Quaternion.LookRotation(relativePosition);
            transform.rotation = look * Quaternion.Euler(0,180,0);

            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot > shootingTime)
            {
                Shoot();
            }
        }
        FindEnemy();
        Outside_Update();


    }

    // Update Function for children
    abstract protected void Outside_Update();

    
    // Target enemy is out of range
    void OnTriggerExit()
    {
        target = null;
        FindEnemy();
    }
    
    // TODO: INTERFACE TO TARGET ENEMY (ACTION FUNCTION WHEN DEAD)
    void OnTargetEnemyDead()
    {
        target = null;
        FindEnemy();
    }
    
    void FindEnemy()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, lm, QueryTriggerInteraction.UseGlobal);
        float currentAge = 10000;      // find "oldest" enemy -> the first enemy
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == enemyTagName)
            {
                if (hitColliders[i].gameObject.GetComponent<Enemy>().lifeTime < currentAge)
                {
                    currentAge = hitColliders[i].gameObject.GetComponent<Enemy>().lifeTime;
                    target = hitColliders[i].gameObject;    // set target to oldest enemy
                }
            }
        }
    }

    void Shoot()
    {
        timeSinceLastShot = 0;
        initiateProjectile();
    }

    abstract protected void initiateProjectile();
}
