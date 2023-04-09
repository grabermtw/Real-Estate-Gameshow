using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameshowManager : MonoBehaviour
{
    private DialogueSystem dialogueSystem;
    public List<BaseGame> games;
    private BaseGame currGame;
    private Queue<BaseGame> gameQueue;

    public static GameshowManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.Log("Only 1 GameshowManager can be loaded at once");
            return;
        }
        dialogueSystem = GetComponent<DialogueSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Opening();
        // shuffle order of games
        System.Random rand = new System.Random();
        gameQueue = new Queue<BaseGame>();
        foreach (BaseGame bg in games.OrderBy(a => rand.Next()))
        {
            gameQueue.Enqueue(bg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Opening()
    {
        List<Dialogue> testDialogue = new List<Dialogue>();
        testDialogue.Add(new Dialogue("Bestudo", "Welcome to the show! I'm your host, Bestudo!", dialogueSystem.wholeStageCam, AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "So you want to be a realtor?", dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "Well, you've got a lot of learning to do!", dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "So whattdya say? Wanna show your real estate smarts?", dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        testDialogue.Add(new Dialogue("Bestudo", "Let's play some games!", dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
        dialogueSystem.PlayDialogue(testDialogue, true, StartNextGame, true);
    }

    public void StartNextGame()
    {
        if (currGame != null)
        {
            currGame.gameObject.SetActive(false);
        }
        currGame = gameQueue.Dequeue();
        currGame.gameObject.SetActive(true);
        currGame.StartGame();
    }
}
