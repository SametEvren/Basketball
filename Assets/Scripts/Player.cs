using UnityEngine;

[CreateAssetMenu(fileName = "New Player",menuName = "Players/Player")]
public class Player : ScriptableObject
{
    public new string name;
    public string surname;
    public float shootBallMoveTime;
    public float shootArc;
    public float ballSpinWhileMoving;
    public float shootingAbility;
}
