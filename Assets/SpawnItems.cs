using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private List<int> wplist= new List<int>();
    
    
    void Start()
    {
        SpawnObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObj()
    {
        wplist = new List<int>(new int[SpawnPoints.Length]);
        for (int i = 0; i < CodeLock.instance.array.Length; i++)
        {
            int spawnIndex = Random.Range(1, SpawnPoints.Length+1);
            while (wplist.Contains(spawnIndex))
            {
                spawnIndex = Random.Range(1, SpawnPoints.Length+1);
            }

            wplist[i] = spawnIndex;
            var myNewNum=Instantiate(CodeLock.instance.array[i], SpawnPoints[wplist[i]-1].position, Quaternion.identity);
            myNewNum.name = CodeLock.instance.array[i].name;
            myNewNum.transform.parent=gameObject.transform;
            CodeLock.instance.array[i] = myNewNum;
        }
        

        
             
        
        
    }
}
