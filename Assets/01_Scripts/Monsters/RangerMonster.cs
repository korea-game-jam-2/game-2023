using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerMonster : MonoBehaviour, IHitable
{
    public GameObject projectilePrefab = null;
    public SpriteRenderer spriteRenderer = null;
    public Animator animator = null;

    public float attackPerSecond = 3f;
    public Vector2 angle;
    public float shotForce = 1f;

    private float _coolTime = 0f;
    private bool _isDie = false;

    private void Start()
    {
        spriteRenderer.flipX = angle.x < 0f;
    }
    private void Update()
    {
        _coolTime -= Time.deltaTime;

        if (_coolTime < 0) {
            _coolTime = 1f / attackPerSecond;
            Shoot();
        }

        Debug.DrawLine(transform.position, (Vector2)transform.position + angle.normalized * shotForce, Color.blue);
    }

    private void Shoot()
    {
        if (_isDie) return;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(angle.normalized *  shotForce);
    }

    public void Hit(int damage)
    {
        _isDie = true;
        animator.SetTrigger("isDie");
        Destroy(gameObject, 1f);
    }
}
