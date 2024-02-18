using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public enum Language
{
    English,
    VietName,
    Portugal,
    Philipin
}

public class LanguageMN : Singleton<LanguageMN>
{
    public TextAsset languageFile;

    internal static Action OnChangeLanguage;

    static Dictionary<string, string> Dictionary = new Dictionary<string, string>();

    public static Language currLanguage = Language.English;

    private void Start()
    {
        LoadLanguage();
    }

    private void LoadLanguage()
    {
        currLanguage = (Language)PlayerPrefs.GetInt("Language", 0);
        Read();
    }

    private static void SaveLanguage()
    {
        PlayerPrefs.SetInt("Language", (int)currLanguage);
        PlayerPrefs.Save();
    }

    public static void Read()
    {
        
        if (Instance.languageFile == null) return;

        Dictionary.Clear();

        var text = ReplaceMarkers(Instance.languageFile.text);
        var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");

        foreach (Match match in matches)
        {
            text = text.Replace(match.Value, match.Value.Replace("\"", null).Replace(",", "[comma]").Replace("\n", "[newline]"));

        }

        var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        var languages = lines[0].Split(',').Select(i => i.Trim()).ToList();

        for (var i = 1; i < lines.Length; i++)
        {
            var columns = lines[i].Split(',').Select(j => j.Trim()).Select(j => j.Replace("[comma]", ",").Replace("[newline]", "\n")).ToList();
            var key = columns[0];

            for (var j = 1; j < languages.Count; j++)
            {
                if (languages[j] == currLanguage.ToString()) {
                    if (!Dictionary.ContainsKey(key))
                        Dictionary.Add(key, columns[j]);

                    else
                        Dictionary[key] = columns[j];
                }
            }
        }
    }

    private static string ReplaceMarkers(string text)
    {
        return text.Replace("[Newline]", "\n");
    }



    public static void SetLanguage(Language lang)
    {     
        currLanguage = lang;
        SaveLanguage();
        Read();
        if (OnChangeLanguage != null)
            OnChangeLanguage();

    }    

    internal static string GetText(string Key)
    {        
        if (Dictionary.ContainsKey(Key))
        {
            return Dictionary[Key];
        }

        return Key;
    }
}
