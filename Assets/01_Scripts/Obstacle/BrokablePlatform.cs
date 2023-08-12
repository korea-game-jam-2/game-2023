using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokablePlatform : MonoBehaviour
{
    public Animator animator = null;
    public List<Rigidbody2D> pieces = null;

    public float sustainTime = 1f;

    private float _playTime = 0f;
    private bool _isPlayerStanding = false;
    private bool _isBroken = false;

    void Update()
    {
        if (!_isPlayerStanding) return;
        if (_isBroken) return;

        _playTime += Time.deltaTime;
        if(sustainTime < _playTime)
        {
            _isBroken = true;
            animator.SetTrigger("isBreak");
            pieces.ForEach(rb2D => rb2D.bodyType = RigidbodyType2D.Dynamic);
            Invoke(nameof(ResetState), 5f);
        }
    }
    private void ResetState() {
        _isBroken = false;
        _isPlayerStanding = false;
        _playTime = 0f;
        pieces.ForEach(rb2D => rb2D.bodyType = RigidbodyType2D.Static);
        animator.SetTrigger("isIdle");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _isPlayerStanding = true;
            animator.SetTrigger("isDither");
        }
        
    }
}
