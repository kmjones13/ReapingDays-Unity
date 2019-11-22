using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public static DialogueManager Instance {
		get;
		set;
	}

	public Text NameText;
	public Text DialogueText;

	public Animator myAnimator;

	public Queue<string> Sentences;


	void Awake(){
		if (Instance != null && Instance != this) {
			Destroy (gameObject);	
		} else {
			Instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		Sentences = new Queue<string> ();
	}
	
	public void StartDialogue ( Dialogue dialogue){
		Debug.Log ("Coversation started with" + dialogue.Name);

		myAnimator.SetBool ("IsOpen", true);

		NameText.text = dialogue.Name;
		Sentences.Clear ();

		foreach (string sentence in dialogue.Sentences) {
			Sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence (){
		if ( Sentences.Count == 0){
			EndDialogue();
			return;
		}

		string sentence = Sentences.Dequeue ();
		StopAllCoroutines();
		StartCoroutine (TypeSentence (sentence));
		//DialogueText.text = sentence;
	}

	IEnumerator TypeSentence ( string sentence)
	{
		DialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			DialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue(){
		Debug.Log ("END");
		myAnimator.SetBool ("IsOpen", false);
	}
}
