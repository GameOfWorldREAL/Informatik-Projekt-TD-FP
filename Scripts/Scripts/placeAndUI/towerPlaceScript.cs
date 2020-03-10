using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerPlaceScript : MonoBehaviour
{
    [HideInInspector]
    public bool destroy = false;               //destroy command
    [HideInInspector]
    public GameObject player;
    public LayerMask layerM;
    Collider[] hitColliders;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(destroy == true)               //destroy it self by command
        {
            Destroy(this.gameObject);
        }

        //Create Box around Tower and detect all collisions in array: box have to be under pivot Object
        hitColliders = Physics.OverlapBox(gameObject.transform.position, GetComponent<BoxCollider>().size/2, transform.rotation, layerM);

        for(int i = 0;i < hitColliders.Length; i++)    //Test if not allowed place Statements existing
        {
            if (hitColliders[i].gameObject.tag != "Ground")
            {
                try
                {
                    player.GetComponent<placeAndUI>().onlyGroundCollision = false;
                }
                catch { }
                break;
            }
            else
            {
                try
                {
                    player.GetComponent<placeAndUI>().onlyGroundCollision = true;
                }
                catch { }
            }
        }
    }
}
