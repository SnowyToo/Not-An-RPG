using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapManager : MonoBehaviour
{
    public Dictionary<Vector3Int, List<Object>> obj;

    public void Awake()
    {
        obj = new Dictionary<Vector3Int, List<Object>>();
    }

    public List<Object> GetObjectsAtPosition(Vector3 pos)
    {
        return obj[GetGridPosition(pos)];
    }

    public void SetObjectAtPosition(Vector3 pos, Object o)
    {
        if (obj.ContainsKey(GetGridPosition(pos)))
        {
            obj[GetGridPosition(pos)].Add(o);
        }
        else
        {
            obj.Add(GetGridPosition(pos), new List<Object>() { o });
        }
    }

    public Object PopObjectAtPosition(Vector3 pos, Object o)
    {
        Object r = null;
        foreach (Object o1 in obj[GetGridPosition(pos)])
        {
            if (o1 == o)
            {
                r = o1;
                obj[GetGridPosition(pos)].Remove(o1);
            }
            break;
        }
        return r;
    }

    public Vector3Int GetGridPosition(Vector3 pos)
    {
        return new Vector3Int(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f), 0);
    }
}
