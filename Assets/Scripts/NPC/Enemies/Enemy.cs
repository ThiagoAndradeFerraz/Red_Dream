using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }


    private void LookAtPlayer()
    {

        transform.LookAt(new Vector3(FPPlayer.Instance.transform.position.x, transform.position.y, FPPlayer.Instance.transform.position.z));
    }

}
