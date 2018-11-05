using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

public Vector2 rangeX;
public GameObject explosion;
public float x_speed;
private int x_direction;

private int moveSteps;
private int moveCount;

public GameObject bulletPrefab;

private float nextFire;

	// Use this for initialization
	void Start () {
		x_direction = 1;
		moveSteps = (int)(44/x_speed);
		moveCount = moveSteps;
		updateFire();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveCount--;
		Rigidbody2D body = this.gameObject.GetComponent<Rigidbody2D>();
		Vector2 newPosition = body.position;
		Vector2 force = new Vector2();
		force.y = 0;
		force.x = x_speed * x_direction;
		newPosition += force;
		newPosition.x = Mathf.Clamp(newPosition.x, rangeX.x, rangeX.y);
		if (moveCount == 0)
		{
			x_speed *= 1.2f;
			moveSteps = (int)(44/x_speed);

			moveCount = moveSteps;
			//turn
			x_direction *= -1;
			newPosition.y -= 2;
		}

		if (Time.time > nextFire)
		{
			Fire();
		}

		body.MovePosition(newPosition);
	}

	void OnTriggerEnter2D(Collider2D other) 
    {
		if (other.tag == "Bullet")
		{
			Rigidbody2D body = this.gameObject.GetComponent<Rigidbody2D>();
			Instantiate (explosion, body.position, Quaternion.identity);
        	Destroy(other.gameObject);
        	Destroy(gameObject);
		}
    }

 	private void Fire() {
		updateFire();
        var bullet = Instantiate(
            bulletPrefab,
            this.transform.position,
            this.transform.rotation);
		bullet.transform.Rotate(Vector3.back * 30f + Vector3.forward * 60f*Random.value);
	}

	private void updateFire()
	{
		nextFire = Time.time + 1 + Random.value * 5;
	}
}
