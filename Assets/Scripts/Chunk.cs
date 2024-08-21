using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private static Chunk Instance = null;
    public static Chunk instance => Instance;

    public GameObject chunkPrefab = null;

    private List<GameObject> chunks = new List<GameObject>();

    private GameObject currentChunk = null;
    private GameObject pastChunk = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentChunk = chunkPrefab;
        currentChunk.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void LeavingCurrentChunk(GameObject chunk, string direction)
    {
        if (chunk == currentChunk)
        {
            switch (direction)
            {
                case "Left":
                    Debug.Log("Left");
                    SpawnChunk("Left");
                    break;
                case "Right":
                    Debug.Log("Right");
                    SpawnChunk("Right");
                    break;
                case "Top":
                    Debug.Log("Top");
                    SpawnChunk("Top");
                    break;
                case "Bottom":
                    Debug.Log("Bottom");
                    SpawnChunk("Bottom");
                    break;
            }
        }
        else
        {
            Debug.Log("Not moving from the current chunk");
        }
    }

    private void SpawnChunk(string direction)
    {
        Vector3 newDirection = Vector3.zero;
        switch (direction)
        {
            case "Left":
                newDirection = Vector3.left;
                break;
            case "Right":
                newDirection = Vector3.right;
                break;
            case "Top":
                newDirection = Vector3.up;
                break;
            case "Bottom":
                newDirection = Vector3.down;
                break;
        }

        newDirection *= 2.5f;
        GameObject newChunk = Instantiate(chunkPrefab, currentChunk.transform.position + newDirection, Quaternion.identity);

        // Find the bound in the right direction
        // Deactive the trigger for the entry side
        switch (direction)
        {
            case "Left":
                newChunk.GetComponent<Block>().Bounds[1].SetActive(false);
                break;
            case "Right":
                newChunk.GetComponent<Block>().Bounds[0].SetActive(false);
                break;
            case "Top":
                newChunk.GetComponent<Block>().Bounds[3].SetActive(false);
                break;
            case "Bottom":
                newChunk.GetComponent<Block>().Bounds[2].SetActive(false);
                break;
        }


        if (pastChunk != null)
            pastChunk.SetActive(false);

        pastChunk = currentChunk;
        pastChunk.GetComponent<SpriteRenderer>().color = Color.red;

        currentChunk = newChunk;
        currentChunk.GetComponent<SpriteRenderer>().color = Color.green;

        currentChunk.SetActive(true);
    }
}
