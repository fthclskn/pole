using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasmanager : MonoBehaviour
{
    public static canvasmanager Instance;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
