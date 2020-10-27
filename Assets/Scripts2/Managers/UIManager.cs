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


public class UIManager : MonoBehaviour
{
    
    
    
    // Language related
    public string language = "PT-BR"; // portuguese by default
    
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

    }

    private void ShowBcg(bool command)
    {
        bcgImg.GetComponent<Image>().enabled = command;
    }




    public void StartDialogue(string name, string number)
    {
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

        isCutscene = true;
        cont = 1;

        //Debug.Log(fileToRead);

        /* To finish dialogue...
        ShowBcg(false);
        imgScene.GetComponent<Image>().enabled = false;
        txtLine.GetComponent<Text>().enabled = false;
        */
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
            }
        }
    }


}
