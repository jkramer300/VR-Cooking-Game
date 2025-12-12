using UnityEngine;
using Hanzzz.MeshSlicerFree;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class CutObject : MonoBehaviour
{
    MeshSlicer meshSlicer = new MeshSlicer();
    int numberOfCutables = 0;
    private Collider knifeCollider;
    public GameObject knife1;
    public GameObject knife2;
    public GameObject knife3;
    public AudioSource cutSound;
    Renderer _renderer;
    bool wait = false;
    // Start is called before the first frame update
    void Start()
    {
        knifeCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (numberOfCutables > 100 || wait)
        {
            return;
        }
        wait = true;
        if (other.gameObject.layer == 8)
        {

            GameObject cutable = other.gameObject;
            _renderer = cutable.GetComponent<Renderer>();
            float[] T = {cutable.transform.position.x, cutable.transform.position.y, cutable.transform.position.z,
            cutable.transform.rotation.x, cutable.transform.rotation.y, cutable.transform.rotation.z,
            cutable.transform.localScale.x, cutable.transform.localScale.y, cutable.transform.localScale.z};
            (GameObject, GameObject) res;

            try
            {
                res = meshSlicer.Slice(cutable, (knife1.transform.position, knife2.transform.position, knife3.transform.position), _renderer.material);
            }
            catch (System.Exception e)
            {
                Debug.Log("CutError: " + e.Message);
                return;
            }

            //Debug.Log($"Test1: {res.Item1}, {T[0]}, {T[1]}, {T[2]}, {T[6]}, {T[7]}, {T[8]}");
            //Debug.Log($"Test2: {res.Item2}, {T[0]}, {T[1]}, {T[2]}, {T[6]}, {T[7]}, {T[8]}");


            if (res.Item1 == null)
            {
                Debug.Log($"Item1");
                Debug.Log($"{res}");
                wait = false;
                return;

            }

            if (res.Item2 == null)
            {
                Debug.Log($"Item2");
                wait = false;
                return;
            }

            TransformClones(res.Item1, T[0], T[1], T[2], T[6], T[7], T[8]);
            TransformClones(res.Item2, T[0], T[1], T[2], T[6], T[7], T[8]);
            ChangeRigidBody(res.Item1);
            ChangeRigidBody(res.Item2);

            ChangeCollider(res.Item1);
            ChangeCollider(res.Item2);

            float q = cutable.GetComponent<Ingredient>().quantity;
            res.Item1.GetComponent<Ingredient>().quantity = q / 2;
            res.Item2.GetComponent<Ingredient>().quantity = q / 2;
            //ChangeGrabInteractable(res.Item1);
            //ChangeGrabInteractable(res.Item2);

            numberOfCutables += 1;

            Destroy(cutable);

            cutSound.Play();

        }
        wait = false;
    }

    void TransformClones(GameObject clone, float posX, float posY, float posZ, float scaleX, float scaleY, float scaleZ)
    {
        clone.transform.position = new Vector3(posX - 0.05f, posY, posZ);
        //res.Item1.transform.rotation = Quaternion.Euler(T[3], T[4], T[5]);
        clone.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
    void ChangeCollider(GameObject clone)
    {
        Collider oldCollider = clone.GetComponent<Collider>();
        if (oldCollider != null)
        {
            Destroy(oldCollider);
        }
        MeshCollider newColider = clone.AddComponent<MeshCollider>();
        newColider.convex = true;
    }

    void ChangeRigidBody(GameObject clone)
    {
        Rigidbody rig = clone.GetComponent<Rigidbody>();
        rig.isKinematic = false;
        rig.useGravity = true;

    }

    void ChangeGrabInteractable(GameObject clone)
    {
        Destroy(clone.GetComponent<XRGrabInteractable>());

        if (GetComponent<XRGrabInteractable>() == null)
        {
            clone.AddComponent<XRGrabInteractable>();
        }
    }

    void ChangeLayer(GameObject clone)
    {
        clone.layer = LayerMask.NameToLayer("Test");
    }
    // Update is called once per frame
    void Update()
    {
    }
}
