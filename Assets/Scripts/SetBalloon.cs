using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SetBalloon : MonoBehaviour
{
    List<WaterBalloon> waterBalloonList;
    Status _status;
    [SerializeField] GameObject WaterBalloon;

    GameObject mapBalloons;
    private void Awake()
    {
        mapBalloons = new GameObject();
        mapBalloons.name = "mapBalloons";

        waterBalloonList = new List<WaterBalloon>();
        _status = GetComponent<Status>();
    }
    public void InstallBalloon()
    {
        if (waterBalloonList.Count >= _status.BalloonCount) return;
        
        var setPos = GameManager.instance.BlockManager.GetObjectSetPosition(transform.position, 0, -0.25f);

        var blockData = GameManager.instance.BlockManager.GetBlockData(setPos);
        if(blockData != null)
        {
            if (blockData.GetBlock() == null)
            {
                var balloon = Instantiate(WaterBalloon, setPos, Quaternion.identity, mapBalloons.transform);
                blockData.SetBlock(balloon.GetComponent<WaterBalloon>());
                balloon.GetComponent<WaterBalloon>().Installed(GetComponent<Player>(), _status.BoomLength);
                balloon.GetComponent<Renderer>().sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(setPos);

                waterBalloonList.Add(balloon.GetComponent<WaterBalloon>());
                //Debug.Log("¼³Ä¡" + blockData.GetPosition() + "/" + setPos);
            }
        }
    }
    public void RemoveBalloon(WaterBalloon balloon)
    {
        waterBalloonList.Remove(balloon);
    }
}
