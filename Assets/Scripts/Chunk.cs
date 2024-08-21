using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private static Chunk Instance = null;
    public static Chunk instance => Instance;

    public GameObject blockPrefab = null;

    private Dictionary<Vector3, GameObject> blocks = new Dictionary<Vector3, GameObject>();

    private GameObject currentBlock = null;
    private GameObject pastBlock = null;

    private int currentBlockIndex = 0;

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
        currentBlock = blockPrefab;
        currentBlock.GetComponent<SpriteRenderer>().color = Color.green;
        blocks.Add(currentBlock.transform.position, currentBlock);
    }

    public void LeavingCurrentChunk(GameObject chunk, string direction)
    {
        if (chunk == currentBlock)
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

        Vector3 newPos = currentBlock.transform.position + newDirection;

        GameObject newChunk;

        if (blocks.ContainsKey(newPos))
        {
            newChunk = blocks[newPos];
        }
        else
        {
            newChunk = Instantiate(blockPrefab, newPos, Quaternion.identity);
            blocks.Add(newPos, newChunk);
        }

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


        if (pastBlock != null)
            pastBlock.SetActive(false);

        pastBlock = currentBlock;
        pastBlock.GetComponent<SpriteRenderer>().color = Color.red;

        currentBlock = newChunk;
        currentBlock.GetComponent<SpriteRenderer>().color = Color.green;

        currentBlock.SetActive(true);
    }
}
