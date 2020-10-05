using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string id;
    string imgName;

    public void LoadInfo(int index)
    {
        id = InvAndNPCmng.Instance.inventoryList[index].getId();
        imgName = InvAndNPCmng.Instance.inventoryList[index].getImgName();

        LoadImg(imgName);
        gameObject.GetComponent<Button>().interactable = true;

        //Debug.Log(id);
    }

    public void LetItEmpty()
    {
        id = " ";
        LoadImg("EMPTY");
        gameObject.GetComponent<Button>().interactable = false;

        //Debug.Log("empty slot");
    }

    private void LoadImg(string imgName)
    {
        string path = "Arts/Itens/" + imgName;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
    }

    // Click method!
    public void SlotClick()
    {
        //Debug.Log(id);
        HighlightSlot();
        UImanager.Instance.LetSlotsClickable(false);
        UImanager.Instance.LetConfirmBtnClickable(true);
    }

    // When clicked
    private void HighlightSlot()
    {
        gameObject.GetComponent<Image>().color = Color.blue;
    }



    public string GetId()
    {
        return id;
    }

}
