using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple : MonoBehaviour
{
    //public GameObject temple;
    public int health = 200; //Gesundheit 
    //private int wellenanzahl;
    //private int gegneranzahl;
    
    //public void SubGegneranzahl(int anzahl)
    //{
    //    gegneranzahl -= anzahl;
    //    if (gegneranzahl==0)
    //    {
            //Starte nächste Welle
    //    }
    //}
    //public void StartNextWave()
    //{
    //    if (wellenanzahl > 0)
    //    {
            //SendMessage an Spawnppunkte neue Welle zu starten
    //        wellenanzahl--;
    //    }
    //    else 
    //    {
    //        EndGame();
    //    }
    //}
    public void EndGame() 
    {
        //Szenenwechsel zu Startbildschirm
    }
    public void SubHealth(int damage)
    {
        health -= damage;
        if (health <= 0)  //Wenn der Tempel zerstört ist
        {
            EndGame();
        }
    }
    void Start()
    {
        
        //Lasse Gegneranzahl von Welle übergeben
    }

    // Update is called once per frame
    void Update()
    {      
        //Bezahlsystem
    }
}
