using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Train : MonoBehaviour
{
    struct Phase
    {
        public uint numPartsBreaking;
        //public uint numEvents; //TODO implement events first
        public float duration;

        public Phase(uint parts, float dur)
        {
            numPartsBreaking = parts;
            duration = dur;
        }
    }


    private static Train instance;

    public static Train Instance
    {
        get { return instance;  }
    }


    public List<PlayerController> players;
    public List<Interactable> interactables;
    public List<TrainPart> trainParts;

    private Queue<Phase> phases = new Queue<Phase>();
    private IEnumerator processPhase;
    private int phaseProgress = 0;

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

        phases.Enqueue(new Phase(1, 3));
        phases.Enqueue(new Phase(1, 4));
        phases.Enqueue(new Phase(1, 5));
        phases.Enqueue(new Phase(1, 6));


        InitiatePhases();
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

    private void InitiatePhases()
    {
        if (phases.Count == 0)
        {
            Debug.LogError("There are no phases in the queue on start up");
        }

        ProcessPhase(phases.Dequeue());
    }

    private IEnumerator WaitForNextPhase(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (phases.Count != 0)
        {
            ProcessPhase(phases.Dequeue());
        }
    }

    private void ProcessPhase(Phase phase)
    {
        Debug.Log($"Processing phase. Duration: {phase.duration}");
        for (int i = 0; i < phase.numPartsBreaking; ++i)
        {
            int randomIndex = Random.Range(0, trainParts.Count - 1);

            DamagePart(trainParts[randomIndex]);

        }

        StartCoroutine(WaitForNextPhase(phase.duration));
    }

    private void DamagePart(TrainPart trainPart)
    {
        switch (trainPart.currentState)
        {
            case PartState.GOOD:
                trainPart.SwitchState(PartState.WARMINGUP);
                break;
            case PartState.WARMINGUP:
                trainPart.SwitchState(PartState.BURNING);
                break;
            case PartState.BURNING:
                trainPart.SwitchState(PartState.BROKEN);
                break;
            default:
                Debug.Log("Part is broken already");
                break;
        }
    }
}
