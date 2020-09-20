using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TxtObj : MonoBehaviour
{
    // Only used in TEXT OBJECTS!!!

    [SerializeField]
    private string localizationKey;

    public void ShowText(bool command)
    {
        string text = (command) ? GetLocalizedValue(localizationKey) : " ";
        gameObject.GetComponent<Text>().text = text;
    }

    private string GetLocalizedValue(string key)
    {
        string text;

        if (key != " " && key != null)
        {
            text = LangMng.Instance.GetValueMenu(key);
        }
        else
        {
            text = "[EMPTY KEY]";
        }

        return text;
    }
}
