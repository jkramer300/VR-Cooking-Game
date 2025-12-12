using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCollider : MonoBehaviour
{
    private Collider _collider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _collider = GetComponent<Collider>();
    }

    public void TickCollider()
    {
        _collider.enabled = true;
    }
    public void UnTickCollider()
    {
        _collider.enabled = false;
    }
}
