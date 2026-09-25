using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using TMPro;

public class BildschirmUI : MonoBehaviour
{
    public GameObject choseRecipe;
    public GameObject choseRecipe2;
    public GameObject uiRight;
    public GameObject uiLeft;
     public GameObject uiRight2;
    public GameObject uiLeft2;
    public CurrentRecipe currentRecipe;
    public GameObject GameScript;

    void Start()
    {
        choseRecipe.SetActive(true);
        uiRight.SetActive(false);
        uiLeft.SetActive(false);
    }

    public void ChoseRecipe2(string recipe)
    {
        for(int i = 0; i < currentRecipe.recipes.Count; i++)
        {
            if(currentRecipe.recipes[i].gameObject.name == recipe)
            {
                currentRecipe.secondRecipe = currentRecipe.recipes[i]; 
            }
        }
        AddRecipeRight(0);
        AddRecipeLeft();
        AddRecipeRight2(0);
        AddRecipeLeft2();
        choseRecipe2.SetActive(false);
        uiLeft.SetActive(true);
        uiRight.SetActive(true);
        GameScript.GetComponent<GameScript>().step +=1;
        AddSteps(0);

        for(int i = 0; i < currentRecipe.currentRecipe.ingredients.Count; i++)
        {
            GameObject.Find("Spawn").GetComponent<Spawning>().Spawn2(currentRecipe.currentRecipe.ingredients[i]);
        }
        //for(int i = 0; i < currentRecipe.secondRecipe.ingredients.Count; i++)
        //{
        //    GameObject.Find("Spawn").GetComponent<Spawning>().Spawn2(currentRecipe.secondRecipe.ingredients[i]);
        //}
    }
        
    public void ChoseRecipe(string recipe)
    {
        for(int i = 0; i < currentRecipe.recipes.Count; i++)
        {
            if(currentRecipe.recipes[i].gameObject.name == recipe)
            {
                currentRecipe.currentRecipe = currentRecipe.recipes[i]; 
            }
        }
        
        choseRecipe.SetActive(false);
        choseRecipe2.SetActive(true);
    }
    public void ResetRecipe()
    {
        choseRecipe.SetActive(true);
        uiLeft.SetActive(false);
        uiRight.SetActive(false);
    }
    void AddRecipeRight(int step)
    {
        Recipe recipe = currentRecipe.currentRecipe;
        uiRight.GetComponent<TextMeshProUGUI>().text = recipe.instructions[step];
    }

    void AddRecipeRight2(int step)
    {
        Recipe recipe = currentRecipe.secondRecipe;
        uiRight2.GetComponent<TextMeshProUGUI>().text = recipe.instructions[step];
    }

    void AddRecipeLeft2()
    {
        Recipe recipe = currentRecipe.secondRecipe;
        for (int i = 0; i < recipe.steps.Count; i++)
        {
            AddText(recipe.steps[i], uiLeft2, 5);
        }
    }

    void AddRecipeLeft()
    {
        Recipe recipe = currentRecipe.currentRecipe;
        for (int i = 0; i < recipe.steps.Count; i++)
        {
            AddText(recipe.steps[i], uiLeft, 5);
        }
    }

    void UpdateRecipe(GameObject ui)
    {
        Recipe recipe = currentRecipe.currentRecipe;
        for (int i = 0; i < ui.transform.childCount; i++)
        {
            if (i % 2 == 0)
            {
                ui.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = recipe.ingredients[i / 2];
            }
            else
            {
                ui.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = System.Math.Round(recipe.quantity[(i - 1) / 2], 3).ToString() + " g";
            }
        }
    }
    void AddText(string text, GameObject ui, int fontSize)
    {
        GameObject textObject = new GameObject();
        textObject.transform.SetParent(ui.transform, false);
        TextMeshProUGUI tmpText = textObject.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = fontSize;
        tmpText.alignment = TextAlignmentOptions.Left;
        tmpText.color = Color.black;
        RectTransform rect = textObject.GetComponent<RectTransform>();
    }

    public void AddSteps(int step)
    {

        Debug.Log(step);
        foreach (Transform UI in uiLeft.transform)
        {
            UI.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        for(int i = 0; i < step; i++)
        {
            uiLeft.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        uiLeft.transform.GetChild(step).GetComponent<TextMeshProUGUI>().color = Color.blue;
        
        AddRecipeRight(step);
        
    }
    public void AddSteps2(int step)
    {

        Debug.Log(step);
        foreach (Transform UI in uiLeft2.transform)
        {
            UI.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        for(int i = 0; i < step; i++)
        {
            uiLeft2.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        uiLeft2.transform.GetChild(step).GetComponent<TextMeshProUGUI>().color = Color.blue;
        
        AddRecipeRight2(step);
    }
}
