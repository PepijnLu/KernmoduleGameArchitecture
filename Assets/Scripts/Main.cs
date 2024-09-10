using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public List<IUpdatable> updatableActors = new List<IUpdatable>();
    // Start is called before the first frame update
    private void Start()
    {
        InstantiateActor<Actor>("Cube", new Vector3(0, 0, 0));
        InstantiateActor<BaseEnemy>("Sphere", new Vector3(2, 2, 2));
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (IUpdatable updActor in updatableActors)
        {
            updActor.ActorUpdate();
        }
    }

    private void InstantiateActor<T>(string prefabName, Vector3 position) where T : class, IInitializable, new()
    {
        T instance = new T();
        instance.Initialize(prefabName, position);

        if (instance is IUpdatable updatableInstance)
        {
            updatableActors.Add(updatableInstance);
        }
    }
}
