using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public bool[] isPuzzle = new bool[3];

    public GameObject[] onPuzzle;
    public GameObject exlplonsion;
    public GameObject pieceUi;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < onPuzzle.Length; i++)
        {
            if (isPuzzle[i] == true)
            {
                onPuzzle[i].SetActive(true);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player") {
            pieceUi.SetActive(true);
            Instantiate(exlplonsion, transform.position, Quaternion.identity);
            if (gameObject.tag == "Eye")
            {
                Destroy(gameObject);
                isPuzzle[0] = true;
                onPuzzle[0].SetActive(true);
            }
            if (gameObject.tag == "Comb")
            {
                Destroy(gameObject);
                isPuzzle[1] = true;
                onPuzzle[1].SetActive(true);
            }
            if(gameObject.tag == "Beak")
            {
                Destroy(gameObject);
                isPuzzle[2] = true;
                onPuzzle[2].SetActive(true);
            }
        }
    }
}
