using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Schneiden2 : MonoBehaviour
{
    public List<GameObject> Teile = new List<GameObject>();
    public List<GameObject> Schnittstellen = new List<GameObject>();
    private GameScript gameScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {   
        /**if (gameObject.GetComponent<FoodWaste>() != null)
        {   
            Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.GetComponent<BoxCollider>();
            gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<XRGrabInteractable>();
            gameObject.GetComponent<Problems>().AddProblem(gameObject);
            Destroy(gameObject.GetComponent<Schneiden2>());
        }**/
        if (Schnittstellen.Count < 1 && transform.childCount > 0)
        {
            Dismantle();
        }
        CheckHolding();

    }

    void CheckHolding()
    {
        if (gameScript.KnifeSelected)
        {
            foreach(GameObject schnittstelle in Schnittstellen)
            {
                if (schnittstelle.activeSelf == false)
                {
                    schnittstelle.SetActive(true);
                }
            }
        }
        else
        {
            foreach(GameObject schnittstelle in Schnittstellen)
            {
                if (schnittstelle.activeSelf == true)
                {
                    schnittstelle.SetActive(false);
                }
            }
        }
    }
    public void Cutting(GameObject schnittstelle)
    {
        Schnittstellen.Remove(schnittstelle);
        Destroy(schnittstelle);

    }

    void Dismantle()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<FoodWaste>() != null)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                Destroy(child.GetComponent<BoxCollider>());
                child.AddComponent<BoxCollider>();
                child.transform.SetParent(null);
                child.AddComponent<Rigidbody>();
                child.AddComponent<XRGrabInteractable>().useDynamicAttach = true;
                child.GetComponent<Problems>().AddProblem(gameObject);
                Teile.Remove(child);
            }
        }

        while (Teile.Count > 0)
        {
            var xr = GetComponent<XRGrabInteractable>();
            if (xr != null)
            {
                Destroy(xr);
            }
            var rig = GetComponent<Rigidbody>();
            if (rig != null)
            {
                Destroy(rig);
            }

            Teile[0].transform.SetParent(null);
            var meshCol = Teile[0].GetComponent<MeshCollider>();
            if (meshCol != null)
            {
                Destroy(meshCol);
            }
            if(gameObject.tag == "Schnitzel")
            {
                Teile[0].tag = "Schnitzel";
            }
            else
            {
                Teile[0].tag = "Gemüse";
            }
            if(Teile[0].AddComponent<BoxCollider>() != null)
            {
                Destroy(Teile[0].AddComponent<BoxCollider>());
            }

            Teile[0].AddComponent<BoxCollider>();
            Teile[0].AddComponent<Rigidbody>();
            Teile[0].AddComponent<XRGrabInteractable>();
            Teile[0].GetComponent<XRGrabInteractable>().useDynamicAttach = true;
            Teile[0].GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            Teile[0].GetComponent<Rigidbody>().mass = 0.01f;
            Teile.Remove(Teile[0]);
        }
        Destroy(gameObject);
    }
}
