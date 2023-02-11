using System.Collections;
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
    public Transform aboveRim;
    public float shootingAbility;
    public float rotateAmount = -1000f;
    public float shootingTime = 1f;
    public float shootingCurve = 3f;
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
            ball.transform.localScale = Vector3.one * 0.35f;
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
        yield return new WaitForEndOfFrame();
        ball.GetComponent<SphereCollider>().isTrigger = false;
    }

    public void JumpShot()
    {
        ball.GetComponent<Ball>().isTakeable = false;
        ball.GetComponent<Ball>().OpenTakeAble();
        StartCoroutine(OpenCollider());
        Transform ballT = ball.transform;
        ballT.parent = null;
        ball.isKinematic = false;

        ballT.LookAt(aboveRim);
        var aboveRimPos = aboveRim.position;
        var sequence = DOTween.Sequence()
            .Append(ball.transform.DOMoveX(aboveRimPos.x + Random.Range(-1f, 1f) / shootingAbility, shootingTime)
                .SetEase(Ease.Linear))
            .Join(ball.transform.DOMoveZ(aboveRimPos.z + Random.Range(-1f, 1f) / shootingAbility, shootingTime)
                .SetEase(Ease.Linear))
            .Join(ball.transform.DOMoveY(aboveRimPos.y + shootingCurve, shootingTime / 2f).OnComplete(() =>
            {
                ball.transform.DOMoveY(aboveRim.position.y, shootingTime / 2f).SetEase(Ease.InSine);
            }).SetEase(Ease.OutSine))
            .Join(ball.transform.DORotate(new Vector3(rotateAmount,0,0), shootingTime).SetEase(Ease.InOutCubic)
                .SetRelative(true));
        
        holdingBall = false;
    }
}
