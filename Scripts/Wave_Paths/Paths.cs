using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Paths : MonoBehaviour
{
    public int[] enemy1;
    public int[] enemy2;
    public int[] enemy3;

    public int delay;

    public GameObject player;

    bool enem1 = false;
    bool enem2 = false;
    bool enem3 = false;

    public GameObject next;
    public GameObject[] enemy = new GameObject[3];

    Wave waveScript;
    Thread waveThread;
    int wave;

    // Start is called before the first frame update
    void Start()
    {
       
        waveScript = player.GetComponent<Wave>();
        wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enem1 == true)
        {
            GameObject obj = Instantiate(enemy[0], transform.position, Quaternion.Euler(0, 0, 0));
            obj.GetComponent<EnemyMoveScript>().moveTo = next;
            obj.GetComponent<Enemy>().player = player;
            obj.GetComponent<EnemyMoveScript>().enabled = true;
            player.GetComponent<Wave>().enemySpawned += 1;
            enem1 = false;
            
        }
        if (enem2 == true)
        {
            GameObject obj = Instantiate(enemy[1], transform.position, Quaternion.Euler(0, 0, 0));
            obj.GetComponent<EnemyMoveScript>().moveTo = next;
            obj.GetComponent<Enemy>().player = player;
            obj.GetComponent<EnemyMoveScript>().enabled = true;
            player.GetComponent<Wave>().enemySpawned += 1;
            enem2 = false;
        }
        if (enem3 == true)
        {
            GameObject obj = Instantiate(enemy[2], transform.position, Quaternion.Euler(0, 0, 0));
            obj.GetComponent<EnemyMoveScript>().moveTo = next;
            obj.GetComponent<Enemy>().player = player;
            obj.GetComponent<EnemyMoveScript>().enabled = true;
            player.GetComponent<Wave>().enemySpawned += 1;
            enem3 = false;
        }

        NewWave();
    }

    public void Run()       //Erzeugung der Gegner
    {
        for (int i = 0; i < enemy1[wave-1]; i++){
            if (enem1 == false)
            { 
                Thread.Sleep(delay * 1000);
                waveScript.aktualisateProcess();
                enem1 = true;
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < enemy2[wave - 1]; i++)
        {

            if (enem2 == false)
            {
                
                Thread.Sleep(delay * 1000);
                waveScript.aktualisateProcess();
                enem2 = true;
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < enemy3[wave - 1]; i++)
        {

            if (enem3 == false)
            {
                
                
                Thread.Sleep(delay * 1000);
                waveScript.aktualisateProcess();
                enem3 = true;
            }
            else
            {
                i--;
            }
        }
    }
     
    public void Beginn()        //Thread für Welle
    {
        Thread waveThread;
        waveThread = new Thread (Run);
        waveThread.Start();
    }

    void NewWave() {        //die Wellen werden mehrfach durchgeführt

        if (wave != waveScript.wave)
        {
            if (Input.GetKey(KeyCode.T) == true)
            {
                wave = waveScript.wave;
                Beginn();
            }
            else
            {
                waveScript.aktualisateProcess();
            }
        }
    }   
}
