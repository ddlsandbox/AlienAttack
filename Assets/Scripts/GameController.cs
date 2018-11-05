using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        //while (true)
        //{
            float posy = spawnValues.y;
            for (int j = 0; j < 3; j++)
            {
                float posx = spawnValues.x;
                for (int i = 0; i < 10; i++)
                {
                    Vector3 spawnPosition = new Vector3(posx, posy, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    var newHazard = Instantiate (hazard, spawnPosition, hazard.transform.rotation);// spawnRotation);
                    posx += 4;
                    //yield return new WaitForSeconds (spawnWait);
                }
                posy -= 4;
            }
        //    yield return new WaitForSeconds (waveWait);
        //}
    }

	// Update is called once per frame
	void Update () {
		
	}
}
