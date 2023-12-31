using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<int> OnMapIndexChange;
    public event Action<int> OnLoseMapIndexChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InvokeMapIndexChange(int mapIndex)
    {
        OnMapIndexChange?.Invoke(mapIndex);
    }
    public void InvokeMapIndexChangeLose(int mapIndex)
    {
        OnLoseMapIndexChange?.Invoke(mapIndex);
    }
}

