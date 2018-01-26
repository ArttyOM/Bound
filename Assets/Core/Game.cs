using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Level level;



    public void NewGame()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        level.GenerateNew();
    }


    private void Awake()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
    }

    // Use this for initialization
    void Start () {
        NewGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
