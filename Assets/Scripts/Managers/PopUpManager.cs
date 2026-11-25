using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    private float openDuration = 1f; 

    private Coroutine closeRoutine;

    public void ShowText(String text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");

        if (closeRoutine != null)
        {
            StopCoroutine(closeRoutine);
        }

        closeRoutine = StartCoroutine(CloseAfterDelay()); 
    }
    

    private IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(openDuration);
        animator.SetTrigger("close");
    }
}