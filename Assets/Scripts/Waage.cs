using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waage : MonoBehaviour
{
    public TMP_Text text;
    Button tareButton;
    float weight;
    float start = 0f;
    public Bowl bowl = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WeighObject();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Schüssel"))
        {
            bowl = other.gameObject.GetComponent<Bowl>();
            Debug.Log("Enter");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Schüssel"))
        {
            weight = 0;
            text.text = $"{weight * 1000f}";
            bowl = null;
            Debug.Log("Exit");
        }
    }

    public void Tare()
    {
        Debug.Log("Tare");
        //text.text = $"0";
        if (bowl != null)
        {
            start = (bowl.contentWeight + bowl.weight) * -1;
        }
        else
        {
            start = 0;
        }
        Debug.Log($"Tare: {start}");
        WeighObject();
    }

    void WeighObject()
    {
        if (bowl != null)
        {
            weight = start + bowl.contentWeight + bowl.weight;
            text.text = $"{(int)(weight * 1000f)}";
        }
        else
        {
            weight = start + 0;
        }

    }
}
