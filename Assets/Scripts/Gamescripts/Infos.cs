using Unity.VisualScripting;
using UnityEngine;

public class Infos : MonoBehaviour
{
    public GameObject activeInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeInfo(string info)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).name == info)
            {
                if(activeInfo != null)
                {
                    activeInfo.SetActive(false);
                }
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                activeInfo = gameObject.transform.GetChild(i).gameObject;

            }
        }
    }
    public void CloseInfo()
    {
        activeInfo.SetActive(false);
    } 
}
