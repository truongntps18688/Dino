using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LanguageText : MonoBehaviour
{
    public string LocalizationKey;

    Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
        Localize();

        LanguageMN.OnChangeLanguage += Localize;
    }


    public void OnDestroy()
    {
        LanguageMN.OnChangeLanguage -= Localize;
    }

    private void Localize()
    {

        string str = LanguageMN.GetText(LocalizationKey);
        if(str != "")
        {
            txt.text = str;
        }
    }
}
