using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public List<PlayerController> players;

    private int numPlayers = 1;

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
