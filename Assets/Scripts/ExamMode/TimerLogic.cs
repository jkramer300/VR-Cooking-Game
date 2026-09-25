using UnityEngine;
using TMPro;
public class TimerLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text zeitAnzeige;
    public bool stop = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartTimer();
    }

    void Timer()
    {
        string m = zeitAnzeige.text.Substring(0, 2);
        string s = zeitAnzeige.text.Substring(3, 2);

        if (int.Parse(s) < 59)
            if (int.Parse(s) < 9)
                s = $"0{int.Parse(s) + 1}";
            else
                s = $"{int.Parse(s) + 1}";
        else if (int.Parse(m) < 59)
        {
            s = "00";
            if (int.Parse(m) < 9)
            {
                m = $"0{int.Parse(m) + 1}";
            }
            else m = $"{int.Parse(m) + 1}";
        }
        zeitAnzeige.text = m + ":" + s;
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
    
    public void StartTimer()
    {
        InvokeRepeating("Timer", 0f, 1f);
    }
    public void StopTimer()
    {
        CancelInvoke("Timer");
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