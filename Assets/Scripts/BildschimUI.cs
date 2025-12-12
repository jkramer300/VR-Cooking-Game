using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
//using TMPro;

public class BildschimUI : MonoBehaviour
{
    public GameObject choseRecipe;
    public GameObject uiRight;
    public GameObject uiLeft;
    public CurrentRecipe currentRecipe;
    void Start()
    {
        AddRecipeRight();
        AddRecipeLeft();
        choseRecipe.SetActive(true);
        uiRight.SetActive(false);
        uiLeft.SetActive(false);
    }

    public void ChoseRecipe()
    {
        currentRecipe.currentRecipe = currentRecipe.recipes[0];
        choseRecipe.SetActive(false);
        uiLeft.SetActive(true);
        uiRight.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRecipe(uiRight);
    }
    void AddRecipeRight()
    {
        Recipe recipe = currentRecipe.currentRecipe;
        for (int i = 0; i < recipe.numberOfIngredients; i++)
        {
            AddText(recipe.ingredients[i], uiRight, 15);
            AddText(System.Math.Round(recipe.quantity[i], 3).ToString() + " g", uiRight, 15);

        }
    }

    void AddRecipeLeft()
    {
        Recipe recipe = currentRecipe.currentRecipe;
        for (int i = 0; i < recipe.numberOfInstructions; i++)
        {
            AddText((i + 1).ToString(), uiLeft, 20);
            AddText(recipe.instructions[i], uiLeft, 8);

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
}
