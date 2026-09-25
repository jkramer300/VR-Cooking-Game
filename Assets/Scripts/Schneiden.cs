using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Schneiden : MonoBehaviour
{
    public List<GameObject> Teile = new List<GameObject>();
    public List<GameObject> Schnittstellen = new List<GameObject>();
    private GameScript gameScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
        for (int i = 0; i < Schnittstellen.Count; i++)
        {
            Schnittstellen[i].GetComponent<Schnittstellen>().num = i + 1;
            Schnittstellen[i].GetComponent<Schnittstellen>().Cuttable = gameObject;

        }

        if (Teile.Count == 8)
        {
            //for (int i = 0; i < 7; i++)
            //    Cutting(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHolding();

        if (Teile.Count == 1)
        {
            CreateNew();
        }
    }

    void CheckHolding()
    {   
        //GameObject gameScript = GameObject.Find("GameScript");

        if (gameScript.KnifeSelected)
        {
            foreach(GameObject schnittstelle in Schnittstellen)
            {   
                if(schnittstelle != null)
                {
                    if (schnittstelle.activeSelf == false)
                    {
                        schnittstelle.SetActive(true);
                    }
                }
            }
        }
        else
        {
            foreach(GameObject schnittstelle in Schnittstellen)
            {
                if(schnittstelle != null)
                {
                    if (schnittstelle.activeSelf == true)
                    {
                        schnittstelle.SetActive(false);
                    }
                }
            }
        }
    }

    void CreateNew()
    {

        GameObject newTeil = Teile[0];
        newTeil.AddComponent<Schneiden2>();
        Debug.Log(newTeil.transform.name);
        Destroy(gameObject.GetComponent<XRGrabInteractable>());
        Destroy(gameObject.GetComponent<Rigidbody>());

        foreach (Transform child in newTeil.transform)
        {
            if (child.gameObject.CompareTag("Schnittstelle"))
            {
                newTeil.GetComponent<Schneiden2>().Schnittstellen.Add(child.gameObject);
                child.gameObject.SetActive(true);
            }
            else
            {
                newTeil.GetComponent<Schneiden2>().Teile.Add(child.gameObject);
            }
        }
        newTeil.transform.SetParent(null);
        //newTeil.AddComponent<BoxCollider>();
        newTeil.AddComponent<Rigidbody>();
        newTeil.AddComponent<XRGrabInteractable>();
        newTeil.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        newTeil.GetComponent<XRGrabInteractable>().useDynamicAttach = true;
        Destroy(gameObject);
    }
    public void Cutting(GameObject obj)
    {
        int num = Schnittstellen.IndexOf(obj) + 1;
        if (num > Teile.Count)
            return;
        GameObject Teil = new GameObject("Teil");
        Teil.transform.position = transform.position;

        Teil.AddComponent<Schneiden>();

        for (int i = 0; i < num; i++)
        {
            Teile[i].transform.SetParent(Teil.transform);
            Teil.GetComponent<Schneiden>().Teile.Add(Teile[i]);
        }
        for (int i = 0; i < num; i++)
        {
            Schnittstellen[i].transform.SetParent(Teil.transform);
            Teil.GetComponent<Schneiden>().Schnittstellen.Add(Schnittstellen[i]);
            Schnittstellen[i].GetComponent<Schnittstellen>().Cuttable = Teil;
        }
        GameObject s = Schnittstellen[num - 1];
        for (int i = 0; i < num; i++)
        {
            Teile.Remove(Teile[0]);
            Schnittstellen.Remove(Schnittstellen[0]);
        }
        Destroy(s);

        CreateNewParent();

    
        Teil.AddComponent<Rigidbody>();
        Teil.AddComponent<XRGrabInteractable>();
        Teil.GetComponent<XRGrabInteractable>().useDynamicAttach = true;
        Teil.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Destroy(gameObject);
    }
    void CreateNewParent()
    {
        GameObject Teil = new GameObject("Teil");
        Teil.transform.position = transform.position;
        Teil.AddComponent<Schneiden>();
        Destroy(gameObject.GetComponent<XRGrabInteractable>());
        Destroy(gameObject.GetComponent<Rigidbody>());

        foreach(GameObject schnittstelle in Schnittstellen)
        { 
            if (schnittstelle != null)
            {
                schnittstelle.transform.SetParent(Teil.transform);
                Teil.GetComponent<Schneiden>().Schnittstellen.Add(schnittstelle);
            }
        }
        foreach(GameObject teil in Teile)
        {
            teil.transform.SetParent(Teil.transform);
            Teil.GetComponent<Schneiden>().Teile.Add(teil);
        }

        Teil.AddComponent<Rigidbody>();
        Teil.AddComponent<XRGrabInteractable>();
        Teil.GetComponent<XRGrabInteractable>().useDynamicAttach = true;
        Teil.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}
