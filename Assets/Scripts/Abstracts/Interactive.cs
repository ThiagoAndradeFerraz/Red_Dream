using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    [SerializeField] private float minDistance = 11f;
    [SerializeField] Transform player;
    protected bool isClose, wasClose = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    protected void CheckDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= minDistance)
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

            if(!isClose && wasClose)
            {
                // Clean UI notification...
                ShowNotification(false);
            }
        }
    }

    private void ShowNotification(bool command)
    {
        UImanager.Instance.ShowNotificationUI(command);
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected abstract void Interact();


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
