using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class TeamManager : Instancable<TeamManager>
{
    public List<GameObject> teamMembers = new List<GameObject>();
    public int playerIndex;
}
