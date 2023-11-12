using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemInfo> items;
    [SerializeField] GameObject itemPrefab;

    public ItemInfo GetRandomItemInfo()
    {
        return items[Random.Range(0, items.Count)];
    }

    public GameObject SpawnRandomItem(Vector3 position)
    {
        var item = Instantiate(itemPrefab, position + new Vector3(0, 0.65f), Quaternion.identity);
        item.GetComponent<Item>().SetItemStatus(GetRandomItemInfo());
        item.transform.Find("Image").GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(position);
        item.transform.Find("Effect").GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(position)-1;
        item.transform.Find("Shadow").GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(position)-2;
        return item;

    }
    public GameObject SpawnItem(Vector3 position, ItemInfo info)
    {
        var item = Instantiate(itemPrefab, position + new Vector3(0, 0.65f), Quaternion.identity);
        item.GetComponent<Item>().SetItemStatus(info);
        item.transform.Find("Image").GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(position);
        item.transform.Find("Effect").GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(position)-1;
        item.transform.Find("Shadow").GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(position) - 2;
        return item;
    }
}
