using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {
	//public bool isInVision = false;

	public float maxDistance = 3f;
	public float minDIstance = 1f;
	public GameObject transmission;



	public float T = 3f;
	public GameObject enemyContainer;
	public float[] chancePerType;
	public GameObject[] enemyTypes;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemy(T));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private float _distance;
	private GameObject _enemy;
	IEnumerator SpawnEnemy (float T){ //передаются все типы врагов и лист вероятностей спавна каждого, а также периодичность спавна

		float[] _chancePerTypeS = chancePerType;
		for (int i = 1; i < chancePerType.Length; i++) {
			_chancePerTypeS [i] +=  _chancePerTypeS[i-1];
		}
		do{
			_distance = Vector3.Distance(this.transform.position, transmission.transform.position);

			if (_distance > maxDistance){
				Debug.Log("too far to spawn a creep");
				yield return null;
			}
			else{
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
				//Debug.Log(temp +" " + _enemy.name);
				Instantiate (_enemy, this.transform.position, Quaternion.identity, enemyContainer.transform);
				yield return new WaitForSeconds(T);
			}
		}
		while (_distance> minDIstance);
		Destroy (gameObject);
	}

}
