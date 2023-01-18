using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UStaticPool<T> where T : Component
{
    public Action<T> onEveryObjectCreated;
    private Queue<T> queue;
    private Vector3 hidePosition;
    private T prefab;
    public int Count => queue.Count;

    public UStaticPool(T prefab, Vector3 hidePosition, Action<T> onEveryObjectCreated = null)
    {
        this.prefab = prefab;
        this.hidePosition = hidePosition;
        if (onEveryObjectCreated != null) this.onEveryObjectCreated = onEveryObjectCreated;
        queue = new Queue<T>();
    }

    public T GetObject()
    {
        try
        {
            var result = queue.Dequeue();
            queue.Enqueue(result);
            return result;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public void CreateObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            T obj = GameObject.Instantiate(prefab, hidePosition, Quaternion.identity);
            obj.gameObject.SetActive(false);
            queue.Enqueue(obj);
            if (onEveryObjectCreated != null) onEveryObjectCreated(obj);
        }
    }
}