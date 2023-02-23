using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonitorInterakcja : MonoBehaviour
{
    public Camera kamera_gracza;
    public GameObject gracz;
    public float odleglosc;

    public bool interakcja;
    private float delay;

    public GameObject kamera_glowna;
    public GameObject kamera_monitor;

    public TMP_Text tekst;
    private string kod = "";

    public string kod_dostepu;
    public bool mozna_pisac = true;

    void Start()
    {   
        kod_dostepu = kod_dostepu.ToUpper();
        tekst.text = kod;
        kamera_monitor.SetActive(false);
    }

    void Update()
    {   
        delay += Time.deltaTime;
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out hit)
            && hit.transform.tag == "Monitor"
            && hit.transform.GetComponent<Outline>() != null
            && hit.distance <= odleglosc
            && interakcja == false
            && mozna_pisac == true)
        {   
            interakcja = true;
            var item = hit.transform;
            item.GetComponent<Outline>().enabled = false;
            kamera_glowna.SetActive(false); kamera_monitor.SetActive(true);
            delay = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape)
            && interakcja == true)
        {   
            kamera_glowna.SetActive(true); kamera_monitor.SetActive(false);
            interakcja = false;
        }
    
        if(interakcja == true && delay > 0.25)
        {   
            foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {   
                if(Input.GetKeyDown(vKey))
                {   
                    //print(vKey);
                    string n = vKey.ToString();
                    if(n == "Q"
                        || n == "W"
                        || n == "E"
                        || n == "R"
                        || n == "T"
                        || n == "Y"
                        || n == "U"
                        || n == "I"
                        || n == "O"
                        || n == "P"
                        || n == "A"
                        || n == "S"
                        || n == "D"
                        || n == "F"
                        || n == "G"
                        || n == "H"
                        || n == "J"
                        || n == "K"
                        || n == "L"
                        || n == "Z"
                        || n == "X"
                        || n == "C"
                        || n == "V"
                        || n == "B"
                        || n == "N"
                        || n == "M")
                    {
                        kod += n;
                        tekst.text = kod;       
                    }

                    if(n == "Alpha1"
                        || n == "Alpha2"
                        || n == "Alpha3"
                        || n == "Alpha4"
                        || n == "Alpha5"
                        || n == "Alpha6"
                        || n == "Alpha7"
                        || n == "Alpha8"
                        || n == "Alpha9"
                        || n == "Alpha0")
                    {
                        n = n.Remove(0, 5);
                        kod += n;
                        tekst.text = kod;
                    }

                    if(n == "Backspace" && kod != "")
                    {
                        kod = kod.Remove(kod.Length - 1);
                        tekst.text = kod;
                    }

                    if(n == "Return")
                    {
                        if(kod == kod_dostepu)
                        {
                            mozna_pisac = false;
                            kamera_glowna.SetActive(true); kamera_monitor.SetActive(false);
                            interakcja = false;
                        }
                        else
                        {
                            kod = "";
                            tekst.text = kod;
                        }
                    }
                }
            }
        }
    }
}
