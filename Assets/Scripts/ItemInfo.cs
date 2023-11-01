using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemInfo : ScriptableObject
{
    public Sprite ItemSprite;
    public Sprite ItemEffectSprite;
    public float Speed = 0;
    public int BoomLength = 0;
    public int BalloonCount = 0;
}
