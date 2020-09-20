using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Language
{
    PTBR, ENG
}

public class LangMng : MonoBehaviour
{
    // *****************
    // LANGUAGE MANAGER
    // *****************

    private Language language;
    private string folderLang = "PT-BR"; // PT-BR by default
    [SerializeField]
    public List<LocalizedEntry> dictionaryMenus = new List<LocalizedEntry>();

    private TextAsset txtFile;
    string strFile;
    [SerializeField]
    string[] partsOfLine;

    public static LangMng Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetLanguage(Language.PTBR);
        FillDictionaryMenus();
        Debug.Log("teste: " + GetValueMenu("QUIT_ID"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handle language selection ********
    public void SetLanguage(Language language)
    {
        this.language = language;

        switch (this.language)
        {
            case Language.PTBR:
                folderLang = "PT-BR";
                break;

            case Language.ENG:
                folderLang = "ENG-US";
                break;
        }
    }

    public string getLanguageFolder()
    {
        return folderLang;
    }

    //*********************************

    // Load menu dicionary
    public void FillDictionaryMenus() 
    {
        string path = folderLang + "/MENUS/MENU";
        txtFile = Resources.Load<TextAsset>(path);
        strFile = txtFile.text;

        string[] linesArray = strFile.Split('\n'); // Splitting by line
        
        for (int i = 0; i < linesArray.Length; i++)
        {
            partsOfLine = linesArray[i].Split('|');
            dictionaryMenus.Add(new LocalizedEntry(partsOfLine[0], partsOfLine[1]));
            //Debug.Log(dictionaryMenus[i].value);
        }
    }

    public string GetValueMenu(string key)
    {
        string item = "[INVALID KEY]";

        if (!dictionaryMenus.Exists(x => x.id == key))
        {
            Debug.Log("Invalid Key! " + key);
            
        }
        else
        {
            item = dictionaryMenus.Find(x => x.id == key).value;
        }

        return item;
    }
}
