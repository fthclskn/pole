using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Restart;
    public bool isAttached;
    public bool StopMoving=false;
   
    private void Awake()
    {
        if(!Instance)
            Instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (StopMoving)
        {
            FindObjectOfType<DetectShapes>().ZaxisVelocity = 0f;
        }
    }

    public IEnumerator GameCompleteMethod()
    {
        yield return new WaitForSeconds(.3f);
        Restart.SetActive(true);

    }
}
