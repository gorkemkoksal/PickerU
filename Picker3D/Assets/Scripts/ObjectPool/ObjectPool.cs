using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] pooledObjects;
    List<GameObject> gameobjectsInScene = new List<GameObject>();
    void Start()
    {
        pooledObjects = GameObject.FindGameObjectsWithTag("Collectables");

        //for (int i = 0; i < pooledObjects.Length; i++)
        //{
        //    if (gameObject.scene == pooledObjects[i].scene) //I tried this but it did not work.Then I thought variety is better.
        //        gameobjectsInScene.Add(pooledObjects[i]);
        //}
        //pooledObjects = gameobjectsInScene.ToArray();
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Length; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
