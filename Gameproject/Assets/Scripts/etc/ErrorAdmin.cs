using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorAdmin {
    static public void ErrorMessegeFromObject(string _context,string _funcName, GameObject _ob)
    {
        if (_ob == null) {
            Debug.Log("[ErrorMessegeFromObject Function Error:: GameObject is Null ]");
        }
        string text = "[ <" + _ob.name + "> " + _funcName + "Function Error:: " + _context + " ]";
        Debug.Log(text);
    }

    static public void ErrorMessege(string _context, string _funcName)
    {
        string text = "[ "+ _funcName + " Function Error:: " + _context + " ]";
        Debug.Log(text);
    }

    static public void WarningMessegeFromObject(string _context, string _funcName, GameObject _ob)
    {
        if (_ob == null)
        {
            Debug.Log("[WarningMessegeFromObject Function Error:: GameObject is Null ]");
        }
        string text = "[ Warning <" + _ob.name + "> " + _funcName + ":: " + _context + " ]";
        Debug.Log(text);
    }

    static public void WarningMessege(string _context, string _funcName)
    {
        string text = "[ Warning" + _funcName + ":: " + _context + " ]";
        Debug.Log(text);
    }
}
