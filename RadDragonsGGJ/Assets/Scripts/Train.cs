using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Train : MonoBehaviour
{
    private static Train instance;

    public static Train Instance
    {
        get { return instance;  }
    }


    public List<PlayerController> players;
    public List<Interactable> interactables;
    public List<TrainPart> trainParts;

    private int numPlayers = 1;
    private bool frozen = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numPlayers = players.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (frozen)
            {
                UnFreezeTime();
            }
            else
            {
                FreezeTime();
            }
        }
    }

    private void FreezeTime()
    {
        foreach (var player in players)
        {
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<PlayerInteraction>().inputEnabled = false;
        }

        frozen = true;
    }

    private void UnFreezeTime()
    {
        foreach (var player in players)
        {
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponent<PlayerInteraction>().inputEnabled = true;
        }

        frozen = false;
    }
}
