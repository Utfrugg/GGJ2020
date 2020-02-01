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
        
    }
}
