using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public List<SavePoint> savePoints;
    public PlayerController playerController;

    private SavePoint _currentSavePoint;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if(savePoints.Count > 0)
        {
            _currentSavePoint = savePoints.First();
        }
    }
    public void SetSavePoint(SavePoint savePoint, int order) {
        if (_currentSavePoint.order <= order) { 
            _currentSavePoint = savePoint;
        }
    }
    public void Respawn() { 
        playerController.ResetState();
        playerController.transform.SetParent(null);
        playerController.transform.position = _currentSavePoint.transform.position;
    }
}
