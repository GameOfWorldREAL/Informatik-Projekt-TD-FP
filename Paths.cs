using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Paths : MonoBehaviour
{
    public int aw;

    public int wen;
    public int xen;
    public int yen;
    public int zen;

    public int wplus;
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
      // timeTime.SECONDS.sleep(1);  //Wartezeit?

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void Run()
    {
        for (int i = 0; i == wen; i++){
            Enemy ew = new Enemy();
        }
        for (int j = 0; j == xen; j++){
            Enemy ex = new Enemy();
        }
        for (int k = 0; k == yen; k++){
            Enemy ey = new Enemy();
        }
        for (int h = 0; h == zen; h++){
            Enemy ez = new Enemy();
        }
    }
     
    public void Beginn()
    {
        Thread waveThread;
        waveThread = new Thread (Run);
        waveThread.Start();
    }

            // void Path() {
            //nextPoint

            // }

    void SetAmountWave(int aw) {

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
    public void CountWaves (){
        Thread waveAmountThread;
        waveAmountThread = new Thread(SetAmountWave(int aw));
        waveAmountThread.Start();
    }
}
