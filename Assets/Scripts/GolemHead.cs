using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHead : MonoBehaviour {

    [SerializeField] public int damage;
    [SerializeField] public int defense;

    private Sprite headSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateSprite(string s)
    {
        headSprite = Resources.Load(s, typeof(Sprite)) as Sprite;
        GetComponent<SpriteRenderer>().sprite = headSprite;
    }
}
