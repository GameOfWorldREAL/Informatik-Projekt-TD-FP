using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class movement : MonoBehaviour
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
    private bool isGrounded = true;
    private float playerHeight = 0;

    private bool wPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;
    private bool aPressed = false;
    private bool rPressed = false;
    private bool spacePressed = false;
    private float camSensX;
    private float camSensY;
    private SmoothMouseLook camLook;
    [Space]
    public bool wAllowed = true;
    public bool sAllowed = true;
    public bool dAllowed = true;
    public bool aAllowed = true;
    public bool spaceAllowed = true;
    private bool rAllowed = true;

    private bool isJumping = false;
    private bool isFalling = false;

    private float g;

    private Vector3 cOldV3 = new Vector3(0, 0, 0);
    private Vector3 bOldV3 = new Vector3(0, 0, 0);
    private Vector3 kOldV3 = new Vector3(0, 0, 0);

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

    void FixedUpdate()
    {
        PressedKey();
        threadFrameCounter = true;

        HeightDetector();
        Jumping();

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
            dPressed = true;                     //setze auf gedrueckt
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

        if (spaceAllowed == true && Input.GetKey(KeyCode.Space))
        {
            spacePressed = true;
        }
        else
        {
            spacePressed = false;
        }

        if (rAllowed == true && Input.GetKey(KeyCode.R))
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
            if (rPressed == true && isJumping == false)
            {
                if (actuellSpeedZ < moveSpeedForwRun)            //moveSpeed als Maximalwert
                {
                    actuellSpeedZ += accelForw / 10;       //Neuer Beschleunigungswert "/10" damit es angenehmere werte gibt
                }

                else if (actuellSpeedZ > moveSpeedForwRun)       //Maximalwert moveSpeed soll nicht ueberschritten werden
                {
                    actuellSpeedZ = moveSpeedForwRun;            //Annahme des Maximalwerts bei ueberschreiten
                }
            }
            else if (isJumping == false)
            {
                if (actuellSpeedZ < moveSpeedForw)            //moveSpeed als Maximalwert
                {

                    actuellSpeedZ += accelForw / 10;

                }
                else if (actuellSpeedZ > moveSpeedForw)       //Maximalwert moveSpeed soll nicht ueberschritten werden
                {
                    actuellSpeedZ = moveSpeedForw;            //Annahme des Maximalwerts bei ueberschreiten
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
            if (actuellSpeedX < moveSpeedSide)            //moveSpeed als Maximalwert
            {
                actuellSpeedX += accelSide / 10;       //Neuer Beschleunigungswert "/10" damit es angenehmere werte gibt
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
        //Sicherheitsmassnahme für den fall einer negativen Beschleunigung nach vorme (rueckwaerts fahren obwohl es stoppen sollte) 
        //und um zu vermeiden dass gleichzeitig zum Bremsen (nichts Druecken) eine Beschleunigung nach hinten stattfindet (S druecken)
        //hier geht man von einem positiven actuellSpeed aus da richtung TRUE ist
        if (wPressed == false && sPressed == false && richtung1 == true && isJumping == false)
        {
            if (actuellSpeedZ > 0)             //actuellSpeed soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedZ -= accelBack / 10;

            }

            else if (actuellSpeedZ < 0)        //actuellSpeed darf nicht kleiner als 0 sein da sich sonst das Objekt Rueckwaerts bewegt
            {
                actuellSpeedZ = 0;           //Annahme des Wertes 0
            }
        }

        if (isJumping == true)
        {
            actuellSpeedZ -= accelBack / 300;
        }

        //"RUECKWAERTS" BREMSEN
        //Sicherheitsmassnahme für den fall einer positiven Beschleunigung nach hinten (vorwaerts fahren obwohl es stoppen sollte) 
        //und um zu vermeiden dass gleichzeitig zum Bremsen (nichts Druecken) eine Beschleunigung nach vorne stattfindet (W druecken) 
        //hier geht man von einem negativen actuellSpeed aus da richtung FALSE ist
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

        //Identisch in X Richtuing
        //"RECHTS" BREMSEN
        if (dPressed == false && aPressed == false && richtung2 == true)
        {
            if (actuellSpeedX > 0)             //actuellSpeed soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedX -= accelSide / 10;  //verringert die geschwindigkeit
            }

            else if (actuellSpeedX < 0)        //actuellSpeed darf nicht kleiner als 0 sein da sich sonst das Objekt Rueckwaerts bewegt
            {
                actuellSpeedX = 0;           //Annahme des Wertes 0
            }
        }

        //"LINKS" BREMSEN
        if (aPressed == false && dPressed == false && richtung2 == false)
        {
            if (actuellSpeedX < 0)             //actuellSpeed soll zum bremsen auf 0 gebracht werden
            {
                actuellSpeedX += accelSide / 10;  //verringert die geschwindigkeit
            }
            else if (actuellSpeedX > 0)        //aktuellSpeed darf nicht groesser als 0 sein da sich sonst das Objekt vorwaerts bewegt
            {
                actuellSpeedX = 0;             //Annahme des Wertes 0
            }
        }
    }


    void Jumping()
    {
        if (spacePressed == true && isGrounded == true && isJumping == false)
        {
            body.GetComponent<Rigidbody>().AddForce(body.transform.up * jumpForce, ForceMode.Impulse);

            isJumping = true;
            wAllowed = false;
            aAllowed = false;
            sAllowed = false;
            dAllowed = false;
            spaceAllowed = false;

            camLook.sensitivityX = camSensX * senseOnJump / 100;
            camLook.sensitivityY = camSensY * senseOnJump / 100;
        }

        else if (isGrounded == true && isJumping == true)
        {
            wAllowed = true;
            aAllowed = true;
            sAllowed = true;
            dAllowed = true;
            spaceAllowed = true;

            isJumping = false;

            camLook.sensitivityX = camSensX;
            camLook.sensitivityY = camSensY;
        }

    }


    void HeightDetector()
    {
        if (Physics.Raycast(pHPivot.transform.position, -transform.up, out RaycastHit hit, 100))
        {
            playerHeight = Vector3.Distance(hit.point, pHPivot.transform.position);

            if (playerHeight > 0.05)
            {
                isGrounded = false;
            }
            else
            {
                isGrounded = true;
            }

            if (isFalling == true)
            {
                playerHeight += -0.83f;
            }
        }
    }
}