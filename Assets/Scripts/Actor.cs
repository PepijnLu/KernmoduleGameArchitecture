using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Observer, IInitializable
{
    protected GameObject newObj;
    protected string prefabName;

    public Actor()
    {

    }

    void IInitializable.Initialize(string prefabName, Vector3 position)
    {
        this.prefabName = prefabName;

        try
        {
            var prefab = Resources.Load<GameObject>(prefabName);
            newObj = GameObject.Instantiate(prefab);

            newObj.transform.position = position;
            Debug.Log("I'm " + prefabName);
        }
        catch (Exception ex)
        {
            Debug.Log("prefab not found");
        }

        CustomStart();
    }

    protected virtual void CustomStart()
    {
        
    }
}
