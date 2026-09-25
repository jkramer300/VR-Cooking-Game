using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Pfanne2 : MonoBehaviour
{
    public GameObject pfanne;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
        {
            pfanne.GetComponent<Braten>().ClearPan();
        }
    }
}
