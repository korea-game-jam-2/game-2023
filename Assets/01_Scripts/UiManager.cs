using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject[] emptyPuzzle;
    public GameObject[] onPuzzle;

    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < emptyPuzzle.Length; i++)
        {
            if (playerController.isPuzzle[i])
            {
                emptyPuzzle[i].SetActive(false);
                onPuzzle[i].SetActive(true);
            }
        }
    }
}
