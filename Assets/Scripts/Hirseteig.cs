using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;
public class Hirseteig : MonoBehaviour
{
    public GameObject bällchen;
    public int number = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected && number > 0)
        {
            number--;
            Instantiate(bällchen, transform.position, transform.rotation);
        }  
        if(number == 0)
        {
            Destroy(gameObject);
        }
    }

}
