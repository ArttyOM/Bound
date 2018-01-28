using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    Level level;
    AI.GridScript grid;
    GameSettings config;
    Wizard wizard;
    Warrior warrior;
    bool Won = false;

    public int TotalTowers;
    public int DestroyedTowers;
    public List<Tower> Towers = new List<Tower>();
    public AudioClip TowerAudioClip;
    public AudioClip GameWinClip;
    public Image TheArrow;

    public List<Image> WarSliders;
    public List<Image> WizSliders;



    public void UpdateLabel()
    {
        Messenger.Broadcast(GameEvent.SCORE_INCREASED);
    }



    public void TowerBroken()
    {
        DestroyedTowers++;
        wizard.Health = 100.0f;
        warrior.Health = 100.0f;
        Messenger.Broadcast(GameEvent.MAGE_HEALTH_CHANGED);
        Messenger.Broadcast(GameEvent.WARRIOT_HEALTH_CHANGED);
        UpdateLabel();
        AudioSource.PlayClipAtPoint(TowerAudioClip, Camera.main.transform.position);
    }

    public void NewGame()
    {
        NextLevel();
        UpdateLabel();

    }


    void TeleportTo(Vector2 position)
    {
        wizard.transform.position = position - new Vector2(-1,0);
        warrior.transform.position = position + new Vector2(1, 0);
    }

    public Vector2 PlayerPos()
    {
        if (warrior != null && wizard != null)
            return (Vector2)(warrior.transform.position + wizard.transform.position) / 2;
        return Vector2.zero;
    }

    public void NextLevel()
    {
        //generate level
        level.GenerateNew();
        //update grid
        grid.transform.position = new Vector2(config.GenerationCell * config.LevelWidth / 2, config.GenerationCell * config.LevelHeight / 2);
        grid.gridSize = new Vector2(1.0f * config.GenerationCell * config.LevelWidth, 1.0f * config.GenerationCell * config.LevelHeight);
        grid.InitGrid();
        //place players
        TeleportTo(level.start);

    }


    private void Awake()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
        grid = GameObject.Find("Grid").GetComponent<AI.GridScript>();
        config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        warrior = GameObject.Find("Warrior").GetComponent<Warrior>();
        wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
        ServiceLocator.Instance.RegisterSingleton(this);
    }

    // Use this for initialization
    void Start () {
        NewGame();
	}

    void UpdateArrow()
    {
        var best = 1000.0f;
        Tower best_tower = null;
        var pl = PlayerPos();
        foreach (var tower in Towers)
        {
            if (tower.status == TowerStatus.Broken)
                continue;
            var d = Vector2.Distance(tower.towerpos, pl);
            if (d < best)
            {
                best = d;
                best_tower = tower;
            }
        }
        var angle = 0.0f;
        if (best_tower != null)
        {
            var v = best_tower.towerpos - (Vector2)pl;
            angle = Mathf.Atan2(v.y, v.x) * 180 / Mathf.PI; 
        }
        else
        {
            var v = level.finish - (Vector2)pl;
            angle = Mathf.Atan2(v.y, v.x) * 180 / Mathf.PI;
            if (!Won && v.magnitude < 2.0f)
            {
                Messenger.Broadcast(GameEvent.GAME_WON);
                AudioSource.PlayClipAtPoint(GameWinClip, Camera.main.transform.position);
                Won = true;
            }

        }
        TheArrow.transform.rotation = Quaternion.AngleAxis(angle-90, new Vector3(0, 0, 1));
    }


    void UpdateCooldowns()
    {
        for (int i = 0; i < WarSliders.Count; i++)
            WarSliders[i].fillAmount = 1 - warrior.Abilities[i].RemainingCooldown();
        for (int i = 0; i < WizSliders.Count; i++)
            WizSliders[i].fillAmount = 1 - wizard.Abilities[i].RemainingCooldown();

    }

    public void RestartGame()
    {
        warrior.IsDead = false;
        wizard.IsDead = false;
        warrior.Health = 100.0f;
        wizard.Health = 100.0f;
        if (CheckPointScript.lastVisited != null)
            TeleportTo(level.start);
        else
            TeleportTo(CheckPointScript.lastVisited.transform.position);

        StartCoroutine(UnrestartGame());
    }

    private IEnumerator UnrestartGame()
    {
        Character.Restarted = true;
        yield return new WaitForSeconds(0.2f);
        Character.Restarted = false;
    }

    // Update is called once per frame
    void Update () {
        UpdateArrow();
        UpdateCooldowns();
    }
}
