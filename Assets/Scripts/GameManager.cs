using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class GameManager : Instancable<GameManager>
{
    [Header("General")]
    public Ball ball;
    
    [Header("FirstBasket")]
    public Transform aboveRimFirst;
    
    [Header("FirstBasket")]
    public Transform hoopFirst;
}
