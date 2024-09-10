using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Actor, IInitializable, IUpdatable
{
    public BaseEnemy() : base()
    {

    }

    protected override void CustomStart()
    {
        base.CustomStart();
        Debug.Log("im a base enemy");
    }


    void IUpdatable.ActorUpdate()
    {
        Debug.Log("update " + prefabName);
    } 
}