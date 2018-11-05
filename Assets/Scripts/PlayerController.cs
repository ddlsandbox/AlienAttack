using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float aceleration;
    public Vector2 maxSpeed;
    public Vector2 rangeX;
    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform bulletSpawn;
    public float fireRate;

    private float nextFire;
    public Slider powerSlider;

    PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
		playerHealth = GetComponent <PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate(){
        Vector2 force = new Vector2();
        Rigidbody2D body = this.gameObject.GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = body.velocity;
        Vector2 newPosition = body.position;

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            fire(0);
        } else if (Input.GetButton("Fire2") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            fire(1);
        }

        if ((Mathf.Abs(currentVelocity.x) < maxSpeed.x) ||  //Si la velocidad no paso el limite
            (currentVelocity.x * Input.GetAxis("Horizontal") < 0.0f) ){ // O se está intentando mover en dirección contraria
            force.x = Input.GetAxis("Horizontal") * aceleration; //Aplico la fuerza
            force.y = 0;
        }

        newPosition += force;
        newPosition.x = Mathf.Clamp(newPosition.x, rangeX.x, rangeX.y);
        
        body.MovePosition(newPosition); //Aplico cambios

        powerSlider.value += 0.2f;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.gameObject.CompareTag("Lava")){
            this.transform.GetChild(2).GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0.0f;
        }
    }

    public void restartGame(){
        Time.timeScale = 1.0f;
        this.transform.GetChild(2).GetComponent<Canvas>().enabled = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void fire(int projectileType) {
        if (projectileType == 0 && powerSlider.value >= 10)
        {
            powerSlider.value -= 10;
            Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);
        }
        else if (projectileType == 1 && powerSlider.value >= 50)
        {
            powerSlider.value -= 50;
            for (int i=0; i<3; i++)
            {
                Instantiate(
                    rocketPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
		if (other.tag == "EnemyBullet")
		{
        	Destroy(other.gameObject);
        	playerHealth.TakeDamage(10);
		}
    }
}
