using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public Transform enemy;
    public GameObject[] spawnPoints;
    [SerializeField] private float SpawnRate = 5.0f;
    [SerializeField] AudioClip[] blahs;

    private float NextSpawn = 0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Random.Range(1,100) > 70 && Time.time > NextSpawn)
        {
            NextSpawn = Time.time + SpawnRate;
            int spawnRow = Random.Range(0, 4);
            Instantiate(enemy, spawnPoints[spawnRow].transform.position, Quaternion.identity);
            GetComponent<AudioSource>().clip = blahs[Random.Range(0, 11)];
            GetComponent<AudioSource>().PlayDelayed(0.7f);
        }
	}
}
