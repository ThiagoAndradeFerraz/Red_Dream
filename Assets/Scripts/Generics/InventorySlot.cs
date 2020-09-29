using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    string id;
    string imgName;

    public void LoadInfo(int index)
    {
        id = GlobalMng.Instance.inventoryList[index].getId();
        imgName = GlobalMng.Instance.inventoryList[index].getImgName();

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
}
