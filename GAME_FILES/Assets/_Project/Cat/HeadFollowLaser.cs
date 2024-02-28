using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadFollowLaser : MonoBehaviour
{
    GameObject defaultPosition;

    void Awake()
    {
        ReturnIdlePosition();
        defaultPosition.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {

    }

    public void AnimateHead(GameObject firstPosition, GameObject secondPosition, float duration)
    {   

    
    }

    public void LookAtFirstPosition(GameObject firstPosition, float duration)
    {
        AnimateHead(gameObject, firstPosition, duration);
    }

    public void ReturnIdlePosition()
    {
       AnimateHead(gameObject, defaultPosition , 1f);
    }

    IEnumerator AnimateLerp(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(endPos - startPos, Vector3.up);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        transform.rotation = endRotation;
    }

    

}
