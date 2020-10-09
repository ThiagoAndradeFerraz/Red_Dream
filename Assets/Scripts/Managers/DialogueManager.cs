using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private string npcName;

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
        npcName = InvAndNPCmng.Instance.npcName;
        string characterFolder = npcName;
        //string characterFile = characterFolder + InvAndNPCmng.Instance.conversationNumber;
        string characterFile = characterFolder + "_" + InvAndNPCmng.Instance.descNextDialog;
        string path = langFolder + "/DIALOGUE/" + characterFolder + "/" + characterFile;

        txtAssetFile = Resources.Load<TextAsset>(path);
        strFile = txtAssetFile.text;
        string[] lines = strFile.Split('\n');

        listLinesDialogue.Clear();

        for(int i = 0; i < lines.Length; i++)
        {
            string[] partsOfLine = lines[i].Split('|');
            listLinesDialogue.Add(new DialogueEntry(partsOfLine[0], partsOfLine[1], partsOfLine[2], partsOfLine[3], partsOfLine[4]));
            //Debug.Log("[ARRAY] emotion npc: " + partsOfLine[3] + " emotion player: " + partsOfLine[4]);
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
                // Atualizando próximo dialogo...

                StateMng.Instance.GoIntr1State(InvAndNPCmng.Instance.npcName, InvAndNPCmng.Instance.descNextDialog);
                iterateCont = 0;

                //Debug.Log("acabou, reiniciando o contador...");

                //UImanager.Instance.ShowUI(UIState.TALK, false); // For some misterious reason, I can only deactivate the talk UI here

                // PUT A IF-ELSE HERE, CHECKING IF IS DEAD OR ALIVE! IF DEAD, GET OUT OF MENU, IF ALIVE, GO BACK TO INTERACT MENU
                //UImanager.Instance.ShowUI(UIState.INTERACTMENU1, true);
            }
        }
    }

    private void ShowLine(int index)
    {
        switch (listLinesDialogue[index].position)
        {
            case "R":
                //UImanager.Instance.txtNpcNameR.GetComponent<Text>().text = listLinesDialogue[index].name;
                UImanager.Instance.txtNpcNameR.GetComponent<Text>().text = "Cassandra"; // <-- PLAYER NAME HERE!!!
                UImanager.Instance.txtNpcNameL.GetComponent<Text>().text = " ";
                break;

            case "L":
                //UImanager.Instance.txtNpcNameL.GetComponent<Text>().text = listLinesDialogue[index].name;
                UImanager.Instance.txtNpcNameL.GetComponent<Text>().text = npcName;
                UImanager.Instance.txtNpcNameR.GetComponent<Text>().text = " ";
                break;

            default:
                Debug.Log("INVALID POSITION IN DIALOGUE FILE IN LINE " + (index+1));
                break;
        }

        UImanager.Instance.txtTalk.GetComponent<Text>().text = listLinesDialogue[index].line;

        LoadCharsImg(npcName, listLinesDialogue[index].emotionNPC);
        LoadCharsImg("Cassandra", listLinesDialogue[index].emotionPlayer);
        //LoadCharsImg("Cassandra", "NEUTRAL");

    }

    private void LoadCharsImg(string charName, string emotion)
    {
        string path = ImgPath(charName, emotion);
        if (charName != "Cassandra") // Not the player...
        {
            UImanager.Instance.imgTalkL.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        }
        else
        {
            UImanager.Instance.imgTalkR.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        }
    }

    private string ImgPath(string charName, string emotion)
    {
        // EXAMPLE: Arts/Characters/Cassandra/Cassandra_NEUTRAL
        string path = "Arts/Characters/" + charName + "/" + charName + "_" + emotion;
        return path;
    }
}
