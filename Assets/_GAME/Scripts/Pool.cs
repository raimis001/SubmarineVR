using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    static readonly List<GameObject> pool = new List<GameObject>();
    static readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    static Transform parent;

    private void Awake()
    {
        parent = transform;
        for (int i = 0; i < transform.childCount; i++) 
        { 
            GameObject g = transform.GetChild(i).gameObject;
            g.SetActive(false);
            prefabs.Add(g.name, g);
        }
    }

    public static GameObject GetPool(string name)
    {
        foreach (GameObject go in pool)
        {
            if (go.name != name)
                continue;
            if (go.activeInHierarchy)
                continue;

            go.SetActive(true);
            return go;
        }

        GameObject g = Instantiate(prefabs[name], parent);
        g.name = name;
        g.SetActive(true);
        pool.Add(g);
        return g;

    }
}
