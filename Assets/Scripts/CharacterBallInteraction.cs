using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBallInteraction : MonoBehaviour
{
    public Transform rightHand;
    public Transform ballHoldTransform;
    public bool holdingBall;
    public Rigidbody ball;
    public Transform hoopPos;
    public int shotPower;
    public int shotHeight;
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
        Debug.Log("Distance: "+ Vector3.Distance(transform.position,hoopPos.position));
        Vector3 hoopNewPos = new Vector3(hoopPos.position.x, shotHeight, hoopPos.position.z);
        Vector3 direction = (hoopNewPos - transform.position).normalized;
        ball.AddForce(direction * shotPower);
        holdingBall = false;
    }
}
