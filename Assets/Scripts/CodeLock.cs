using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CodeLock : MonoBehaviour
{
    public int codeLength;
    public int placeInCode;
    public string code = "";
    public string attemtedCode;
    public GameObject[] array;
    public List<String> arx = new List<String>();
    public List<int> s = new List<int>();
    public Transform toOpen;

    public static CodeLock instance;

    public CameraShake cameraShake;
    public GameObject padLock;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        s = new List<int>(new int[array.Length]);
        for (int i = 0; i < array.Length; i++)
        {
            var x = Random.Range(1, array.Length+1 );
            while (s.Contains(x))
            {
                 x = Random.Range(1, array.Length +1);
            }

            s[i] = x;
            arx.Add(array[s[i]-1].gameObject.name);
            
        }
        
        foreach (var a in arx)
        {
            code += a;
        }
        
        codeLength = code.Length;
    }
    
    
    public void CheckCode()
    {
        if (attemtedCode == code)
        {
            //StartCoroutine(Open());
            Explosion.instance.explode();
            Debug.Log("true code");
            //todo when can hit in order
            attemtedCode = "";
            placeInCode = 0;
        }
        else if (attemtedCode!=code && placeInCode<=codeLength)
        {
            StartCoroutine(cameraShake.Shake(0.8f,0.1f));
            attemtedCode = "";
            placeInCode = 0;
            Debug.Log("Wrong Code");
            //todo when cannot hit in order
        }
    }

    IEnumerator Open()
    {
        //todo what is gonna happen when the player hits in order
        toOpen.Rotate( new Vector3(0,90,0),Space.World);
        padLock.transform.position = new Vector3(padLock.transform.position.x, padLock.transform.position.y +.6f, padLock.transform.position.z);
        yield return new WaitForSeconds(4f);
        toOpen.Rotate( new Vector3(0,-90,0),Space.World);
    }
     public void SetValue(string value)
     {
         placeInCode++;
         
         if (placeInCode <= codeLength)
         {
             attemtedCode += value;
             
            
         }
         
         /*if (placeInCode == codeLength )
         {
             CheckCode();

            // attemtedCode = "";
             //placeInCode = 0;
         }*/
     }
}
