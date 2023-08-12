using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask hitLayer;
    public float liveSeconds = 3f;
    public int damage = 1;
    
    private bool _isDone = false;
    private float _playTime = 0f;
    private void Update()
    {
        _playTime += Time.deltaTime;
        if (_playTime > liveSeconds) { 
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isDone)
        {
            if (1 << collision.gameObject.layer == hitLayer) {
                IHitable hitable = collision.gameObject.GetComponent<IHitable>();
                if (hitable != null)
                {
                    hitable.Hit(damage);
                }
            }
            
        }
        

        if (1<<collision.gameObject.layer == LayerMask.NameToLayer("Ground")) { 
            _isDone = true;
        }
    }
}
