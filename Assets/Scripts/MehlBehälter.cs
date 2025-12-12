using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MehlBehälter : MonoBehaviour
{
    // Start is called before the first frame update
    public Material flour;
    public GameObject flourCap;
    (float, float) capHeight = (0.00846f, -0.00945f);
    ClipShader clipShader = new ClipShader();
    float inhalt = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        clipShader.ChangeShader(inhalt, (0.87f, 1.13f), (0.9f, 1.17f), (0, 0.003f), capHeight, flourCap, flour, 0, 0, 0);
    }
}
