using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    [Tooltip("‘O’ñğŒ")]public Dictionary<string, bool> Preconditions { get; protected set; }

    [Tooltip("Œø‰Ê")]public Dictionary<string, bool> Effects { get; protected set; }

    public float cost = 1f;

    public abstract bool CheckCanExecute();
    public abstract bool ExecuteAction();
}
