using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

public class GameManager : Instancable<GameManager>
{
    [Header("General")]
    public Ball ball;

    public CinemachineFreeLook cinemachineFreeLook;
    
    [Header("FirstBasket")]
    public Transform aboveRimFirst;
    
    [Header("FirstBasket")]
    public Transform hoopFirst;
}
