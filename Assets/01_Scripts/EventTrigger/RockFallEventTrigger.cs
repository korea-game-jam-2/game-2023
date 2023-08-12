using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallEventTrigger : MonoBehaviour
{
    public List<GameObject> RockBodies = null;

    private bool _isPlayed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPlayed) return;


        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _isPlayed = true;

            RockBodies.ForEach(rb2D => rb2D.SetActive(true));
        }
    }
}
