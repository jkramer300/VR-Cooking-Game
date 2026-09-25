using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;
using System.Threading;
using UnityEditor;

public class Egg : MonoBehaviour
{
    public GameObject eggBottom;
    public GameObject eggParent;
    public GameObject eggWhite;
    public GameObject eggCollider;
    bool cracked = false;
    bool test = true;
    InputAction pressedButton;
    // Start is called before the first frame update
    void Start()
    {
        /*Collider parentCollider = GetComponent<Collider>();
        if (parentCollider != null && eggBottom.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(eggWhite.GetComponent<Collider>(), eggBottom.GetComponent<Collider>());
            Physics.IgnoreCollision(eggWhite.GetComponent<Collider>(), parentCollider);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
            OpenEgg();
    }


    void CrackEgg()
    {
        eggBottom.transform.localPosition += new Vector3(0f, 0f, -6e-05f);
        cracked = true;
        //Physics.IgnoreCollision(GetComponent<Collider>(), eggBottom.GetComponent<Collider>());
        //eggBottom.AddComponent<Rigidbody>();
        //eggBottom.AddComponent<XRGrabInteractable>();
        //eggBottom.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && !cracked)
        {
            float speed = rb.linearVelocity.magnitude;
            //Debug.Log("Geschwindigkeit beim Aufprall: " + speed + " m/s");

            if (speed > 0.5)
            {
                CrackEgg();
            }
        }
    }

    public void OpenEgg()
    {
        if (cracked && test)
        {
            Destroy(eggCollider);
            Destroy(GetComponent<BoxCollider>());
            //Destroy(eggBottom.GetComponent<Collider>());
            //Destroy(eggWhite.GetComponent<Collider>());

            eggBottom.transform.SetParent(eggParent.transform, true);
            eggWhite.transform.SetParent(eggParent.transform, true);


            eggBottom.AddComponent<MeshCollider>().convex = true;
            eggWhite.AddComponent<BoxCollider>();


            eggWhite.AddComponent<Rigidbody>();
            eggWhite.AddComponent<XRGrabInteractable>();


            eggBottom.AddComponent<Rigidbody>();
            eggBottom.AddComponent<XRGrabInteractable>();
            test = false;
            eggWhite.layer = 9;
            eggWhite.transform.SetParent(null);

            GetComponent<Problems>().AddProblem(gameObject);
        }
    }
}
