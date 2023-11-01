using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    [SerializeField] Tilemap Ground;
    [SerializeField] List<Tile> groundTiles = new List<Tile>();

    [SerializeField] Tilemap Blocks;
    [SerializeField] List<Tile> blockTiles = new List<Tile>();
    [SerializeField] List<Tile> unbreakableTiles = new List<Tile>();

    private void Awake()
    {
        if(Ground == null)
            Ground = GameObject.Find("Grid").transform.Find("Ground").GetComponent<Tilemap>();
        if(Blocks == null)
            Blocks = GameObject.Find("Grid").transform.Find("Blocks").GetComponent<Tilemap>();

        AssignGround();
        AssignUnbreakable();
        AssignBlock();
    }

    void AssignGround()
    {
        if (groundTiles.Count == 0) return;

        int n = 0;
        for (int i = 0; i < 15; i++)
            for(int j = 0; j < 15; j++) { 
                Ground.SetTile(new Vector3Int(i, j), groundTiles[n]);
                if (++n == groundTiles.Count) n = 0;
            }
    }
    private void AssignUnbreakable()
    {
        if (unbreakableTiles.Count == 0) return;

        for (int i = 0; i < 15; i++)
            for (int j = 0; j < 15; j++)
            {
                int n = UnityEngine.Random.Range(0, 100);
                if(n < unbreakableTiles.Count)
                    Blocks.SetTile(new Vector3Int(i, j), unbreakableTiles[n]);
            }
    }

    void AssignBlock()
    {
        if (blockTiles.Count == 0) return;

        int n = 0;
        for (int i = 0; i < 15; i++)
            for (int j = 0; j < 15; j++)
            {
                if (Ground.GetTile(new Vector3Int(i, j)) == null) continue;
                if (Blocks.GetTile(new Vector3Int(i, j)) != null) continue;

                int c = UnityEngine.Random.Range(0, 101);

                if(c <= 50)
                    Blocks.SetTile(new Vector3Int(i, j), blockTiles[n]);


                if (++n == blockTiles.Count) n = 0;
            }
    }

    public bool CanInstallThere(Vector3 pos)
    {
        var newPosition = Blocks.WorldToCell(pos);

        if (Ground.GetTile(newPosition) == null) return false;
        if (Blocks.GetTile(newPosition) != null) return false;

        return true;
    }

    public void SetObject(AnimatedTile tileObject, Vector3 pos)
    {
        Blocks.SetTile(Blocks.WorldToCell(pos), tileObject);
    }
}
