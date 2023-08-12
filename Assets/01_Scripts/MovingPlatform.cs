
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    Animator anim;
    public bool isMirror = false;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isMirror", isMirror);
    }
}
