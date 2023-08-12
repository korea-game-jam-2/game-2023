using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    
    public GameObject[] health;
    public GameObject[] emptyHealth;

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
