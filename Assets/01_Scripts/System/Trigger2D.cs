using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger2D : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public LayerMask layerMask;
    public bool justOnce;

    private bool _isPlayed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (justOnce && _isPlayed) return;

        if (1 << collision.gameObject.layer == layerMask) {
            _isPlayed = true;
            onTriggerEnter.Invoke();
        }
    }
}
