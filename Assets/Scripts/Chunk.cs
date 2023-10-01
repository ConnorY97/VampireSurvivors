using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Tilemap mMap;
    [SerializeField] public List<GameObject> mProps = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (mMap == null)
        {
            Debug.Log("Missing tile map on chunk");
            return;
        }
        if (mProps.Count == 0)
        {
            Debug.Log("Missing props on chunk");
            return;
        }
    }
}
