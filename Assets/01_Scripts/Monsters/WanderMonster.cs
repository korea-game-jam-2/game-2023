using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMonster : MonoBehaviour
{
    public float moveSpeed = 1f;

    private void Update()
    {
        Vector3 origin = transform.position;
        Vector3 rayDistance = transform.right * 0.1f;

    }
}
