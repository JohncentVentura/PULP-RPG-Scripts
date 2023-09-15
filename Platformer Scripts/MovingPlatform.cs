using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float xMovement;
    [SerializeField] private float yMovement;
    [HideInInspector] private float zMovement;
    [SerializeField] private float rotationMovement;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTimer;

    [Header("Animations")]
    private Animator animator;
    [SerializeField] private string animationName;

    void Start()
    {
        try //Try if the MovingPlatform has an animator
        {
            animator = GetComponent<Animator>();
            
            if (AnimatorContainsParam("Direction"))
            {
                animator.SetFloat("Direction", xMovement);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        StartCoroutine(InverseCoroutine());
    }

    void Update()
    {
        if (animator != null)
        {
            if (animationName == "Tricycle1") animator.Play("Tricycle1");
            else if (animationName == "Tricycle2") animator.Play("Tricycle2");
            else if (animationName == "Tricycle3") animator.Play("Tricycle3");
            else if (animationName == "Car1") animator.Play("Car1");
            else if (animationName == "Car2") animator.Play("Car2");
            else if (animationName == "Car3") animator.Play("Car3");
            else if (animationName == "Bus1") animator.Play("Bus1");
            else if (animationName == "Bus2") animator.Play("Bus2");
            else if (animationName == "Bus3") animator.Play("Bus3");

            if (AnimatorContainsParam("Direction"))
            {
                if (xMovement == 1) animator.SetFloat("Direction", 1); //move to right
                else if (xMovement == -1) animator.SetFloat("Direction", 0); //move to left
            }
        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(xMovement, yMovement, zMovement) * Time.deltaTime * moveSpeed);
        transform.Rotate(new Vector3(0, 0, rotationMovement) * Time.deltaTime * moveSpeed);
    }

    private IEnumerator InverseCoroutine()
    {
        yield return new WaitForSeconds(moveTimer);
        xMovement = InverseNumber(xMovement);
        yMovement = InverseNumber(yMovement);
        zMovement = InverseNumber(zMovement);
        rotationMovement = InverseNumber(rotationMovement);
        StartCoroutine(InverseCoroutine());
    }

    private float InverseNumber(float number)
    {   
        if (number > 0) return -number; //if number is positive, make it negative
        else return Mathf.Abs(number); //if number is negative, make it positive, Mathf.Abs makes the number positive
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject; //get player GameObject
            player.transform.SetParent(transform); //set MovingPlatform the parent of the player
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject; //get player GameObject
            player.transform.SetParent(transform); //set MovingPlatform the parent of the player
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject; //get player GameObject
            player.transform.parent = null; //remove Player parent
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private bool AnimatorContainsParam(string paramName)
    {
        bool isParamExist = false;
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
            {
                isParamExist = true;
                break;
            }
        }
        return isParamExist;
    }
}