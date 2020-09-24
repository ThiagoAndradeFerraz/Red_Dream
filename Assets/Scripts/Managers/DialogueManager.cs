using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private TextAsset txtAssetFile;
    private string strFile;
    private List<DialogueEntry> listLinesDialogue = new List<DialogueEntry>();
    private int iterateCont = 0;

    public static DialogueManager Instance { get; private set; }

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

    private void Update()
    {
        
        //Debug.Log("update...");
        if(iterateCont > 0)
        {
            IterateList();
        }
    }


    public void StartDialogue()
    {
        LoadTextFile();
        ShowLine(iterateCont);
        iterateCont++;
    }

    private void LoadTextFile()
    {
        // Assets/Resources/PT-BR/DIALOGUE/Valquiria/Valquiria1 < - Example!
        //Debug.Log("loading file...");

        string langFolder = LangMng.Instance.getLanguageFolder();
        string characterFolder = StateMng.Instance.npcName;
        string characterFile = characterFolder + StateMng.Instance.conversationNumber;
        string path = langFolder + "/DIALOGUE/" + characterFolder + "/" + characterFile;

        txtAssetFile = Resources.Load<TextAsset>(path);
        strFile = txtAssetFile.text;
        string[] lines = strFile.Split('\n');

        listLinesDialogue.Clear();

        for(int i = 0; i < lines.Length; i++)
        {
            string[] partsOfLine = lines[i].Split('|');
            listLinesDialogue.Add(new DialogueEntry(partsOfLine[0], partsOfLine[1], partsOfLine[2], partsOfLine[3]));
            //Debug.Log(listLinesDialogue[i].line);
        }
    }

    private void IterateList()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click...");
            if(iterateCont < listLinesDialogue.Count)
            {
                ShowLine(iterateCont);
                iterateCont++;
            }
            else
            {
                //Debug.Log("acabou, reiniciando o contador...");
                
                UImanager.Instance.ShowUI(UIState.TALK, false); // For some misterious reason, I can only deactivate the talk UI here
                UImanager.Instance.ShowUI(UIState.INTERACTMENU1, true);

                
                iterateCont = 0;
            }
        }
    }

    private void ShowLine(int index)
    {
        UImanager.Instance.txtNpcNameL.GetComponent<Text>().text = listLinesDialogue[index].name;
        UImanager.Instance.txtTalk.GetComponent<Text>().text = listLinesDialogue[index].line;



        //talkPanel, imgTalkL, imgTalkR, txtNpcNameL, txtNpcNameR, txtTalk
    }


}
