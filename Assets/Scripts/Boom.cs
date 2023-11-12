using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boom : MonoBehaviour
{
    WaterBalloon parentBalloon;

    [SerializeField] Sprite start;
    [SerializeField] Sprite mid;
    [SerializeField] Sprite end;
    public void BoomStart(WaterBalloon balloon,  int length)
    {
        parentBalloon = balloon;
        parentBalloon.Remove();

        Wave(Vector3.zero, 1);
        Wave(Vector3.left, length);
        Wave(Vector3.right, length);
        Wave(Vector3.up, length);
        Wave(Vector3.down, length);
    }
    public void Wave(Vector3 direction, int length = 0)
    {
        int gil = 0;
        while (gil++ < length)
        {
            var pos = transform.position + direction * gil;

            var block = GameManager.instance.BlockManager.GetBlockData(pos);
            if (block != null)
            {
                if (block.GetBlock())
                {
                    var delete = false;
                    if (!block.GetBlock().gameObject.GetComponent<Item>()) delete = true;
                    block.GetBlock().BlockBreak();
                    if (delete)
                        return;
                }
            }
            else return;

            //if (CheckBlock(transform.position + direction * gil, new Vector2(0.8f, 0.8f))) break;

            var julgi = Instantiate(gameObject, transform.position + direction * gil, Quaternion.identity);

            var sr = julgi.GetComponent<SpriteRenderer>();
            sr.sprite = gil != length ? mid : end;
            sr.sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(julgi.transform.position);
            if (direction == Vector3.zero)
                sr.sprite = start;

            if (direction == Vector3.right)
                julgi.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (direction == Vector3.up)
                julgi.transform.rotation = Quaternion.Euler(0, 0, 90);
            if (direction == Vector3.left)
                julgi.transform.rotation = Quaternion.Euler(0, 0, 180);
            if (direction == Vector3.down)
                julgi.transform.rotation = Quaternion.Euler(0, 0, 270);

            //Debug.Log(direction +"/"+ gil);
            
        }
    }
    private void Start()
    {
        Destroy(gameObject, 0.13f);
    }
}
