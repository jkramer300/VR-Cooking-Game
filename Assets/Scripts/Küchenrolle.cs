using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Küchenrolle : MonoBehaviour
{
    public GameObject Paper;
    public Transform SpawnPoint;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPaper();
    }
    void SpawnPaper()
    {
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
        {
            Instantiate(Paper, SpawnPoint.position, SpawnPoint.rotation);
        }
    }
}
