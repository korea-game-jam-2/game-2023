///kyoungSoo

using UnityEngine;

public class Thorn : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Thorn")
        {
            Debug.Log("Contect Thorn");
        }
    }
}
