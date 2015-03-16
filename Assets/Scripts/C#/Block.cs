using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public enum BlockType { NONE, ACTIVE, PASSIVE };
    public string id;
    public BlockType blockType = BlockType.NONE;
    public string description = "";
    public bool unlocked;

}
