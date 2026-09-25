using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public int numberOfIngredients;
    public List<String> ingredients;
    public List<float> quantity;
    public int numberOfTools;
    public List<String> tools;
    public List<float> time;
    public int ovenMode;
    public int ovenDegree;
    public int ovenTime;
    public List<String> steps;
    public List<String> instructions;
    public int numberOfInstructions;

    void Update()
    {
        for (int i = 0; i < numberOfIngredients; i++)
        {
            if (quantity[i] < 0f || quantity[i] < 0.001f)
            {
                quantity[i] = 0f;
            }
        }
    }
}
