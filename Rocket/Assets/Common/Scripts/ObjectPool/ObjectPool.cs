using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ObjectPool
{
    private readonly Stack<GameObject> _stack = new Stack<GameObject>();
    private readonly GameObject _prefab;
    private readonly Transform _root;
    public ObjectPool(GameObject prefab)
    {
        _prefab = prefab;
        _root = new GameObject($"[{_prefab.name}]").transform;
    }

    protected virtual GameObject Instantiate()
    {
        return Object.Instantiate(_prefab);
    }

    public GameObject Pop()
    {
        GameObject gameObject;
        if (_stack.Count == 0)
        {
            gameObject = Instantiate();
            gameObject.name = _prefab.name;
            
        }
        else
        {
            gameObject = _stack.Pop();
        }
        gameObject.SetActive(true);
        gameObject.transform.SetParent(null);
        return gameObject;
    }

    public void Push(GameObject gameObject)
    {
        _stack.Push(gameObject);
        gameObject.transform.SetParent(_root);
        gameObject.SetActive(false);
    }
    
    
}