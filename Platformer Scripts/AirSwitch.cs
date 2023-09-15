using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSwitch : MonoBehaviour
{   
    [SerializeField] private bool isSwitchOn;
    [SerializeField] private GameObject switchOn;
    [SerializeField] private GameObject switchOff;
    [SerializeField] private GameObject airCurrent1;
    [SerializeField] private GameObject airCurrent2;

    [SerializeField] private AudioClip airSwitchClip;

    void Start()
    {
        RenderSwitch();
    }

    void Update()
    {
        if(isSwitchOn)
        {   
            if(airCurrent1 != null) airCurrent1.gameObject.SetActive(true);
            if(airCurrent2 != null) airCurrent2.gameObject.SetActive(true);
        }
        else if(!isSwitchOn)
        {   
            if(airCurrent1 != null) airCurrent1.gameObject.SetActive(false);
            if(airCurrent2 != null) airCurrent2.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            AudioManager.instance.PlaySFX(airSwitchClip);
            if(isSwitchOn) isSwitchOn = false;
            else if(!isSwitchOn) isSwitchOn = true;
            RenderSwitch();
        }
    }

    private void RenderSwitch()
    {
        if(isSwitchOn)
        {
            switchOn.gameObject.SetActive(true);
            switchOff.gameObject.SetActive(false);
        }
        else if(!isSwitchOn)
        {
            switchOn.gameObject.SetActive(false);
            switchOff.gameObject.SetActive(true);
        }
    }
}
