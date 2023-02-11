using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterBallActions : MonoBehaviour
{
    [SerializeField] private Player player;
    
    public void JumpShot()
    {
        GameManager gameManager = GameManager.Instance;
        
        var aboveRim = gameManager.aboveRimFirst;
        var ball = gameManager.ball;
        var ballT = ball.transform;
        var ballRigid = ball.GetComponent<Rigidbody>();
        
        ball.isTakeable = false;
        ball.OpenTakeAble();
        
        StartCoroutine(OpenCollider(ball.GetComponent<SphereCollider>()));
        
        ballT.parent = null;
        ballRigid.isKinematic = false;

        ballT.LookAt(aboveRim);
        var aboveRimPos = aboveRim.position;

        #region DOTween Shooting Sequence
        var sequence = DOTween.Sequence()
            .Append(ball.transform.DOMoveX(aboveRimPos.x + Random.Range(-1f, 1f) / player.shootingAbility, player.shootBallMoveTime)
                .SetEase(Ease.Linear))
            .Join(ball.transform.DOMoveZ(aboveRimPos.z + Random.Range(-1f, 1f) / 100, player.shootBallMoveTime)
                .SetEase(Ease.Linear))
            .Join(ball.transform.DOMoveY(aboveRimPos.y + player.shootArc, player.shootBallMoveTime / 2f).OnComplete(() =>
            {
                ball.transform.DOMoveY(aboveRim.position.y, player.shootBallMoveTime / 2f).SetEase(Ease.InSine);
            }).SetEase(Ease.OutSine))
            .Join(ball.transform.DORotate(new Vector3(-player.ballSpinWhileMoving,0,0), 1).SetEase(Ease.InOutCubic)
                .SetRelative(true));
        #endregion
        
        GetComponent<CharacterBallInteraction>().holdingBall = false;
    }

    public void PassTheBall()
    {
        GameManager gameManager = GameManager.Instance;
        var ball = gameManager.ball;
        var ballT = ball.transform;
        var ballRigid = ball.GetComponent<Rigidbody>();
        
        ball.isTakeable = false;
        ball.OpenTakeAble();
        StartCoroutine(OpenCollider(ball.GetComponent<SphereCollider>()));
        ballT.parent = null;
        ballRigid.isKinematic = false;
        var sequence = DOTween.Sequence()
            .Append(ball.transform.DOMove(
                TeamManager.Instance.teamMembers[TeamManager.Instance.playerIndex].transform.position, 0.5f));
        
        for (int i = 0; i < TeamManager.Instance.teamMembers.Count; i++)
        {
            TeamManager.Instance.teamMembers[i].GetComponent<CharacterMovement>().enabled = false;
        }

        TeamManager.Instance.teamMembers[TeamManager.Instance.playerIndex].GetComponent<CharacterMovement>().enabled =
            true;

        gameManager.cinemachineFreeLook.Follow = TeamManager.Instance.teamMembers[TeamManager.Instance.playerIndex].transform;
        gameManager.cinemachineFreeLook.LookAt = TeamManager.Instance.teamMembers[TeamManager.Instance.playerIndex].transform;
        
        GetComponent<CharacterBallInteraction>().holdingBall = false;

       
    }
    
    IEnumerator OpenCollider(SphereCollider ballCollider)
    {
        yield return new WaitForEndOfFrame();
        ballCollider.isTrigger = false;
    }
}
