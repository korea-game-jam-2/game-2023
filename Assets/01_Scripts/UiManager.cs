using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    
    public GameObject[] health;
    public GameObject[] emptyHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthDown(int hp)
    {
        health[hp].SetActive(false);
        emptyHealth[hp].SetActive(true);
    }
}
