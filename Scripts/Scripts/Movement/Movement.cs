﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class Movement : MonoBehaviour
{
    public GameObject body;
    public GameObject pHPivot;
    public Camera cam;
    [Space]
    public float moveSpeedForwRun = 10;
    public float moveSpeedForw = 5;
    public float moveSpeedBack = 5;
    public float accelForw = 5;
    public float accelBack = 2.5f;
    private bool richtung1 = false;
    private float actuellSpeedZ = 0;
    [Space]
    public float moveSpeedSide = 5;
    public float accelSide = 5;
    private bool richtung2 = false;
    private float actuellSpeedX = 0;
    [Space]
    public float jumpForce = 100;
    [Tooltip("In Percent")]
    public float senseOnJump = 25;
    [HideInInspector]
    public bool isGrounded = true;

    private bool wPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;
    private bool aPressed = false;
    private bool rPressed = false;
    private float camSensX;
    private float camSensY;
    private SmoothMouseLook camLook;
    [Space]
    public bool wAllowed = true;
    public bool sAllowed = true;
    public bool dAllowed = true;
    public bool aAllowed = true;
    private bool rAllowed = true;

    private float g;
    Rigidbody rb;
    Thread t1;
    private bool threadFrameCounter = false;

    void Thread1Method()
    {
        while (true)
        {
            if (threadFrameCounter == true)
            {
                FrontDirectionZ();
                Break();
                SideDirectionX();
                threadFrameCounter = false;
            }
        }
    }

    void Start()
    {
        Cursor.visible = false;
        camLook = cam.GetComponent<SmoothMouseLook>();
        camSensX = camLook.sensitivityX;
        camSensY = camLook.sensitivityY;

        t1 = new Thread(Thread1Method);
        t1.Start();

        rb = body.GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(6, 31);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        PressedKey();
        threadFrameCounter = true;

        rb.velocity = body.transform.forward * actuellSpeedZ +
                      body.transform.right * actuellSpeedX + new Vector3(0, rb.velocity.y, 0);
    }

    void PressedKey()
    {
        if (wAllowed == true && Input.GetKey(KeyCode.W) && sPressed == false)     //abfrage ob die Taste gedrueckt ist um fehler zu vermeiden andere nicht
        {
            wPressed = true;                      //setze auf gedrueckt
            richtung1 = true;                     //setze auf vorwaerts
        }
        else
        {
            wPressed = false;                    //setze auf nicht gedrueckt
        }

        if (sAllowed == true && Input.GetKey(KeyCode.S) && wPressed == false)
        {
            sPressed = true;
            richtung1 = false;         //setze auf rueckwaerts
        }
        else
        {
            sPressed = false;
        }

        if (dAllowed == true && Input.GetKey(KeyCode.D) && aPressed == false)     //abfrage ob die Taste gedrueckt ist um fehler zu vermeiden andere nicht
        {
            dPressed = true;                      //setze auf gedrueckt
            richtung2 = true;                     //setze auf vorwaerts
        }
        else
        {
            dPressed = false;                    //setze auf nicht gedrueckt
        }

        if (aAllowed == true && Input.GetKey(KeyCode.A) && dPressed == false)
        {
            aPressed = true;
            richtung2 = false;         //setze auf rueckwaerts
        }
        else
        {
            aPressed = false;
        }

        if (rAllowed == true && Input.GetKey(KeyCode.LeftShift))
        {
            rPressed = true;
        }
        else
        {
            rPressed = false;
        }
    }


    void FrontDirectionZ()
    {
        //VORWAERTS

        if (wPressed == true)
        {
            if (rPressed == true)
            {
                if (actuellSpeedZ < moveSpeedForwRun)            //moveSpeed als Maximalwert
                {
                    actuellSpeedZ += accelForw / 10;             //Neuer Beschleunigungswert "/10" damit es angenehmere werte gibt
                }

                else if (actuellSpeedZ > moveSpeedForwRun)       //Maximalwert moveSpeed soll nicht ueberschritten werden
                {
                    actuellSpeedZ = moveSpeedForwRun;            //Annahme des Maximalwerts bei ueberschreiten
                }
            }
            else
            {
                if (actuellSpeedZ < moveSpeedForw)               //moveSpeed als Maximalwert
                {

                    actuellSpeedZ += accelForw / 10;

                }
                else if (actuellSpeedZ > moveSpeedForw)          //Maximalwert moveSpeed soll nicht ueberschritten werden
                {
                    actuellSpeedZ = moveSpeedForw;               //Annahme des Maximalwerts bei ueberschreiten
                }
            }
        }

        //RUECKWAERTS

        // Grundlegend invertiert zu Vorwaerts d.h mit negativen Werten und Abfragen statt positiven
        if (sPressed == true)
        {

            if (actuellSpeedZ > moveSpeedBack * -1)
            {
                actuellSpeedZ -= accelForw / 10;
            }

            else if (actuellSpeedZ < moveSpeedBack * -1)

            {
                actuellSpeedZ = moveSpeedBack * -1;
            }
        }
    }


    void SideDirectionX()
    {
        //RECHTS

        if (dPressed == true)
        {
            // wenn die Taste gedrueckt wird :
            if (actuellSpeedX < moveSpeedSide)             //moveSpeed als Maximalwert
            {
                actuellSpeedX += accelSide / 10;          //Neuer Beschleunigungswert "/10" damit es angenehmere werte gibt
            }

            else if (actuellSpeedX > moveSpeedSide)       //Maximalwert moveSpeed soll nicht ueberschritten werden
            {
                actuellSpeedX = moveSpeedSide;            //Annahme des Maximalwerts bei ueberschreiten
            }
        }

        //LINKS
        // Grundlegend invertiert zu Vorwaerts d.h mit negativen Werten und Abfragen statt positiven
        if (aPressed == true)
        {

            if (actuellSpeedX > moveSpeedSide * -1)
            {
                actuellSpeedX -= accelSide / 10;
            }

            else if (actuellSpeedX < moveSpeedSide * -1)

            {
                actuellSpeedX = moveSpeedSide * -1;
            }
        }
    }


    void Break()
    {
        //"VORWAERTS" BREMSEN
        //Bremmsverhalten bei bewegungsrictung nach vorne
        if (wPressed == false && sPressed == false && richtung1 == true)
        {
            if (actuellSpeedZ > 0)             //actuellSpeedZ soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedZ -= accelBack / 10;

            }

            else if (actuellSpeedZ < 0)        //actuellSpeedZ darf nicht kleiner als 0 sein da sich sonst das Objekt Rueckwaerts bewegt
            {
                actuellSpeedZ = 0;           //Annahme des Wertes 0
            }
        }

        //"RUECKWAERTS" BREMSEN
        //Bremmsverhalten bei bewegungsrictung nach hinten
        if (sPressed == false && wPressed == false && richtung1 == false)
        {
            if (actuellSpeedZ < 0)             //actuellSpeed soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedZ += accelBack / 10;  //verringert die geschwindigkeit
            }
            else if (actuellSpeedZ > 0)        //aktuellSpeed darf nicht groesser als 0 sein da sich sonst das Objekt vorwaerts bewegt
            {
                actuellSpeedZ = 0;             //Annahme des Wertes 0
            }
        }

        //Identisch: in X Richtuing
        //"RECHTS" BREMSEN
        if (dPressed == false && aPressed == false && richtung2 == true)
        {
            if (actuellSpeedX > 0)             //actuellSpeedX soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedX -= accelSide / 10;  //verringert die geschwindigkeit
            }

            else if (actuellSpeedX < 0)        //actuellSpeedX darf nicht kleiner als 0 sein da sich sonst das Objekt Rueckwaerts bewegt
            {
                actuellSpeedX = 0;           //Annahme des Wertes 0
            }
        }

        //"LINKS" BREMSEN
        if (aPressed == false && dPressed == false && richtung2 == false)
        {
            if (actuellSpeedX < 0)             //actuellSpeedX soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedX += accelSide / 10;  //verringert die geschwindigkeit
            }
            else if (actuellSpeedX > 0)        //aktuellSpeedX darf nicht groesser als 0 sein da sich sonst das Objekt vorwaerts bewegt
            {
                actuellSpeedX = 0;             //Annahme des Wertes 0
            }
        }
    }
}