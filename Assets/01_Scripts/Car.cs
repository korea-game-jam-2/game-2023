using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Animator anim;
    [SerializeField] Sprite[] cars;
    [SerializeField] SpriteRenderer thisCar;
    float speed;
    [SerializeField] bool isMirror;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isMirror", isMirror);
    }

    public void initCar()
    {
        speed = Random.Range(0.4f, 1.4f);
        anim.SetFloat("Speed", speed);
        
        thisCar.sprite = cars[Random.Range(0, cars.Length)];
    }
}
