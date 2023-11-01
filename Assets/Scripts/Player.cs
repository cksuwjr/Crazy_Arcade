using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Move _move;
    Status _status;
    PlayerAnimation _playerAnim;
    SetBalloon _setBalloon;

    Vector3 direction;
    float dirX;
    float dirY;

    bool spacePressed = false;
    private void Awake()
    {
        _move = GetComponent<Move>();
        _status = GetComponent<Status>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _setBalloon = GetComponent<SetBalloon>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        if(!(dirX == 0 && dirY == 0))
            direction = new Vector3(dirX, dirY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
            spacePressed = true;
    }

    private void FixedUpdate()
    {
        if ((dirX == 0 && dirY == 0))
            _move.MoveTo(direction * Time.fixedDeltaTime, 0);
        else
            _move.MoveTo(direction * Time.fixedDeltaTime, _status.Speed);
        _playerAnim.SetAnimationWithVector(direction);

        if (spacePressed)
        {
            _setBalloon.InstallBalloon();
            spacePressed = !spacePressed;
        }
    }
}
