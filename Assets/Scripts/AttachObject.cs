using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttachObject : MonoBehaviour
{
    public GameObject attachObject;
    public float yLocalDifference;
    public float xLocalAngleDifference;
    public GameObject attachParent = null;
    bool notInTrigger = true;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (attachObject.GetComponent<Collider>() == other && notInTrigger)
        {
            Debug.Log("Test1");
            notInTrigger = false;
            attachObject.GetComponent<Rigidbody>().isKinematic = true;
            attachObject.GetComponent<MeshCollider>().convex = false;
            attachObject.transform.position = transform.position + new Vector3(0, yLocalDifference * 0.15f, 0f);
            attachObject.transform.localEulerAngles = transform.localEulerAngles + new Vector3(xLocalAngleDifference, 0, 0f);
            if (attachObject.transform.parent != null)
            {
                Debug.Log($"{attachObject.transform.parent.name}");
                attachObject.transform.SetParent(transform, true);
                Debug.Log($"{attachObject.transform.parent.name}");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
    }
    public void PickUp()
    {
        if (attachObject.transform.parent != null)
        {
            Debug.Log("Test2");
            notInTrigger = true;
            attachObject.GetComponent<MeshCollider>().convex = true;
            //attachObject.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log($"{attachObject.transform.parent.name}");
            attachObject.transform.SetParent(attachParent.transform, true);
            Debug.Log($"{attachObject.transform.parent.name}");
        }
    }
}
