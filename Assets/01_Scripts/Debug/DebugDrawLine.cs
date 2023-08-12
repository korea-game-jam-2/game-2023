using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DebugDrawLine : MonoBehaviour
{
    public Vector2 angle;
    public float distance = 1f;

    void Update()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + angle.normalized * distance, Color.red);
    }
}
