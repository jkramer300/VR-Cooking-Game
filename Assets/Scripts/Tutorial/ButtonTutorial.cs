using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTutorial : MonoBehaviour
{   
    public Button testButton;
    public TextMeshProUGUI TextField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestButton()
    {
        TextField.text = "Button Clicked";
        testButton.image.color = Color.red;
    }
}
