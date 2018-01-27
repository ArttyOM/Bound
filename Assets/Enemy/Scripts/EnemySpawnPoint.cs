using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {
	public bool isInVision = false;

	public float T = 3f;
	public float[] chancePerType;
	public GameObject[] enemyTypes;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemy(T));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private GameObject _enemy;
	IEnumerator SpawnEnemy (float T){ //передаются все типы врагов и лист вероятностей спавна каждого, а также периодичность спавна
		
		float[] _chancePerTypeS = chancePerType;
		for (int i = 1; i < chancePerType.Length; i++) {
			_chancePerTypeS [i] +=  _chancePerTypeS[i-1];
		}
		do{
			float temp = Random.Range(0,_chancePerTypeS[_chancePerTypeS.Length-1]);
			for (int i=0; i<_chancePerTypeS.Length; i++){
				if (temp<_chancePerTypeS[i])
				{
					_enemy= enemyTypes[i];
					break;
				}
			}
			//Debug.Log(_chancePerTypeS[0]+" "+_chancePerTypeS[1]+" "+_chancePerTypeS[2]);
			//_enemy = null;
			//Debug.Log (temp);
			Debug.Log(temp +" " + _enemy.name);
			Instantiate (_enemy, new Vector3(0,0,0),Quaternion.identity, this.transform);
			yield return new WaitForSeconds(T);
		}
		while (!isInVision);
		Destroy (gameObject);
	}

}
