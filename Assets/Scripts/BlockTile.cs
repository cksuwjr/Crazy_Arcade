using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New BreakableBlockTile", menuName = "New Tile")]
public class BlockTile : Tile
{
    public bool breakable;
}
