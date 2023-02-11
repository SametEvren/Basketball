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
    private CharacterAnimationController _characterAnimationController;
    

    private void Start()
    {
        _characterAnimationController = GetComponent<CharacterAnimationController>();
    }

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
        if (holdingBall && Input.GetButtonDown("Fire1"))
        {
            transform.LookAt(hoopPos);
            _characterAnimationController.PlayJumpShot();
        }

        if (holdingBall && Input.GetButtonDown("Fire2"))
        {
            if (TeamManager.Instance.playerIndex < TeamManager.Instance.teamMembers.Count - 1) TeamManager.Instance.playerIndex++;
            else TeamManager.Instance.playerIndex = 0;


                transform.LookAt(TeamManager.Instance.teamMembers[TeamManager.Instance.playerIndex].transform);
            _characterAnimationController.PlayPass();
        }
    }
}
