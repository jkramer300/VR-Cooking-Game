using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Kiste : MonoBehaviour
{   
    public GameObject Deckel;
    public GameObject pea;
    public Transform spawn;
    int numberOfPeas = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected && numberOfPeas > 0)
        {   
            Spawn();
            Deckel.SetActive(false);
        }    
    }
    void Spawn()
    {   
        Instantiate(pea, spawn.position, spawn.rotation);
        numberOfPeas --;
        
    }

}
