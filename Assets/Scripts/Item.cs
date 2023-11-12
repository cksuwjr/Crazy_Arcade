using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Block
{
    SpriteRenderer ImageSR;
    SpriteRenderer EffectSR;
    private void Awake()
    {
        ImageSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        EffectSR = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
    public void SetItemStatus(ItemInfo info)
    {
        itemInfo = info;
        ImageSR.sprite = info.ItemSprite;
        EffectSR.sprite = info.ItemEffectSprite;
    }

    public override void BlockBreak()
    {
        if (!gameObject.activeSelf) return;
        Remove();
    }
    public override void Remove()
    {
        var block = GameManager.instance.BlockManager.GetBlockData(position);
        if (block != null)
            block.SetBlock(null);

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<Status>().Speed += itemInfo.Speed;
            collision.GetComponent<Status>().BoomLength += itemInfo.BoomLength;
            collision.GetComponent<Status>().BalloonCount += itemInfo.BalloonCount;
            Remove();
        }
    }
}
