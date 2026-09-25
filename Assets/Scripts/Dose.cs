using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Dose : MonoBehaviour
{   

    public int open = 0;
    public int inhalt = 150;
    public Transform spawn;
    public GameObject Deckel;
    public GameObject pea;
    public ParticleSystem p;
    int numberOfPeas = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        OpenDose();
    }
    void OpenDose()
    {
        // open to drain
        if(InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected && open == 0)
        {
            open = 1;
            Deckel.transform.Rotate(45f, 0f, 0f);
            p.Play();
        }
        // Drain
        else if (open == 1 && inhalt > 0)
        {   
            inhalt -= 1;
        }
        else if(open == 1 && inhalt == 0 && p.isPlaying)
        {   
            p.Stop();
            open = 2;
        }
        //openComplete
        else if(InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected && open >= 2 && inhalt == 0 && numberOfPeas > 0)
        {   
            Spawn();
            Deckel.SetActive(false);
            open = 3;
        }
    }
    void Spawn()
    {   
        Instantiate(pea, spawn.position, spawn.rotation);
        numberOfPeas -= 1;
    }
}
