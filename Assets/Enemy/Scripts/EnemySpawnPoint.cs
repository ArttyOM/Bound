using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {
	//public bool isInVision = false;

	public float maxDistance = 3f;
	public float minDIstance = 1f;

    public bool online = false;
    public bool dead = false;



    public float T = 3f;
	public static GameObject enemyContainer;
	public float[] chancePerType;
	public GameObject[] enemyTypes;

	// Use this for initialization
	void Awake(){
		if (enemyContainer == null) enemyContainer = GameObject.FindWithTag ("EnemyContainer");
	}

	void Start () {
		StartCoroutine (SpawnEnemy(T));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject RandomEnemy()
    {
        float[] _chancePerTypeS = new float[chancePerType.Length];
        for (int i = 0; i < chancePerType.Length; i++)
            _chancePerTypeS[i] = chancePerType[i];
        for (int i = 1; i < chancePerType.Length; i++)
            _chancePerTypeS[i] += _chancePerTypeS[i - 1];
        float temp = Random.Range(0, _chancePerTypeS[_chancePerTypeS.Length - 1]);
        for (int i = 0; i < _chancePerTypeS.Length; i++)
            if (temp < _chancePerTypeS[i])
                return enemyTypes[i];
        return enemyTypes[0];
    }


    IEnumerator SpawnEnemy (float T){ //передаются все типы врагов и лист вероятностей спавна каждого, а также периодичность спавна
		do{
			if (online){
                Instantiate(RandomEnemy(), this.transform.position, Quaternion.identity, enemyContainer.transform);
                yield return new WaitForSeconds(T);
            }
            else
            {
				yield return new WaitForSeconds(T);
			}
		}
		while (!dead);
		Destroy (gameObject);
	}

}
