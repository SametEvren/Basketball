using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isTakeable;
    
    public void OpenTakeAble()
    {
        StartCoroutine(OpenTakeAbleEnum());
    }
    
    IEnumerator OpenTakeAbleEnum()
    {
        yield return new WaitForSeconds(1f);
        isTakeable = true;
    }
}
