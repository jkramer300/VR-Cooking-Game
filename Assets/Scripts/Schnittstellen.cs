using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;


public class Schnittstellen : MonoBehaviour
{
    public int type = 0;
    public int num;
    public GameObject Cuttable;
    private bool canBeenCalled = true;
    public AudioSource cutSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        canBeenCalled = true;
    }
    void OnTriggerStay(Collider other)
    {
        if (num > 0 && other.CompareTag("Messer") && type == 1 && CheckCollider(other) && canBeenCalled)
        {
            canBeenCalled = false;
            Debug.Log(other.gameObject.transform.eulerAngles);
            Debug.Log(transform.localEulerAngles);
            Cuttable.GetComponent<Schneiden>().Cutting(gameObject);
            other.gameObject.GetComponent<AudioSource>().Play();
        }
        else if (other.CompareTag("Messer") && type == 0 && CheckCollider(other) && canBeenCalled)
        {
            canBeenCalled = false;
            Cuttable.GetComponent<Schneiden2>().Cutting(gameObject);
            other.gameObject.GetComponent<AudioSource>().Play();
        }

    }
    bool CheckCollider(Collider other)
    {
        Collider col = GetComponent<Collider>();
        return other.bounds.Contains(col.bounds.min) && other.bounds.Contains(col.bounds.max);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
