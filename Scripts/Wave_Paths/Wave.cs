using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    public int waveCount;
    public int wave = 1;
    private int lastWave;
    public int enemySpawned;
    public int enemyKilled;
    public int timeOut = 2;
    public int maxWaveTime;

    public bool inProcess;
    float actualTime;
    float time;
    float timeEND;
    // Start is called before the first frame update
    void Start()
    {
        time = timeOut + Time.time + 3;
        timeEND = maxWaveTime + Time.time;
        lastWave = wave;
        inProcess = true;
        actualTime = Time.time;                    //for Thread
    }

    // Update is called once per frame
    void Update()
    {
        if (wave == lastWave)
        {
            if (inProcess == false)
            {  
                if (enemyKilled == enemySpawned)
                {
                    if (waveCount > wave)
                    {
                        wave += 1;
                        return;
                    }
                    else
                    {
                        SceneManager.LoadScene("EndGood");
                    }
                }
            }
            if(Time.time <= time)
            {
                inProcess = true;
            }
            else
            {
                inProcess = false;
                //wave++;
            }

            if (Time.time >= timeEND)
            {
                if (waveCount > wave)
                {
                    wave += 1;
                    return;
                }
                else
                {
                    SceneManager.LoadScene("EndGood");
                }
                Debug.LogWarning("Something went wrong");
            }
        }
        else
        {
            time = timeOut + Time.time;
            timeEND = maxWaveTime + Time.time;
            lastWave = wave;
            inProcess = true;
        }

        actualTime = Time.time;
    }

    public void aktualisateProcess()
    {
        inProcess = true;
        time = timeOut + actualTime;
    }
}
