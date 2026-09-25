using System.Collections;
using System.Collections.Generic;
using Hanzzz.MeshSlicerFree;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.SpatialKeyboard;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class Spawning : MonoBehaviour
{
    public List<GameObject> ingredients;
    public GameObject SpawnPoint;
    public GameObject ingredientsPanel;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnItem(GameObject button)
    {
        string name = button.name;
        if (name == "Carrot")
        {
            Spawn(0);
        }
        else if (name == "Flour")
        {
            Spawn(1);
        }
        else if (name == "Sugar")
        {
            Spawn(2);
        }
        else if (button.name == "Oil")
        {
            Spawn(3);
        }
        else if (button.name == "Egg")
        {
            Spawn(4);
        }
        else if (button.name == "BakingPowder")
        {
            Spawn(5);
        }
        else if (button.name == "Almonds")
        {
            Spawn(6);
        }
    }
    void Spawn(int number)
    {
        Instantiate(ingredients[number], SpawnPoint.transform.position, SpawnPoint.transform.rotation);
    }
    public void Spawn2(string name)
    {
        for(int i = 0; i < ingredients.Count; i++)
        {
            if(ingredients[i].name == name)
            {
                Instantiate(ingredients[i], SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            }
        }
    }
    
    public void Search(TMP_InputField input)
    {
        foreach (Transform child in ingredientsPanel.transform)
        {
            if (child.gameObject.name.StartsWith(input.text))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
