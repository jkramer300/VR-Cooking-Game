using System.Collections.Generic;
using UnityEngine;


public class GatheringEquipment : MonoBehaviour
{
    List<string> tools = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckEquipment();
        foreach(string ing in tools)
        {
            Debug.Log(ing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tools.Count < 1 && GameObject.Find("GameScript").GetComponent<GameScript>().step == 3)
        {
            GameObject.Find("GameScript").GetComponent<GameScript>().step += 1;
        }
    }

    void CheckEquipment()
    {
        List<string> t = new List<string>(GameObject.Find("Recipe").GetComponent<CurrentRecipe>().currentRecipe.tools);
        tools = t;
    }

    void OnTriggerEnter(Collider other)
    {   
        foreach(string ing in tools)
        {
            Debug.Log(ing);
        }
        Debug.Log(other.gameObject.name);
        if (tools.Contains(other.gameObject.name))
        {
            tools.Remove(other.gameObject.name);
        }
    }
}
