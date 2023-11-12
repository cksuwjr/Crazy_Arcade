using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class WaterBalloon : Block
{
    Player Installer;
    int Length;

    bool Boomed = false;
    float time = 2f;
    [SerializeField] GameObject boomEffect;
    public void Installed(Player installer, int length)
    {
        GetComponent<Collider2D>().isTrigger = true;
        Installer = installer;
        Length = length;
        StartCoroutine("CountDown");
    }
    public override void BlockBreak()
    {
        BoomRequest();

        Remove();
    }
    public void BoomRequest()
    {
        if (!Boomed)
        {
            Boomed = true;
            Boom();
        }
    }
    public void Boom()
    {
        Installer.GetComponent<SetBalloon>().RemoveBalloon(this);
        Instantiate(boomEffect, transform.position, Quaternion.identity).GetComponent<Boom>().BoomStart(this, Length);
    }
    IEnumerator CountDown()
    {
        while (!Boomed)
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0)
                Boom();
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            GetComponent<Collider2D>().isTrigger = false;
    }
    
}
