using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temple : MonoBehaviour
{
    public int health = 200; //Gesundheit 

    public void EndGame()
    {
        SceneManager.LoadScene("EndBad");
    }
    public void SubHealth(int damage)
    {
        health -= damage;
        if (health <= 0)  //Wenn der Tempel zerstört ist
        {
            EndGame();
        }
    }
}