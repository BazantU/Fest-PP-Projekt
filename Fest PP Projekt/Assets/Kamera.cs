using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{   
    public Transform gracz;
    public Camera kamera_gracza;
    public float sila;

    float rotacja_pionowa = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {   
        float mysz_Y = Input.GetAxis("Mouse Y") * sila * Time.deltaTime;
        float mysz_X = Input.GetAxis("Mouse X") * sila * Time.deltaTime;

        rotacja_pionowa -= mysz_Y;
        rotacja_pionowa = Mathf.Clamp(rotacja_pionowa, -90f, 90f);

        gracz.Rotate(Vector3.up * mysz_X);
        kamera_gracza.transform.localRotation = Quaternion.Euler(rotacja_pionowa, 0f, 0f);
    }
}
