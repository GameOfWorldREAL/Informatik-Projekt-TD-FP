using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anleitung : MonoBehaviour { 

    public void SceneLoader(int scenel)
    {
        SceneManager.LoadScene(scenel);
        }


}