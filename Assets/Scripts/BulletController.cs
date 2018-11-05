using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private Rigidbody2D rb;
	public float bulletSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.up * bulletSpeed;
	}

	void OnTriggerEnter2D(Collider2D other) 
    {
		if ((other.tag == "Bullet" || other.tag == "EnemyBullet") && tag != other.tag)
		{
			Destroy(gameObject);
		}
    }
}
