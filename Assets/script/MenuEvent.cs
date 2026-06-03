using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvent : MonoBehaviour
{

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}