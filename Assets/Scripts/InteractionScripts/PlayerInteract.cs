using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerInteract : MonoBehaviour {


	/* Add this to the script responsible for player movement
	=>
	// Remove all player control when we're in dialogue
	if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
		return;
	}
	<=  */
	public float interactionRadius = 2.0f;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;

		// Flatten the sphere into a disk, which looks nicer in 2D games
		Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1,1,1));

		// Need to draw at position zero because we set position in the line above
		Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
	}
	
	// Update is called once per frame
	// TODO edit the key to be the confirm key
	void Update () {

		if (FindObjectOfType<DialogueRunner> ().isDialogueRunning == true) {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			return;
		} else {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			CheckForNearbyNPC ();
		}
	}

	/// Find all DialogueParticipants
	/** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
	public void CheckForNearbyNPC ()
	{
		var allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
		var target = allParticipants.Find (delegate (NPC p) {
			return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
				(p.transform.position - this.transform.position)// is in range?
					.magnitude <= interactionRadius;
		});
		if (target != null) {
			// Kick off the dialogue at this node.
			FindObjectOfType<DialogueRunner> ().StartDialogue (target.talkToNode);
		}
	}
}
