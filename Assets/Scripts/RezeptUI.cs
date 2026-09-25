using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
//using TMPro;

public class RezeptUI : MonoBehaviour
{
    public GameObject ui;
    public CurrentRecipe currentRecipe;
    void Start()
    {
        AddRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateRecipe();
    }
    void AddRecipe()
    {
        Recipe recipe = currentRecipe.currentRecipe;
        for (int i = 0; i < recipe.numberOfInstructions; i++)
        {
            AddText((i + 1).ToString(), 20);
            AddText(recipe.instructions[i], 8);

        }

    }
    void UpdateRecipe()
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
                ui.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = System.Math.Round(recipe.quantity[(i - 1) / 2], 3).ToString();
            }
        }
    }
    void AddText(string text, int fontSize)
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
