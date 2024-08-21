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
    public GameObject startingBlock = null;

    private Dictionary<Vector3, GameObject> blocks = new Dictionary<Vector3, GameObject>();

    private GameObject currentBlock = null;
    private GameObject pastBlock = null;

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
        currentBlock = startingBlock;
        SetObjectColour(startingBlock, Color.green);
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
                    SpawnChunk("Left", currentBlock);
                    break;
                case "Right":
                    Debug.Log("Right");
                    SpawnChunk("Right", currentBlock);
                    break;
                case "Top":
                    Debug.Log("Top");
                    SpawnChunk("Top", currentBlock);
                    break;
                case "Bottom":
                    Debug.Log("Bottom");
                    SpawnChunk("Bottom", currentBlock);
                    break;
            }
        }
        else if (chunk == pastBlock)
        {
            switch (direction)
            {
                case "Left":
                    Debug.Log("Left");
                    SpawnChunk("Left", pastBlock);
                    break;
                case "Right":
                    Debug.Log("Right");
                    SpawnChunk("Right", pastBlock);
                    break;
                case "Top":
                    Debug.Log("Top");
                    SpawnChunk("Top", pastBlock);
                    break;
                case "Bottom":
                    Debug.Log("Bottom");
                    SpawnChunk("Bottom", pastBlock);
                    break;
            }
        }
        else
        {
            Debug.Log("Not moving from the current or past block");
        }
    }

    private void SpawnChunk(string direction, GameObject thisBlock)
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

        Vector3 newPos = thisBlock.transform.position + newDirection;

        GameObject newBlock;

        if (blocks.ContainsKey(newPos))
        {
            newBlock = blocks[newPos];
        }
        else
        {
            newBlock = Instantiate(blockPrefab, newPos, Quaternion.identity);
            blocks.Add(newPos, newBlock);
        }

        Block newBlockRef = newBlock.GetComponent<Block>();
        Block currentBlockRef = thisBlock.GetComponent<Block>();
        // Deactive the trigger for the entry side
        switch (direction)
        {
            case "Left":
                DeactivateBlockBound(newBlockRef, 1);
                DeactivateBlockBound(currentBlockRef, 0);
                break;
            case "Right":
                DeactivateBlockBound(newBlockRef, 0);
                DeactivateBlockBound(currentBlockRef, 1);
                break;
            case "Top":
                DeactivateBlockBound(newBlockRef, 3);
                DeactivateBlockBound(currentBlockRef, 2);
                break;
            case "Bottom":
                DeactivateBlockBound(newBlockRef, 2);
                DeactivateBlockBound(currentBlockRef, 3);
                break;
        }


        if (pastBlock != null && thisBlock != pastBlock)
            pastBlock.SetActive(false);

        pastBlock = thisBlock;
        SetObjectColour(pastBlock, Color.red);

        currentBlock = newBlock;
        SetObjectColour(currentBlock, Color.green);

        currentBlock.SetActive(true);
    }

    private void SetObjectColour(GameObject obj, Color color)
    {
        obj.GetComponent<SpriteRenderer>().color = color;
    }

    private void DeactivateBlockBound(Block block, int index)
    {
        block.Bounds[index].SetActive(false);
    }
}
