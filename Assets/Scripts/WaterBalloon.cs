using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    Player Installer;
    bool Boomed = false;
    float time = 3f;
    public void Installed(Player installer)
    {
        Installer = installer;
        StartCoroutine(CountDown());
    }
    public void Boom()
    {
        Boomed = true;
        Installer.GetComponent<SetBalloon>().RemoveBalloon(this);
    }
    public void Remove()
    {
        gameObject.SetActive(false);
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
}
