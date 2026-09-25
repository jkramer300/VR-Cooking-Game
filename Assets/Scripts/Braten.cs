using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

public class Braten : MonoBehaviour
{
    int stufe = 0;
    public List<GameObject> Inhalt;
    public float temperatur = 0f; //0-150
    public bool oil = false;
    public bool veg = false;
    public bool soy = false;
    public bool sour = false;
    public bool tomato = false;

    bool aufHerd = false;
    int counter = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        ChangeTemperatur();
        ChangeAudio();
    }

    public void ClearPan()
    {
        foreach(GameObject i in Inhalt)
            {
                i.transform.SetParent(null, true);
            }
    }
    IEnumerator AddToPan(GameObject other)
    {
       Debug.Log("test1");
        yield return new WaitForSeconds(5);
        Debug.Log("test2");
        if (Inhalt.Contains(other.gameObject))
        {
            other.transform.SetParent(transform, true);
            Destroy(other.gameObject.GetComponent<XRGrabInteractable>());
            Destroy(other.gameObject.GetComponent<Rigidbody>());
        }
    }
    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Gemüse") || other.gameObject.CompareTag("Schnitzel"))
        {
            Inhalt.Add(other.gameObject);

            StartCoroutine(AddToPan(other.gameObject));

        }
        else if (other.gameObject.CompareTag("Pfanne"))
        {
            aufHerd = true;
            stufe = other.gameObject.GetComponent<HerdPlatte>().stufe;
        }
        else if (other.gameObject.CompareTag("Oil"))
        {
            oil = true;
        }
        else if (other.gameObject.CompareTag("GemüseBrühe"))
        {
            veg = true;
        }
        else if (other.gameObject.CompareTag("Egg"))
        {
            Inhalt.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Tomatenmark"))
        {
            tomato = true;
        }
        else if (other.gameObject.CompareTag("Soy"))
        {
            soy = true;
        }
        else if (other.gameObject.CompareTag("Sour"))
        {
            sour = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gemüse"))
        {
            if (Inhalt.Contains(other.gameObject))
            {
                Inhalt.Remove(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Pfanne"))
        {
            aufHerd = false;
            stufe = 0;
        }
        else if (other.gameObject.CompareTag("Egg"))
        {
            if (other.gameObject.CompareTag("Gemüse"))
            {            
                Inhalt.Remove(other.gameObject);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pfanne"))
        {
            stufe = other.gameObject.GetComponent<HerdPlatte>().stufe;
            for(int i = 0; i < Inhalt.Count; i++)
            {
                if(temperatur >= 150f*0.9f)
                    if(Inhalt[i].GetComponent<BratElement>() != null)
                        Inhalt[i].GetComponent<BratElement>().bratzustand ++;
            }
        }
    }
    void ChangeTemperatur()
    {
        float maxTemp = GetMaxTemp();

        if (aufHerd && temperatur < maxTemp)
        { 
            temperatur += 0.5f;
        }    
        else if(temperatur > 0)
        {
                temperatur -= 0.5f;
        }
    }
    void ChangeAudio()
    {
        //float maxTemp = GetMaxTemp();

        if(temperatur <= 150*0.9f) // soll sound nicht spielen
        {
            if (GetComponent<AudioSource>().isPlaying) // spielt sound
            {
                GetComponent<AudioSource>().Stop();
            }
            
        }
        else // soll sound spielen
        {
            if (!GetComponent<AudioSource>().isPlaying)// spielt keinen sound
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
    float GetMaxTemp()
    {
        float maxTemp = 0f;
        if(stufe == 1)
        {
            maxTemp = 100f;
        }
        else if(stufe == 2)
        {
            maxTemp = 150f;
        }
        else if(stufe == 3)
        {
            maxTemp = 200f;
        }
        return maxTemp;
    }
    void ChangeStufe()
    {
        if(stufe > 0)
        {
            
        }
    }
}
