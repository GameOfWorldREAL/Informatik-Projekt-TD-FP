using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class zoomOut : MonoBehaviour
{
    public Camera cam;                 //camera
    public GameObject player;          //player

    public Vector3 offsetMove1;        //first point relative player
    public Vector3 offsetMove2;        //second point relative first point
    public float moveSpeed;
    [Space]
    public Vector3 globalRotation;     //final rotation
    public float rotateSpeed;
    Vector3 state0Point;               //creates relative to player points from offsetMove1/2
    Vector3 state1Point;
    Vector3 state2Point;
    Vector3 moveDirection1;            //Vector of move direction
    Vector3 moveDirection2;
    Quaternion state0Roation;          //actual rotation
    bool map = false;            
    bool moveInProcess = false;
    bool rotateInProcess = false;
    bool stopMoveing = false;
    bool stopRotate = false;
    float MapDelay;
    int state=1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.M) == true && Time.time > MapDelay)         //Switch betwenn zoom out and in
        {
            if (map == false)
            {
                map = true;
            }
            else
            {
                map = false;
                stopRotate = false;
            }
            MapDelay = Time.time + 0.4f;
        }
        if (moveInProcess == false && map == false)                          //reset player changes/ give player controll back
        {
            player.GetComponent<Movement>().enabled = true;
            cam.GetComponent<SmoothMouseLook>().enabled = true;
            player.GetComponent<placeAndUI>().enabled = true;
        }

        MoveCam();          
        RotateCam();
    }

    private void RotateCam()
    {
        if (map == true)
        {
            if (rotateInProcess == false) 
            {
                state0Roation = cam.transform.rotation;          //save start rot 
                rotateInProcess = true;                          //start process
            }
            if (rotateInProcess == true)
            {
                if (Vector3.Distance(cam.transform.eulerAngles, globalRotation) > 0.1f)               //rotate over time to global rotation
                {
                    cam.transform.rotation =
                        Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(globalRotation), rotateSpeed * Time.deltaTime);
                }
                else
                {
                    cam.transform.eulerAngles = globalRotation;
                }
            }
        }
        else                                                       //rotate back to old rotation
        { 
            if (rotateInProcess == true)
            {
                if (Vector3.Distance(cam.transform.eulerAngles, state0Roation.eulerAngles) > 0.1f)
                {
                    cam.transform.rotation =
                        Quaternion.Lerp(cam.transform.rotation, state0Roation, rotateSpeed * Time.deltaTime);
                }
                else
                {
                    cam.transform.rotation = state0Roation;
                    rotateInProcess = false;
                }
            }
        }
    }

    private void MoveCam()
    {
        if (map == true)
        {
            if (moveInProcess == false && player.GetComponent<Movement>().isGrounded == true)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);            //Disable player controll and set velocety to 0
                cam.GetComponent<SmoothMouseLook>().enabled = false;
                player.GetComponent<placeAndUI>().enabled = false;
                player.GetComponent<Movement>().enabled = false;

                state0Point = cam.transform.position;                                        //Creates relative points to player
                state1Point =
                  Vector3.Normalize(new Vector3(cam.transform.right.x, 0, cam.transform.right.z)) * offsetMove1.x +
                  new Vector3(0, 1, 0) * offsetMove1.y +
                  Vector3.Normalize(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z)) * offsetMove1.z
                   + cam.transform.position;
                state2Point =
                 Vector3.Normalize(new Vector3(cam.transform.right.x, 0, cam.transform.right.z)) * offsetMove2.x +
                  new Vector3(0, 1, 0) * offsetMove2.y +
                  Vector3.Normalize(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z)) * offsetMove2.z
                   + state1Point;

                moveDirection1 = Vector3.Normalize(state1Point - state0Point);
                moveDirection2 = Vector3.Normalize(state2Point - state1Point);

                stopMoveing = false;
                moveInProcess = true;              //start process   
                rotateInProcess = false;           //reset rotation start
            }
            else if(moveInProcess == false)
            {
                stopMoveing = true;              //start process
            }
            if (stopMoveing == false)
            {
                if (state == 1)                  //move to first point
                {
                    cam.transform.position += moveDirection1 * moveSpeed * Time.deltaTime;

                    if (Vector3.Normalize(state1Point - cam.transform.position).Equals(-moveDirection1))  //Sicherheits Abfrage
                    {
                        cam.transform.position = state1Point;
                        state = 2;
                    }
                    else if (Vector3.Distance(cam.transform.position, state1Point) < 0.7)   //Besserer Übergang
                    {
                        state = 2;
                    }
                }
                else if (state == 2 && stopMoveing == false)  //second point
                {
                    cam.transform.position += moveDirection2 * moveSpeed * Time.deltaTime;

                    if (Vector3.Normalize(state2Point - cam.transform.position).Equals(-moveDirection2))  //Sicherheits Abfrage
                    {
                        cam.transform.position = state2Point;
                        stopMoveing = true;

                    }
                    else if (Vector3.Distance(cam.transform.position, state2Point) < 0.7)   //Besserer Übergang
                    {
                        stopMoveing = true;
                    }
                }
            }
        }
        else
        {
            if (moveInProcess == true)           //rückwärts zu oben
            {
                stopMoveing = false;

                if (state == 1) 
                {
                    cam.transform.position += -moveDirection1 * moveSpeed * Time.deltaTime;

                    if (Vector3.Normalize(state0Point - cam.transform.position).Equals(moveDirection1))  //Sicherheits Abfrage
                    {
                        cam.transform.position = state0Point;
                        moveInProcess = false;
                    }
                    else if (Vector3.Distance(cam.transform.position, state0Point) < 0.7)   //Besserer Übergang
                    {
                        cam.transform.position = state0Point;
                        moveInProcess = false;
                    }
                }
                else if (state == 2)
                {
                    cam.transform.position += -moveDirection2 * moveSpeed * Time.deltaTime;

                    if (Vector3.Normalize(state1Point - cam.transform.position).Equals(moveDirection2))  //Sicherheits Abfrage
                    {
                        cam.transform.position = state1Point;
                        state = 1;

                    }
                    else if (Vector3.Distance(cam.transform.position, state1Point) < 0.7)   //Besserer Übergang
                    {
                        state = 1;
                    }
                }
            }
        }
    }
}
