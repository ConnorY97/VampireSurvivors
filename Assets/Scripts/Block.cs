using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<GameObject> Bounds = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < Bounds.Count; i++) { Bounds[i].SetActive(true); }
    }
}
