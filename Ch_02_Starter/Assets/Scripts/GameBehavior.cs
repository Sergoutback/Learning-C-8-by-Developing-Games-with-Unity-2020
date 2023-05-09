using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;


public class GameBehavior : MonoBehaviour, IManager
{
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    
    public Stack<string> lootStack = new Stack<string>();
    Queue<string> activePlayers = new Queue<string>();

    
    private string _state;
    
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        
        set
        {
            _itemsCollected = value;
            
            Debug.LogFormat("Items: {0}", _itemsCollected);
            
            if (_itemsCollected >= maxItems)
            {
                TestView("You’ve found all the items!", false, true);
                showWinScreen = true;
            }
            else
            {
                labelText = "Item found, only " +
                            (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    private int _playerHP = 3;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                TestView("You want another life with that?", true, false);
            }
            else
            {
                labelText = "Ouch... that’s got hurt.";
            }
        }
    }

    private void TestView(string labelText, bool showLossScreen, bool showWinScreen)
    {
        this.labelText = labelText;
        this.showLossScreen = showLossScreen;
        this.showWinScreen = showWinScreen;
        Time.timeScale = 0f;
    }
    
    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);

    }
    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        debug(_state);
        
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
        
        // Добавление элементов в конец очереди
        activePlayers.Enqueue("Harrison");
        activePlayers.Enqueue("Alex");
        activePlayers.Enqueue("Haley");
        LogWithDelegate(debug);
        
        GameObject player = GameObject.Find("Player");
        // 2
        PlayerBehavior playerBehavior = 
            player.GetComponent<PlayerBehavior>();
        // 3
        playerBehavior.playerJump += HandlePlayerJump;

    }
    
    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }
    
    public static void Print(string newText)
    {
        Debug.Log(newText);
    }
    
    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    
    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You’ve got a good chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
        var firstPlayer = activePlayers.Peek();
        //var firstPlayer = activePlayers.Dequeue();
    }


    
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25),
            "Player Health:" + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25),
            "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50,
            300, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                    Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);

            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                    Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }
            }
        }
    }
}