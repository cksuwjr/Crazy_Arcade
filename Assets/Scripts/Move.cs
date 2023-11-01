using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] private float SpeedFerItem = 40f; 
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void MoveTo(Vector3 dir, float speed)
    {
        _rigidbody.velocity = dir * (speed * SpeedFerItem);
    }
}
