using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IntrGO : MonoBehaviour
{
    // INTERACTIVE GAME OBJECT

    protected Transform player;
    protected bool isClose, wasClose = false;
    [SerializeField] protected bool isPerson = false;
    [SerializeField] private float minDistance = 15f;
    protected string intrName = "OBJECT";
    [SerializeField] protected string namePerson; // Used when is a person
    [SerializeField] protected string keyWord; // Used when is a object

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.GetComponent<Transform>();
        SetName();
    }

    private void Update()
    {
        CheckDistance();
    }

    protected void CheckDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= minDistance)
        {
            wasClose = isClose;
            isClose = true;

            if (isClose && !wasClose)
            {
                // Call for UI notification...
                ShowNotification(true);
            }
            // Check for player input...
            CheckForInput();
        }
        else
        {
            wasClose = isClose;
            isClose = false;

            if (!isClose && wasClose)
            {
                // Clean UI notification...
                ShowNotification(false);
            }
        }
    }

    private void ShowNotification(bool command)
    {

        MngParent.Instance.uiManager.GetComponent<UIManager>().ShowNotification(command, intrName);
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected abstract void Interact();

    protected void SetName()
    {
        if (isPerson)
        {
            intrName = namePerson;
        }
        else
        {
            intrName = MngParent.Instance.uiManager.GetComponent<UIManager>().GetTranslatedWord(keyWord);
        }
    }

}
