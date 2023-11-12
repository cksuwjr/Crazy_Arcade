using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Move _move;
    Status _status;
    PlayerAnimation _playerAnim;
    SetBalloon _setBalloon;
    Renderer _renderer;
    Renderer _shadowRenderer;

    Vector3 direction;

    bool spacePressed = false;

    KeyCode key = KeyCode.None;
    private void Awake()
    {
        _move = GetComponent<Move>();
        _status = GetComponent<Status>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _setBalloon = GetComponent<SetBalloon>();
        _renderer = GetComponent<Renderer>();
        _shadowRenderer = transform.Find("CharacterShadow").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow)) key = KeyCode.DownArrow;
        if (Input.GetKey(KeyCode.UpArrow)) key = KeyCode.UpArrow;
        if (Input.GetKey(KeyCode.LeftArrow)) key = KeyCode.LeftArrow;
        if (Input.GetKey(KeyCode.RightArrow)) key = KeyCode.RightArrow;

        if (Input.GetKeyDown(KeyCode.Space)) spacePressed = true;
    }

    private void FixedUpdate()
    {
        if (key == KeyCode.DownArrow) { direction.x = 0; direction.y = -1; }
        if (key == KeyCode.UpArrow) { direction.x = 0; direction.y = 1; }
        if (key == KeyCode.LeftArrow) { direction.x = -1; direction.y = 0; }
        if (key == KeyCode.RightArrow) { direction.x = 1; direction.y = 0; }


        _playerAnim.SetAnimationWithVector(direction);

        if (key == KeyCode.None)
            _move.MoveTo(direction * Time.fixedDeltaTime, 0);
        else
            _move.MoveTo(direction * Time.fixedDeltaTime, _status.Speed);

        if (GameManager.instance.BlockManager.GetBlockData(transform.position + (direction * 0.5f) - new Vector3(0,0.25f, 0))== null)
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        key = KeyCode.None;

        if (spacePressed)
        {
            _setBalloon.InstallBalloon();
            spacePressed = !spacePressed;
        }
        _renderer.sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(transform.position);
        _shadowRenderer.sortingOrder = GameManager.instance.BlockManager.GetOrderInLayer(transform.position) - 1;
    }
}
