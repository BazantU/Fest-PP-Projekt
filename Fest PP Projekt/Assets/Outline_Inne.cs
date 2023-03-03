using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Inne : MonoBehaviour
{
    public Camera kamera_gracza;
    private List<GameObject> przedmioty_lista = new List<GameObject>(); 
    public float odleglosc;

    public MonitorInterakcja monitorSC;

    void Update()
    {   
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit) 
            && hit.transform.GetComponent<Outline>() != null
            && hit.distance <= odleglosc)
        {   
            var przedmiot = hit.transform;
            
            //Monitor
            if(przedmiot.tag == "Monitor"
                && monitorSC.interakcja == false)
            {
                przedmiot.GetComponent<Outline>().enabled = true;
                if (!przedmioty_lista.Contains(przedmiot.gameObject))
                {
                    przedmioty_lista.Add(przedmiot.gameObject);
                }
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
    }
}
