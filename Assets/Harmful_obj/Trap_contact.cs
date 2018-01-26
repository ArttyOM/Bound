using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap_contact : MonoBehaviour {
	public int damage_for_player;
	public int damage_for_enemy;
	public Health_script health_ref;


	void OnTriggetEnter (Collider other){
		if (other.gameObject.tag == "player") {
			Trap_effect (other.gameObject);	
		}
		if (other.gameObject.tag == "enemy") {
			Trap_effect (other.gameObject);
		}
		if (other.gameObject.tag == "untagged") {
			Debug.Log ("Unindefened item in trap");
		}
	}

	void Trap_effect (GameObject someone) {
		if (someone.tag == "player") {
			Health_script health_ref = someone.GetComponent<Health_script> ();
			health_ref.HP -= damage_for_player;
		}
		if (someone.tag == "enemy") {
			Health_script health_ref = someone.GetComponent<Health_script> ();
			health_ref.HP -= damage_for_enemy;
		}
	}
}