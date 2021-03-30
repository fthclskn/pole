using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public Slider energyBar;

    public int maxEnergy = 100;
    public int currentEnergy;
    public RawImage barRawImage;

    public static EnergySystem instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentEnergy= maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = maxEnergy;
    }

   
    void Update()
    {
        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= 0.25f * Time.deltaTime;
        barRawImage.uvRect = uvRect;
    }

    public void UseEnergy(int amount)
    {
        if (currentEnergy- amount >=0)
        {
            currentEnergy -= amount;
            energyBar.value = currentEnergy;
        }
        else
        {
            Debug.Log("Not enough Energy");
        }
    }

    
}


