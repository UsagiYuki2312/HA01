using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDynamicPool<T> where T : Component
{
    public UDynamicPool(T prefab, Vector3 hidePosition, int appendNumbers, int poolCapacity = 4, Action<T> onEveryObjectCreated = null)
    {
        this.hidePosition = hidePosition;
        this.prefab = prefab;
        this.appendNumbers = appendNumbers;
        this.index = 0;
        if (onEveryObjectCreated != null) this.onEveryObjectCreated = onEveryObjectCreated;
        list = new List<T>(poolCapacity);
    }

    public UDynamicPool(T prefab, Vector3 hidePosition, Transform parent, int appendNumbers, Action<T> onEveryObjectCreated = null)
    {
        this.hidePosition = hidePosition;
        this.prefab = prefab;
        this.parent = parent;
        this.appendNumbers = appendNumbers;
        this.index = 0;
        if (onEveryObjectCreated != null) this.onEveryObjectCreated = onEveryObjectCreated;
        list = new List<T>();
    }

    public Action<T> onEveryObjectCreated;
    private List<T> list;
    private Vector3 hidePosition;
    private T prefab;
    private int index = 0;
    public int appendNumbers = 1;
    private Transform parent;

    public T GetObject()
    {
        if (list[index].gameObject.activeInHierarchy)
        {
            index = list.Count;
            CreateObjects(appendNumbers);
        }
        T result = list[index];
        index = (index + 1) % list.Count;
        return result;
    }

    public void RemoveObject(T obj)
    {
        if (list.Contains(obj)) list.Remove(obj);
    }

    public void CreateObjects(int amount)
    {
        if (parent == null)
            for (int i = 0; i < amount; i++)
            {
                T obj = GameObject.Instantiate(prefab, hidePosition, Quaternion.identity);
                obj.gameObject.SetActive(false);
                list.Add(obj);
                if (onEveryObjectCreated != null) onEveryObjectCreated(obj);
            }
        else
            for (int i = 0; i < amount; i++)
            {
                T obj = GameObject.Instantiate(prefab, parent);
                obj.transform.localPosition = hidePosition;
                obj.gameObject.SetActive(false);
                list.Add(obj);
                if (onEveryObjectCreated != null) onEveryObjectCreated(obj);
            }
    }

    public IEnumerable GetActiveObjects()
    {
        foreach (T obj in list)
            if (obj.gameObject.activeInHierarchy) yield return obj;
    }

    public IEnumerable GetInActiveObjects()
    {
        foreach (T obj in list)
            if (!obj.gameObject.activeInHierarchy) yield return obj;
    }

    public IEnumerable GetAllObjects()
    {
        foreach (T obj in list)
            yield return obj;
    }
}
