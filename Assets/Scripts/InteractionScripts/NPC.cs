/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using Yarn.Unity;
/// attached to the non-player characters, and stores the name of the
/// Yarn node that should be run when you talk to them.
public class NPC : MonoBehaviour {

	public string characterName = "";

	[FormerlySerializedAs("startNode")]
	public string talkToNode = "";

	[Header("Optional")]
	public TextAsset scriptToLoad;

	// Use this for initialization
	void Start() {
		if (scriptToLoad != null) {
			FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
		}
	}

	float interactionRadius = 0.5f;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;

		// Flatten the sphere into a disk, which looks nicer in 2D games
		Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 1, 1));

		// Need to draw at position zero because we set position in the line above
		Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
	}

}
