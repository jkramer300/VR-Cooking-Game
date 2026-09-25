using UnityEngine;
using TMPro;

public class Uhr : MonoBehaviour
{
    public GameObject min;
    public GameObject sec;
    public GameObject start;
    public TMP_Text zeitAnzeige;
    public bool stop = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
    void Timer()
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
            CancelInvoke("Timer");
            GetComponent<AudioSource>().Play();
        }
        zeitAnzeige.text = h + ":" + m;
    }
    public void StartButtonClicked()
    {
        if(stop)
        {
            InvokeRepeating("Timer", 0f, 1f);
            stop = false;
        }
        else
        {
            CancelInvoke("Timer");
            stop = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    void Run()
    {
        if (!stop)
        {
            
        }
    }
    
    void ChangeTime(string type)
    {   
        string _text = zeitAnzeige.text;
        string _min = _text.Substring(0,2);
        string _sec= _text.Substring(3,2);

        if(type == "m")
        {
            _min = AddToString(_min);
            if(_min.Length > 2)
            {
                _min = _min.Substring(1,2);
            }
        }
        else
        {   
            _sec = AddToString(_sec);
            if(_sec.Length > 2)
            {
                _sec = _sec.Substring(1,2);
                ChangeTime("m");
            }
        }
        zeitAnzeige.text = _min + ":" + _sec;
    }
    string AddToString(string _text)
    {
        int num = int.Parse(_text);
        num += 1;
        _text = num.ToString();
        return _text;
    }
}
