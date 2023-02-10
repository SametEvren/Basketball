using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CharacterBallInteraction : MonoBehaviour
{
    public Transform rightHand;
    public Transform ballHoldTransform;
    public bool holdingBall;
    public Rigidbody ball;
    public Transform hoopPos;
    public int shotPower;
    public int shotHeight;
    public Transform aboveRim;
    public float shootingAbility;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Ball"))
        {
            if (!other.GetComponent<Ball>().isTakeable)
                return;
            var o = other.gameObject;
            o.GetComponent<SphereCollider>().isTrigger = true;
            o.transform.parent = rightHand;
            o.transform.position = ballHoldTransform.position;
            ball.isKinematic = true;
            holdingBall = true;
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("Distance: "+ Vector3.Distance(transform.position,hoopPos.position));
        if (holdingBall && Input.GetButtonDown("Fire1"))
        {
            transform.LookAt(hoopPos);
            GetComponent<CharacterAnimationController>().PlayJumpShot();
        }
    }

    IEnumerator OpenCollider()
    {
        yield return new WaitForSeconds(0.5f);
        ball.GetComponent<SphereCollider>().isTrigger = false;
    }

    public void JumpShot()
    {
        ball.GetComponent<Ball>().isTakeable = false;
        ball.GetComponent<Ball>().OpenTakeAble();
        StartCoroutine(OpenCollider());
        ball.transform.parent = null;
        ball.isKinematic = false;
        //Debug.Log("Distance: "+ Vector3.Distance(transform.position,hoopPos.position));

        // float distance = Vector3.Distance(transform.position, hoopPos.position);
        // if (distance >= 17)
        //     shotPower = 700;
        // if (distance >= 8 && distance < 17)
        //     shotPower = 600;
        // if (distance >= 5 && distance < 8)
        //     shotPower = 500;
        // if (distance >= 0 && distance < 5)
        //     shotPower = 450;
        //
        // Vector3 hoopNewPos = new Vector3(aboveRim.position.x, shotHeight, aboveRim.position.z);
        // Vector3 direction = (hoopNewPos - transform.position).normalized;
        // ball.AddForce(direction * shotPower);
        
        // Vector3[] path = { ball.transform.position, aboveRim.position };
        // ball.transform.DOPath(path, 1f, PathType.CatmullRom);
        ball.transform.DOMoveX(aboveRim.position.x + Random.Range(-1f,1f) / shootingAbility, 1f).SetEase(Ease.Linear);
        ball.transform.DOMoveZ(aboveRim.position.z + Random.Range(-1f,1f) / shootingAbility, 1f).SetEase(Ease.Linear);
        ball.transform.DOMoveY(aboveRim.position.y + 3, 0.5f).OnComplete(() =>
        {
            ball.transform.DOMoveY(aboveRim.position.y, 0.5f).SetEase(Ease.InSine);
        }).SetEase(Ease.OutSine);
        
        holdingBall = false;
    }
}
