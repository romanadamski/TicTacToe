using System;
using UnityEngine;

[Serializable]
public class SerializableTuple<T1, T2>
{
    [SerializeField]
    private T1 item1;
    public T1 Item1 => item1;
    [SerializeField]
    private T2 item2;
    public T2 Item2 => item2;
}
