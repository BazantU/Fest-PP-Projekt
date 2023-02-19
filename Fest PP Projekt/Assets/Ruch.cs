using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruch : MonoBehaviour
{
    public CharacterController controller;
    public float predkosc_ruchu;
    public float sila_grawitacji;
    public float sila_skoku;

    private Vector3 predkosc_spadania;

    void Update()
    {
        float ruchX = Input.GetAxis("Horizontal");
        float ruchZ = Input.GetAxis("Vertical");

        if(controller.isGrounded && predkosc_spadania.y < 0)
        {
            predkosc_spadania.y = -2f;
        }

        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            predkosc_spadania.y = sila_skoku;
        }
        
        Vector3 ruch = transform.right * ruchX + transform.forward * ruchZ;
        controller.Move(ruch * predkosc_ruchu * Time.deltaTime);

        predkosc_spadania.y += sila_grawitacji * Time.deltaTime;
        controller.Move(predkosc_spadania * Time.deltaTime);
    }
}
