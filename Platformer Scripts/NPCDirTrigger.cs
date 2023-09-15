using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDirTrigger : MonoBehaviour
{   
    [HideInInspector] public bool isPlayerDetected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = false;
        }
    }
}
