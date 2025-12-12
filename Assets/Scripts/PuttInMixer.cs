using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PuttInMixxer : MonoBehaviour
{
    GameObject ingredient = null;
    public CurrentRecipe currentRecipe;
    public Material doug;
    public GameObject dougCap;
    (float, float) capHeight = (0.0075f, -0.0096f);
    (float, float) capSize = (0.011f, 0.0135f);
    float recipeWeight = 0f;
    float currentWeight = 0f;
    Recipe rec;
    ClipShader clipShader = new ClipShader();
    bool addFlour = false;
    GameObject flour;
    public float factor = 1;
    public bool mixxed = false;
    public GameObject inhalt;
    public GameObject drehknopf;
    void Start()
    {
        rec = currentRecipe.GetComponent<CurrentRecipe>().currentRecipe;
        foreach (float q in rec.quantity)
        {
            recipeWeight += q;
        }
        doug.SetFloat("_RevealAmount", doug.GetFloat("_Min"));
        //clipShader.ChangeShader(1, capSize, (0, 0), capSize, capHeight, dougCap, doug, -1, 0, 0.0001f, 0);
    }
    void Update()
    {
        if (addFlour)
        {
            AddFlour();
        }
        Mix();
    }
    void Mix()
    {
        foreach (float i in rec.quantity)
        {
            if (i > 0)
            {
                return;
            }
        }
        if (drehknopf.transform.eulerAngles.y > 180f)
        {
            Debug.Log("MixTest");
            //Debug.Log($"{drehknopf.transform.eulerAngles.x},{drehknopf.transform.eulerAngles.y}, {drehknopf.transform.eulerAngles.z}");
            inhalt.transform.eulerAngles += new Vector3(0f, 0f, 10f);
            mixxed = true;
        }
    }
    public bool GetMixxed()
    {
        return mixxed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            ingredient = other.gameObject;
            if (ingredient.name.Contains("Karrote") && rec.quantity[5] > 0)
            {
                ChangeRecipe("Karrote", ingredient);
            }
            else if (ingredient.name.Contains("EiWeis") && rec.quantity[6] > 0)
            {
                ChangeRecipe("Ei", ingredient);
            }
            currentWeight += ingredient.GetComponent<Ingredient>().quantity;
            clipShader.ChangeShader(currentWeight / recipeWeight, capSize, (0, 0), capSize, capHeight, dougCap, doug, -1, 0, 0.0001f, 0);
            Destroy(ingredient);
        }
        else if (other.gameObject.layer == 9)
        {
            addFlour = true;
            flour = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            addFlour = false;
        }
    }
    void ChangeRecipe(string ingType, GameObject ing)
    {
        rec = currentRecipe.GetComponent<CurrentRecipe>().currentRecipe;
        for (int i = 0; i < rec.numberOfIngredients; i++)
        {
            if (rec.ingredients[i] == ingType)
            {
                rec.quantity[i] -= ing.GetComponent<Ingredient>().quantity; // change Quantity of Recipe
            }
        }
    }

    void AddFlour()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 5)
                return;
            if (flour.GetComponent<Bowl>().types[i] > 0f && currentRecipe.currentRecipe.quantity[i] > 0f)
            {
                flour.GetComponent<Bowl>().types[i] -= 0.001f;
                currentRecipe.currentRecipe.quantity[i] -= 0.001f;
                break;
            }
        }

        flour.GetComponent<Bowl>().AddFlour(-1);
        currentWeight += 0.001f;
        //currentRecipe.currentRecipe.quantity[1] -= 0.001f;
        clipShader.ChangeShader(currentWeight / recipeWeight, capSize, (0, 0), capSize, capHeight, dougCap, doug, -1, 0, 0.0001f, 0);



    }
    public void RemoveDoug()
    {
        if (factor > 0)
        {
            factor -= 0.001f;
            clipShader.ChangeShader(factor, capSize, (0, 0), capSize, capHeight, dougCap, doug, -1, 0, 0.0001f, 0);
        }
    }
}
