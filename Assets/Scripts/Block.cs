using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Block : MonoBehaviour
{
    public bool breakable = true;
    internal ItemInfo itemInfo;
    internal Vector3Int position;
    public void SetInnerItem(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
    }
    public void SetPosition(Vector3Int position)
    {
        this.position = position;
    }
    public virtual void BlockBreak()
    {
        if (!gameObject.activeSelf) return;
        if (!breakable) return;
        Remove();
        if (itemInfo)
        {
            var item = GameManager.instance.ItemManager.SpawnItem(transform.position, itemInfo);
            var block = GameManager.instance.BlockManager.GetBlockData(transform.position);
            if (block != null)
            {
                block.SetBlock(item.GetComponent<Item>());
                item.GetComponent<Item>().SetPosition(position);
            }
        }
    }
    public virtual void Remove()
    {
        var block = GameManager.instance.BlockManager.GetBlockData(transform.position);
        if(block != null)
            block.SetBlock(null);

        gameObject.SetActive(false);
    }
}
