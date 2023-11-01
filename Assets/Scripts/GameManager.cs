using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public ItemManager ItemManager;
    public BlockManager BlockManager;

    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }
            return _instance;
        }
    }

}
