using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public enum BlockType { ACTIVE, PASSIVE };
    public string id;
    public BlockType blockType;
    public string description = "";
    public bool unlocked;
}
