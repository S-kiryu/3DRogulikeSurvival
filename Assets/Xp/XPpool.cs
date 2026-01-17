using System.Collections;
using UnityEngine;

public class XPpool : MonoBehaviour
{
    public static XPpool Instance { get; private set; }

    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private int poolSize = 100;

    //private Queue<GameObject> xpPool = new Queue<GameObject>();


}
