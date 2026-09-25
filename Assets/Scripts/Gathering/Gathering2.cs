using UnityEngine;
using System.Collections.Generic;

public class Gathering2 : MonoBehaviour
{
    public List<GameObject> gatheringPoints = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPoints();
    }

    void CheckPoints()
    {
        foreach(GameObject point in gatheringPoints)
        {   
            switch (point.GetComponent<GatheringPoint>().set)
            {
                case -1:
                    return;
                case 0:
                    return;

                case 1:
                    break;

                case 2:
                    break;
            }
        }
        GameObject.Find("GameScript").GetComponent<GameScript>().step += 1;
        gameObject.SetActive(false);
    }
}
