using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameshowManager : MonoBehaviour
{
    private DialogueSystem dialogueSystem;
    public List<BaseGame> games;
    public Animator bestudoAnim;
    public GameObject menuButton;
    public GameObject finalResult;
    public TextMeshProUGUI resultText;
    public GameObject screensaver;
    private BaseGame currGame;
    private Queue<BaseGame> gameQueue;

    public int totalMoney;
    public TextMeshProUGUI moneyText;

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
        AddMoney(0);
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
        dialogueSystem.PlayDialogue(testDialogue, true, StartNextGame, false);
    }

    public void StartNextGame()
    {
        if (currGame != null)
        {
            currGame.gameObject.SetActive(false);
        }
        if (gameQueue.Count > 0)
        {
            currGame = gameQueue.Dequeue();
            currGame.gameObject.SetActive(true);
            currGame.StartGame();
        }
        else
        {
            EndingDialogue();
        }
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        moneyText.text = string.Format("Money: {0}", amount.ToString("C0"));
    }

    private void EndingDialogue()
    {
        finalResult.SetActive(true);
        screensaver.SetActive(false);
        resultText.SetText(string.Format("{0}", totalMoney.ToString("C0")));
        List<Dialogue> endDialogue = new List<Dialogue>();
        if (totalMoney > 0)
        {
            endDialogue.Add(new Dialogue("Bestudo", string.Format("Wow! You won {0}! That's a lot of money!", totalMoney.ToString("C0")), dialogueSystem.bestudoCam, AnimCategory.CorrectAnswer));
            endDialogue.Add(new Dialogue("Bestudo", string.Format("You'll be a great realtor someday!"), dialogueSystem.wholeStageCam, AnimCategory.CorrectAnswer));
        }
        else
        {
            endDialogue.Add(new Dialogue("Bestudo", string.Format("Wow... you won $0."), dialogueSystem.bestudoCam, AnimCategory.WrongAnswer));
            endDialogue.Add(new Dialogue("Bestudo", string.Format("You'll never be a realtor!"), dialogueSystem.wholeStageCam, AnimCategory.WrongAnswer));
        }
        dialogueSystem.PlayDialogue(endDialogue, false, EndingDance, false);
    }

    public void EndingDance()
    {
        if (totalMoney > 0)
        {
            bestudoAnim.SetInteger("RandomChoice", Random.Range(0, 5));
            bestudoAnim.SetTrigger("EndDance");
        }
        else
        {
            bestudoAnim.SetTrigger("EndLoser");
        }
        menuButton.SetActive(true);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
