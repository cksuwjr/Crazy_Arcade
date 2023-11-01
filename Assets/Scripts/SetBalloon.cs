using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetBalloon : MonoBehaviour
{
    List<WaterBalloon> waterBalloonList;
    Status _status;
    [SerializeField] GameObject WaterBalloon;
    [SerializeField] AnimatedTile WaterBalloonTile;
    private void Awake()
    {
        waterBalloonList = new List<WaterBalloon> ();
        _status = GetComponent<Status> ();
    }
    public void InstallBalloon()
    {
        if (waterBalloonList.Count >= _status.BalloonCount) return;
        if (!GameManager.instance.BlockManager.CanInstallThere(transform.position)) return;


        GameManager.instance.BlockManager.SetObject(WaterBalloonTile, transform.position);
        Debug.Log("¹°Ç³¼± ¼³Ä¡");
        //GameObject InstantiatedWaterBalloon = Instantiate(WaterBalloon, GameManager.instance.BlockManager.GetVector3IntPosition(transform.position + new Vector3(0,0,0)), Quaternion.identity);
        //waterBalloonList.Add(InstantiatedWaterBalloon.GetComponent<WaterBalloon>());
        //InstantiatedWaterBalloon.GetComponent<WaterBalloon>().Installed(GetComponent<Player>());
    }
    public void RemoveBalloon(WaterBalloon balloon)
    {
        waterBalloonList.Remove(balloon);
        balloon.Remove();
    }
}
