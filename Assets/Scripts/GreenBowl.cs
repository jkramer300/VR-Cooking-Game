using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;
public class GreenBowl : MonoBehaviour
{
    public List<GameObject> content;
    public GameObject inhalt;
    //public Material water;
    public GameObject waterInhalt;
    public bool water = false;
    private GameScript gs;

    public int step = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gs = GameObject.Find("GameScript").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case 0:
                FirstStep();
                break;

            case 1:
                AddVegetablesToBowl();
                break;

            case 2:
                WashVegetables();
                break;
        }
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
        {
            ChangeWater(false);
        }
    }
    void FirstStep()
    {
        if(gs.currentStep.id == 4)           
        {
            step++;
        }
    }
    void AddVegetablesToBowl()
    {
        WashVegetables[] wash = FindObjectsByType<WashVegetables>();
        foreach(WashVegetables veg in wash)
        {
            if (!content.Contains(veg.gameObject))
            {
                return;
            }
        }
        step++;
    }
    void WashVegetables()
    {
        if (water)
        {
            foreach(GameObject veg in content)
            {
                if(veg.GetComponent<WashVegetables>() != null)
                {
                    veg.GetComponent<WashVegetables>().washed = true;
                }
            }
            step++;
        }
    }
    void PutBowlInPlace()
    {
        
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
        else if (other.gameObject.GetComponent<WashVegetables>() != null)
        {
            //content.Add(other.gameObject.GetComponent<WashVegetables>());
            //content.Add(other.gameObject);
            if(content.Find(x => x == other.gameObject) == null)
            {
                content.Add(other.gameObject);
                //RemovePhysics(other.gameObject);
            }
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
    public void Selected()
    {   
        Debug.Log("Selected:");
        foreach (GameObject ingredient in content)
        {
            //ingredient.GetComponent<Rigidbody>().isKinematic = true;
            //_parent = ingredient.transform.parent.gameObject;
            RemovePhysics(ingredient.gameObject);
        }
    }
}
