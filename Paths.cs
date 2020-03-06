using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Paths : MonoBehaviour
{
    public int aw;          //Wellenazahl

    public int wen;         //Gegneranzahlen
    public int xen;
    public int yen;
    public int zen;

    public int wplus;       //Anzahl der Erhöhung der Generzahlen
    public int xplus;
    public int yplus;
    public int zplus;

    public int w;  // nextPoint Variable
    public int x;
    public int y;
    public int z;

    Thread waveThread;
    Thread waveAmountThread;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void Run()       //Erzeugung der Gegner
    {
        for (int i = 0; i == wen; i++){
          // GameObject ew = new GameObject();
            GameObject ew = gameObject.Tag("Enemy");
            ew.gameObject.GetComponent<EnemyMoveScript>().moveTo = next;
        }
        for (int j = 0; j == xen; j++){
          // GameObject ex = new GameObject();
            GameObject ex = gameObject.Tag("Enemy");
            ex.gameObject.GetComponent<EnemyMoveScript>().moveTo = next;
        }
        for (int k = 0; k == yen; k++){
          //  GameObject ey = new GameObject();
            GameObject ey = gameObject.Tag("Enemy");
            ey.gameObject.GetComponent<EnemyMoveScript>().moveTo = next;
        }
        for (int h = 0; h == zen; h++){
          //  GameObject ez = new GameObject();
            GameObject ez = gameObject.Tag("Enemy");
            ez.gameObject.GetComponent<EnemyMoveScript>().moveTo = next;
        }
        SetEnemyNumber();
    }
     
    public void Beginn()        //Thread für Welle
    {
        Thread waveThread;
        waveThread = new Thread (Run);
        waveThread.Start();
    }

    void SetAmountWave(int aw) {        //die Wellen werden mehrfach durchgeführt

        for (int l = 1; l == aw; l++)
        {
            Beginn();
        }
    }

    void wave () {          //Welle bzw. Feld wird erstellt

        int[] EnemyNumberField = new int[4] { wen, xen, yen, zen };     //die Ecken werden in das Feld eingegeben
    }

    void SetEnemyNumber(){          //Gegneranzahlen werden erhöht
        wen = wen + wplus;
        xen = xen + xplus;
        yen = yen + yplus;
        zen = zen + zplus;
    }
    public void CountWaves (){      //Thread lässt Wellen mehrfach ablaufen
        Thread waveAmountThread;
        waveAmountThread = new Thread(SetAmountWave(int aw));
        waveAmountThread.Start();
    }
}
