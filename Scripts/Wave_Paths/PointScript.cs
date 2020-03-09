using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    public GameObject next;
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
                hitColliders[i].gameObject.GetComponent<EnemyMoveScript>().moveTo = next;
            }
        }
        //Debug.Log("Erkannt");
    }
}

