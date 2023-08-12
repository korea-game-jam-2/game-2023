using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public bool[] isPuzzle = new bool[3];

    public GameObject[] emptyPuzzle;
    public GameObject[] onPuzzle;

    public GameObject exlplonsion;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < emptyPuzzle.Length; i++)
        {
            if (isPuzzle[i] == true)
            {
                emptyPuzzle[i].SetActive(false);
                onPuzzle[i].SetActive(true);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Instantiate(exlplonsion, transform.position, Quaternion.identity);
            if (gameObject.tag == "Eye")
            {
                Destroy(gameObject);
                isPuzzle[0] = true;
                emptyPuzzle[0].SetActive(false);
                onPuzzle[0].SetActive(true);
            }
            if (gameObject.tag == "Comb")
            {
                Destroy(gameObject);
                isPuzzle[1] = true;
                emptyPuzzle[1].SetActive(false);
                onPuzzle[1].SetActive(true);
            }
            if(gameObject.tag == "Beak")
            {
                Destroy(gameObject);
                isPuzzle[2] = true;
                emptyPuzzle[2].SetActive(false);
                onPuzzle[2].SetActive(true);
            }
        }

    }
}
