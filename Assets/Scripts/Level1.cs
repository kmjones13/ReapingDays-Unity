using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {
	public GameObject MyCharacter;
	// Use this for initialization
	void Start () {
		//DialogueTrigger CharacterScript = Character.GetComponent<DialogueTrigger>();
		//CharacterScript.TriggerDialogue ();
		 
		DialogueTrigger CharacterScript = MyCharacter.GetComponent<DialogueTrigger>();
		CharacterScript.TriggerDialogue (); 


	}
}