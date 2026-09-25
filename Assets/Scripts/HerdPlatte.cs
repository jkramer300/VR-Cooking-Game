using UnityEngine;

public class HerdPlatte : MonoBehaviour
{
    public int stufe = 0;
    public GameObject drehschalter;
    public GameObject licht;
    public Material[] lichtFarbe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        stufe = CheckMode(drehschalter);
        ChangeLight();

    }
    void ChangeLight()
    {
        Renderer renderer = licht.GetComponent<Renderer>();
        if(stufe > 0 && renderer.material != lichtFarbe[1])
        {
            renderer.material = lichtFarbe[1];
        }
        else if(stufe == 0 && renderer.material != lichtFarbe[0])
        {
            renderer.material = lichtFarbe[0];
        }
    }
    int CheckMode(GameObject _gameObject)
    {
        float x = _gameObject.GetComponent<Drehschalter>().rotation;
        if (x == 270f)
        {
            return 0;
        }
        else if (x == 180f)
        {
            return 1;
        }
        else if (x == 90f)
        {
            return 2;
        }
        else if (x == 0f)
        {
            return 3;
        }
        return 0;

    }
}
