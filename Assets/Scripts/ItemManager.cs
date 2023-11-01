using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemInfo> items;

    public ItemInfo GetRandomItemInfo()
    {
        return items[Random.Range(0, items.Count)];
    }
}
