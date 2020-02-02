using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField] private Canvas uiCanvas;

    public List<PlayerController> players;
    public List<Interactable> interactables;
    public List<TrainPart> trainParts;

    [SerializeField] private int maxBrokenParts = 4;

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
        CheckLose();

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
        else
        {
            Debug.Log("WIN!");
            var screen = uiCanvas.transform.Find("WinScreen").gameObject;
            screen.SetActive(true);
            var button = screen.transform.Find("Menu");
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
        }
    }

    private void ProcessPhase(Phase phase)
    {
        phaseProgress++;
        Debug.Log($"Processing phase {phaseProgress}");
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

    public void CheckLose()
    {
        if (players[0].dead &&
            players[1].dead &&
            players[2].dead &&
            players[3].dead)
        {
            Debug.Log("LOSE");
            var screen = uiCanvas.transform.Find("LoseScreen").gameObject;
            screen.SetActive(true);
            var button = screen.transform.Find("Menu");
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }

            return;
            
        }

        int brokenparts = 0;
        foreach (var part in trainParts)
        {
            if (part.currentState == PartState.BROKEN)
            {
                brokenparts++;
            }
        }

        if (brokenparts >= maxBrokenParts)
        {
            Debug.Log("LOSE");
            var screen = uiCanvas.transform.Find("LoseScreen").gameObject;
            screen.SetActive(true);
            var button = screen.transform.Find("Menu");
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
            //Show Lose Screens
        }
    }
}
