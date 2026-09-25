using UnityEngine;

public class Herd : MonoBehaviour
{
    GameObject platte1;
    GameObject platte2;
    GameObject platte3;
    GameObject platte4;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platte1 = GameObject.Find("platte1");
        platte2 = GameObject.Find("platte2");
        platte3 = GameObject.Find("platte3");
        platte4 = GameObject.Find("platte4");

        platte1.GetComponent<Collider>().isTrigger = true;
        platte2.GetComponent<Collider>().isTrigger = true;
        platte3.GetComponent<Collider>().isTrigger = true;
        platte4.GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void collider1Listener()
    {

    }
    void collider2Listener()
    {

    }
    void collider3Listener()
    {

    }
    void collider4Listener()
    {

    }
}
