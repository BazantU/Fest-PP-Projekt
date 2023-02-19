using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemy : MonoBehaviour
{   
    public Camera kamera_gracza;
    public Transform reka_gracza;
    public float odleglosc;

    private List<GameObject> przedmioty_lista = new List<GameObject>();
    private bool trzyma_przedmiot = false;

    void Update()
    {
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit) 
            && hit.transform.tag == "Item" 
            && hit.transform.GetComponent<Outline>() != null
            && hit.distance <= odleglosc)
        {   
            var przedmiot = hit.transform;
            przedmiot.GetComponent<Outline>().enabled = true;

            if (!przedmioty_lista.Contains(przedmiot.gameObject))
            {
                przedmioty_lista.Add(przedmiot.gameObject);
            }
        }

        for (int i = 0; i < przedmioty_lista.Count; i++)
        {
            var obiekt = przedmioty_lista[i];
            if(obiekt.transform.GetComponent<Outline>() != null && Physics.Raycast(ray, out hit) && obiekt != hit.transform.gameObject)
            {
                obiekt.transform.GetComponent<Outline>().enabled = false;           
                przedmioty_lista.Remove(obiekt);
            }
        }

        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out hit)
            && trzyma_przedmiot == false
            && hit.transform.tag == "Item"
            && hit.transform.GetComponent<Outline>() != null
            && hit.transform.GetComponent<Rigidbody>() != null
            && hit.distance <= odleglosc)
        {   
            trzyma_przedmiot = true;
            var item = hit.transform;
            item.GetComponent<Outline>().enabled = false;

            item.GetComponent<Rigidbody>().isKinematic = true;
            item.SetParent(reka_gracza, true);

            item.localPosition = new Vector3(0f, 0f, 0f);
            item.localRotation = Quaternion.Euler(Vector3.zero);
        }

        if(Input.GetKeyDown(KeyCode.Q) && trzyma_przedmiot == true)
        {
            for (int i = 0; i < reka_gracza.childCount; i++)
            {   
                var wyrzuc_przedmiot = reka_gracza.GetChild(i);
                if(wyrzuc_przedmiot.tag == "Item" 
                    && wyrzuc_przedmiot.GetComponent<Rigidbody>() != null
                    && wyrzuc_przedmiot.GetComponent<Outline>() != null
                    )
                {   
                    wyrzuc_przedmiot.GetComponent<Outline>().enabled = false;      
                    wyrzuc_przedmiot.SetParent(null);
                    wyrzuc_przedmiot.GetComponent<Rigidbody>().isKinematic = false;

                    trzyma_przedmiot = false;
                }
            }
        }
    }
}
