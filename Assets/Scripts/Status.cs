using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private float _Hp = 1f;
    public float Hp { get { return _Hp; } set { _Hp = value; } }

    [SerializeField] private float _Speed = 1f;
    public float Speed { get { return _Speed; } set { _Speed = value; } }

    [SerializeField] private int _BoomLength = 1;
    public int BoomLength { get { return _BoomLength; }set { _BoomLength = value; } }

    [SerializeField] private int _BalloonCount = 1;
    public int BalloonCount { get {  return _BalloonCount; } set {  _BalloonCount = value; } }
}
