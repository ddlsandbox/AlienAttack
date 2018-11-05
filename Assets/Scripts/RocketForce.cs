using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketForce : MonoBehaviour {

	private Rigidbody2D rb;
	public float Force;

	// Use this for initialization
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce(this.transform.up * Force);
		Force++;
	}
}
