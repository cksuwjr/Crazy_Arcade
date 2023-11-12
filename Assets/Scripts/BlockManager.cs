using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    [SerializeField] Tilemap Ground;

    [SerializeField] Tilemap Blocks;

    [SerializeField] GameObject blockPrefab;

    int minOrderinLayer;
    public Vector2 mapStartPoint;
    public Vector3 mapMax;
    public Vector3 mapMin;


    public Dictionary<Vector3Int, GroundData> GroundDatas;
    public class GroundData
    {
        TileBase blockTile;
        Block block;
        Vector3 position;
        public GroundData()
        {
            this.blockTile = null;
            this.block = null;
            this.position = Vector3.zero;
        }
        public GroundData(TileBase tile, Block block, Vector3 position)
        {
            this.blockTile = tile;
            this.block = block;
            this.position = position;
        }
        public void SetGroundData(TileBase tile, Block block, Vector3 position)
        {
            this.blockTile = tile;
            this.block = block;
            this.position = position;
        }
        public Block GetBlock()
        {
            return block;
        }
        public void SetBlock(Block block)
        {
            this.block = block;
        }
        public Vector3 GetPosition()
        {
            return position;
        }
    }
    private void Awake()
    {
        if(Ground == null) Ground = GameObject.Find("Grid").transform.Find("Ground").GetComponent<Tilemap>();
        if(Blocks == null) Blocks = GameObject.Find("Grid").transform.Find("Blocks").GetComponent<Tilemap>();

        GroundDatas = new Dictionary<Vector3Int, GroundData>();

        SetMapMaxMin();

        AssignGround();
        AssignBlock();
        Ground.GetComponent<Renderer>().sortingOrder = minOrderinLayer - 10;

    }

    private void SetMapMaxMin()
    {
        mapStartPoint = new Vector2(Ground.cellBounds.x, Ground.cellBounds.y);
        mapMin = new Vector2(Ground.cellBounds.xMin, Ground.cellBounds.yMin);
        mapMax = new Vector2(Ground.cellBounds.xMax, Ground.cellBounds.yMax); 
    }

    void AssignGround()
    {
        BoundsInt bounds = Ground.cellBounds;
        TileBase[] allTiles = Ground.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) continue;

                Vector3Int Point = Ground.WorldToCell(new Vector3(x + bounds.x, y + bounds.y));

                TileData tileData = new TileData();
                tile.GetTileData(Point, Blocks, ref tileData);

                GroundDatas.Add(Point, new GroundData());
            }
        }
    }

    void AssignBlock()
    {
        var mapBlocks = new GameObject();
        mapBlocks.name = "mapBlocks";

        BoundsInt bounds = Blocks.cellBounds;
        TileBase[] allTiles = Blocks.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {

                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) continue;

                Vector3Int Point = Blocks.WorldToCell(new Vector3(x + bounds.x, y + bounds.y));

                TileData tileData = new TileData();
                tile.GetTileData(Point, Blocks, ref tileData);

                GameObject block = Instantiate(blockPrefab, Point + Blocks.tileAnchor, Quaternion.identity, mapBlocks.transform);

                SpriteRenderer sr = block.GetComponent<SpriteRenderer>();
                sr.sprite = tileData.sprite;
                sr.sortingOrder = -Point.y;

                Block bc = block.AddComponent<Block>();
                bc.SetPosition(Point);
                bc.breakable = Blocks.GetTile<BlockTile>(Point).breakable;
                //Debug.Log(Blocks.GetTile<BlockTile>(Point).breakable);

                if (UnityEngine.Random.Range(0, 101) < 30f)
                    bc.SetInnerItem(GameManager.instance.ItemManager.GetRandomItemInfo());

                if(minOrderinLayer > sr.sortingOrder)
                    minOrderinLayer = sr.sortingOrder;

                if (GroundDatas.ContainsKey(Point))
                    GroundDatas[Point].SetGroundData(tile, bc, Point + Blocks.tileAnchor);
            }
        }
        Blocks.ClearAllTiles();
    }
    public Vector3 GetObjectSetPosition(Vector3 position, float xAdjust = 0, float yAdjust = 0)
    {
        return Blocks.WorldToCell(new Vector3(Mathf.RoundToInt(position.x + xAdjust - Blocks.tileAnchor.x), Mathf.RoundToInt(position.y + yAdjust - Blocks.tileAnchor.y))) + Blocks.tileAnchor;
    }
    public int GetOrderInLayer(Vector3 position)
    {
        return -Blocks.WorldToCell(position).y;
    }
    public GroundData GetBlockData(Vector3 position)
    {
        //Debug.Log(position);
        var pos = Blocks.WorldToCell(position);
        //Debug.Log(pos);
        if (GroundDatas.ContainsKey(pos))
            return GroundDatas[pos];

        return null;
    }

}
