using TMPro;
using UnityEngine;


public class Backofensteuerung : MonoBehaviour
{
    public TMP_Text zeitAnzeige;
    public AudioSource ovenTimerSound;
    public AudioSource ovenSound;
    public CurrentRecipe recipe;
    public GameObject mode;
    public GameObject degree;
    GameObject inhalt = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StundenButtonClicked()
    {
        string h = zeitAnzeige.text.Substring(0, 2);
        string m = zeitAnzeige.text.Substring(3, 2);

        if (int.Parse(h) < 59)
            if (int.Parse(h) < 9)
                h = $"0{int.Parse(h) + 1}";
            else
                h = $"{int.Parse(h) + 1}";
        else h = "00";

        zeitAnzeige.text = h + ":" + m;

    }
    public void MinutenButtonClicked()
    {
        string h = zeitAnzeige.text.Substring(0, 2);
        string m = zeitAnzeige.text.Substring(3, 2);

        if (int.Parse(m) < 59)
            if (int.Parse(m) < 9)
                m = $"0{int.Parse(m) + 1}";
            else
                m = $"{int.Parse(m) + 1}";
        else if (int.Parse(h) < 59)
        {
            m = "00";
            if (int.Parse(h) < 9)
            {
                h = $"0{int.Parse(h) + 1}";
            }
            else h = $"{int.Parse(h) + 1}";
        }
        else
        {
            m = "00";
            h = "00";
        }
        zeitAnzeige.text = h + ":" + m;
    }
    public void StartButtonClicked()
    {
        if (StartOven())
        {
            ovenSound.Play();
            InvokeRepeating("OfenTimer", 0f, 1f);
        }
    }
    public void SkipButtonClicked()
    {
        zeitAnzeige.text = "00:00";
        CancelInvoke("OfenTimer");
    }
    public void ClearButtonClicked()
    {
        zeitAnzeige.text = "00:00";
        CancelInvoke("OfenTimer");
    }
    void OfenTimer()
    {
        string h = zeitAnzeige.text.Substring(0, 2);
        string m = zeitAnzeige.text.Substring(3, 2);

        if (int.Parse(m) > 0)
            if (int.Parse(m) < 11)
                m = $"0{int.Parse(m) - 1}";
            else
                m = $"{int.Parse(m) - 1}";
        else if (int.Parse(h) > 0)
        {
            m = "59";
            if (int.Parse(h) < 9)
            {
                h = $"0{int.Parse(h) - 1}";
            }
            else h = $"{int.Parse(h) - 1}";
        }
        else
        {
            m = "00";
            h = "00";
            CancelInvoke("OfenTimer");
            ovenSound.Stop();
            ovenTimerSound.Play();
            if (inhalt != null)
                inhalt.GetComponent<KuchenForm>().CakeFinished();
        }
        zeitAnzeige.text = h + ":" + m;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Form")
        {
            inhalt = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Form")
        {
            inhalt = null;
        }
    }
    bool StartOven()
    {
        if (inhalt != null)
        {
            Debug.Log($"{recipe.currentRecipe.ovenMode}, {recipe.currentRecipe.ovenDegree}, {recipe.currentRecipe.ovenTime}");
            Debug.Log($"{CheckMode(mode)}, {CheckDegree(degree)}, {CheckTime()}");
            if (CheckMode(mode) == recipe.currentRecipe.ovenMode && CheckDegree(degree) == recipe.currentRecipe.ovenDegree && CheckTime() == recipe.currentRecipe.ovenTime)
                return true;
        }
        return false;
    }
    int CheckMode(GameObject _gameObject)
    {
        float x = _gameObject.GetComponent<Drehschalter>().rotation;
        if (x == 270f)
        {
            return 1;
        }
        else if (x == 180f)
        {
            return 2;
        }
        else if (x == 90f)
        {
            return 3;
        }
        else if (x == 0f)
        {
            return 4;
        }
        return 0;

    }
    int CheckDegree(GameObject _gameObject)
    {
        float x = _gameObject.GetComponent<Drehschalter>().rotation;
        if (x == 270f)
        {
            return 0;
        }
        else if (x == 180f)
        {
            return 100;
        }
        else if (x == 90f)
        {
            return 150;
        }
        else if (x == 0f)
        {
            return 200;
        }
        return 0;
    }
    int CheckTime()
    {
        return int.Parse(zeitAnzeige.text.Substring(0, 2)) * 100 + int.Parse(zeitAnzeige.text.Substring(3, 2));
    }
}
