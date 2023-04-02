using System.Collections.Generic;
using UnityEngine;



public  class ObjectPool
{   
    private GameObject _prefab;
    private int _poolCapacity = 1;
    public ObjectPool(GameObject prefab, int poolCapacity)
    {
        _prefab= prefab;
        _poolCapacity = poolCapacity;
        CreatePool();
    }
    protected Queue<GameObject> _objectPool = new Queue<GameObject>();

    public GameObject GetFromPool()
    {        
        if (_objectPool.Count == 0)        
            CreateNewPoolObject();        

        GameObject newPoolObject = _objectPool.Dequeue();        
        return newPoolObject;
    } 
    
    public void ReturnToPool(GameObject objectToReturn)
    {        
        objectToReturn.gameObject.SetActive(false);
        _objectPool.Enqueue(objectToReturn);
    }
    
    private void CreatePool()
    {
        for (int i = 0; i < _poolCapacity; i++)        
            CreateNewPoolObject();       
    }   
    private void CreateNewPoolObject()
    {
        GameObject newObject = GameObject.Instantiate(_prefab);
                
        newObject.SetActive(false);
        _objectPool.Enqueue(newObject);
    }
}
