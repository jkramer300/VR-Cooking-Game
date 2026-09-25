using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Bowl2 : MonoBehaviour
{   
    public List<GameObject> content;
    public GameObject inhalt;
    //public Material water;
    public GameObject waterInhalt;
    public bool water = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //water.SetFloat("_RevealAmount", 0.01f);
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
        {
            if (water)
            {
                ChangeWater(false);
            }
            else
            {
                UnSelected();
            }
        }
    }
    
    public void ChangeWater(bool add)
    {
        water = add;
        waterInhalt.SetActive(add);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            ChangeWater(true);
        }
        else if (other.gameObject.GetComponent<Ingredient>() != null)
        {
            //content.Add(other.gameObject);
            if(content.Find(x => x == other.gameObject) == null)
            {
                content.Add(other.gameObject);
                //RemovePhysics(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        content.Remove(other.gameObject);
    }
    public void Selected()
    {
        Debug.Log("Selected:");
        foreach (GameObject ingredient in content)
        {
            //ingredient.GetComponent<Rigidbody>().isKinematic = true;
            //_parent = ingredient.transform.parent.gameObject;
            RemovePhysics(ingredient);
        }
    }

    void RemovePhysics(GameObject ingredient)
    {
        ingredient.transform.SetParent(inhalt.transform, true);
        Destroy(ingredient.GetComponent<XRGrabInteractable>());
        Destroy(ingredient.GetComponent<Rigidbody>());
    }
    public void UnSelected()
    {
        foreach (GameObject ingredient in content)
        {
            ingredient.transform.SetParent(null, true);
            //ingredient.GetComponent<Rigidbody>().isKinematic = false;
            ingredient.AddComponent<Rigidbody>();
            ingredient.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            ingredient.AddComponent<XRGrabInteractable>();
        }
        content.Clear();
    }
}
