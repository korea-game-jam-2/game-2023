using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    PlayerController controller;
    public GameObject[] health;
    public GameObject[] emptyHealth;

    void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(controller.hp < 0){
            for(int i=0; i<health.Length; i++) { 
                health[i].SetActive(false);
                emptyHealth[i].SetActive(true);
            }
        }

        if(controller.hp == 3)
        {
            for(int i = 0; i<health.Length;i++)
            {
                health[i].SetActive(true);
                emptyHealth[i].SetActive(false);
            }
        }
    }

    public void HealthDown(int hp)
    {
        health[hp].SetActive(false);
        emptyHealth[hp].SetActive(true);
    }
    public void HealthUp(int hp)
    {
        health[hp].SetActive(true);
        emptyHealth[hp].SetActive(false);
    }
}
