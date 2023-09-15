using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{   
    private Vector2 startPosition;
    [SerializeField] private GameObject virtualCamera;
    [SerializeField] private float parallaxSpeed;
    
    private void Start()
    {
        virtualCamera = GameObject.Find("CM vcam1");
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {   
        float distanceX = (virtualCamera.transform.position.x * parallaxSpeed);
        float distanceY = (virtualCamera.transform.position.y * parallaxSpeed);
        Vector2 distanceVector = new Vector2(virtualCamera.transform.position.x * parallaxSpeed, virtualCamera.transform.position.y * parallaxSpeed);
        transform.position = new Vector3(startPosition.x + distanceVector.x, startPosition.y + distanceVector.y, transform.position.z);
    }
}