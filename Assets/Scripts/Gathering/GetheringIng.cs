using UnityEngine;
using System.Collections.Generic;

public class GetheringIng : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<string> ingredients = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckEquipment();
        foreach(string ing in ingredients)
        {
            Debug.Log(ing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredients.Count < 1)
        {
            GameObject.Find("GameScript").GetComponent<GameScript>().step += 1;
            gameObject.SetActive(false);
        }
    }

    void CheckEquipment()
    {
        List<string> t = new List<string>(GameObject.Find("Recipe").GetComponent<CurrentRecipe>().currentRecipe.ingredients);
        ingredients = t;
    }

    void OnTriggerEnter(Collider other)
    {
        if (ingredients.Contains(other.gameObject.name))
        {
            ingredients.Remove(other.gameObject.name);
        }
    }
}
