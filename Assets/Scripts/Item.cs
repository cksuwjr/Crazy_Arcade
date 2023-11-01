using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    ItemInfo itemInfo;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<Status>().Speed += itemInfo.Speed;
            collision.GetComponent<Status>().BoomLength += itemInfo.BoomLength;
            collision.GetComponent<Status>().BalloonCount += itemInfo.BalloonCount;
            gameObject.SetActive(false);
        }
    }
}
