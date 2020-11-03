using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogEntry
{
    public string text;
    public string file;

    public DialogEntry(string text, string file)
    {
        this.text = text;
        this.file = file;
    }
}

public class DictionaryEntry
{
    public string id;
    public string word;

    public DictionaryEntry(string id, string word)
    {
        this.id = id;
        this.word = word;
    }
}

public class UIManager : MonoBehaviour
{
    
    // Language related
    public string language = "PT-BR"; // portuguese by default
    private List<DictionaryEntry> listDictionary = new List<DictionaryEntry>();

    // General
    private GameObject generalParent;
    private Transform dialogueParent;
    private Transform bcgImg;

    // Dialogue related
    private Transform imgScene, txtLine;
    private List<DialogEntry> listLines = new List<DialogEntry>();
    private bool isCutscene = false;
    private int cont;
    private string folderImgName;

    // Notification pop-up
    private Transform notificationPanel, txtName, txtInteract;

    // Elevator related
    private Transform elevatorPanel, elevatorBtnPanel, elevatorDispPanel, txtDisp;





    private void Awake()
    {
        LoadDictionary();

    }

    private void Start()
    {
        GetElements();
    }

    private void Update()
    {
        if (isCutscene)
        {
            IterateDialogue();
        }
    }

    private void GetElements()
    {
        // General
        generalParent = GameObject.FindGameObjectWithTag("uiGeneral");
        bcgImg = generalParent.GetComponent<Transform>().GetChild(0);

        // Dialogue
        dialogueParent = generalParent.GetComponent<Transform>().GetChild(1);
        imgScene = dialogueParent.GetChild(0);
        txtLine = dialogueParent.GetChild(1);

        // Notifiation pop-up
        notificationPanel = generalParent.GetComponent<Transform>().GetChild(2);
        txtInteract = notificationPanel.GetChild(0);
        txtName = notificationPanel.GetChild(1);

        // Elevator related
        elevatorPanel = generalParent.GetComponent<Transform>().GetChild(3);
        elevatorBtnPanel = elevatorPanel.GetChild(0);
        elevatorDispPanel = elevatorPanel.GetChild(1);
        txtDisp = elevatorDispPanel.GetChild(0);


    }

    private void ShowBcg(bool command)
    {
        bcgImg.GetComponent<Image>().enabled = command;
    }

    public void StartDialogue(string name, string number)
    {
        MngParent.Instance.playerCanWalk = false;
        // Loading the text file to a list 
        string txtPath = language + "/DIALOGUE/" + name + "/" + name + number;
        TextAsset txtAsset = Resources.Load<TextAsset>(txtPath);
        string[] strLines = txtAsset.text.Split('\n');
        
        for(int i = 0; i < strLines.Length; i++)
        {
            string[] strParts = strLines[i].Split('|');
            listLines.Add(new DialogEntry(strParts[0], strParts[1]));
        }

        // Showing the UI...
        //      Image...
        ShowBcg(true);
        imgScene.GetComponent<Image>().enabled = true;

        folderImgName = name;
        string imgPath = "Arts/Frames/" + folderImgName + "/" + listLines[0].file;
        imgScene.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
        
        //      Text...
        txtLine.GetComponent<Text>().enabled = true;
        txtLine.GetComponent<Text>().text = listLines[0].text;

        isCutscene = true; // Allows the IterateDialogue method to be used in the Update method...
        cont = 1;

    }

    private void IterateDialogue()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(cont < listLines.Count)
            {
                string imgPath = "Arts/Frames/" + folderImgName + "/" + listLines[cont].file;
                Debug.Log(imgPath);
                imgScene.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
                txtLine.GetComponent<Text>().text = listLines[cont].text;
                cont++;
            }
            else
            {
                ShowBcg(false);
                imgScene.GetComponent<Image>().enabled = false;
                txtLine.GetComponent<Text>().enabled = false;
                cont = 0;
                listLines.Clear();
                isCutscene = false;
                MngParent.Instance.playerCanWalk = true;
            }
        }
    }

    private void LoadDictionary()
    {
        // Assets/Resources/PT-BR/WORDS/WORDS.txt
        string path = language + "/WORDS/WORDS";
        TextAsset txtAsset = Resources.Load<TextAsset>(path);
        string[] lines = txtAsset.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            listDictionary.Add(new DictionaryEntry(parts[0], parts[1]));
            //Debug.Log(listDictionary[i].id);
        }
    }

    public string GetTranslatedWord(string key)
    {
        string word = "[INVALID KEY]";
        
        if(!listDictionary.Exists(x => x.id == key)){
            Debug.Log("[ERROR] Invalid key " + key);
        }
        else
        {
            word = listDictionary.Find(x => x.id == key).word;
        }

        return word;
    }

    public void ShowNotification(bool command, string objName)
    {
        notificationPanel.GetComponent<Image>().enabled = command;
        txtName.GetComponent<Text>().enabled = command;
        txtInteract.GetComponent<Text>().enabled = command;

        if (command)
        {
            txtName.GetComponent<Text>().text = objName;
            txtInteract.GetComponent<Text>().text = GetTranslatedWord("INTERACT_ID");
        }
    }

    public void ShowElevatorUI(bool command)
    {
        ShowBcg(command);
        ShowNotification(false, " ");
        elevatorPanel.GetComponent<Image>().enabled = command;
        elevatorBtnPanel.gameObject.SetActive(command);
        elevatorDispPanel.gameObject.SetActive(command);
    }

    // Update the number shown in the elevator display
    public void UpdateDisplay(char n)
    {
        if(txtDisp.GetComponent<Text>().text == "00")
        {
            txtDisp.GetComponent<Text>().text = "0" + n;
        }
        else if(txtDisp.GetComponent<Text>().text[0] == '0')
        {
            string prev = txtDisp.GetComponent<Text>().text[1].ToString();
            txtDisp.GetComponent<Text>().text = prev + n;
        }
        else
        {
            txtDisp.GetComponent<Text>().text = "0" + n;
        }
    }

    // Used outside to get the elevator display number
    public string GetDisplayNbr()
    {
        string nbr = txtDisp.GetComponent<Text>().text;
        return nbr;
    }

}
