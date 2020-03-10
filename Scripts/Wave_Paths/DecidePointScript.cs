using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecidePointScript : MonoBehaviour
{
    public GameObject next;
    public GameObject next2;
    public float radius = 5;
    public LayerMask lm;

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, lm, QueryTriggerInteraction.UseGlobal);     //die Punkte werden weitergegeben, wenn
        for (int i = 0; i < hitColliders.Length; i++)                                                                           //ein Gegner sich in der Sphäre befindet
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                if(Random.Range(0,2) == 0)
                {
                    hitColliders[i].gameObject.GetComponent<EnemyMoveScript>().moveTo = next;
                }
                else
                {
                    hitColliders[i].gameObject.GetComponent<EnemyMoveScript>().moveTo = next2;
                }
                
            }
        }
        //Debug.Log("Erkannt");
    }
}
