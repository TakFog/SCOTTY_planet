using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBody : MonoBehaviour {

    [SerializeField] public int damage;
    [SerializeField] public int defense;

    private Sprite bodySprite;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateSprite(string s)
    {
        bodySprite = Resources.Load(s, typeof(Sprite)) as Sprite;
        GetComponent<SpriteRenderer>().sprite = bodySprite;
    }
}
