using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFeet : MonoBehaviour {

    [SerializeField] public int damage;
    [SerializeField] public float speed;

    public RuntimeAnimatorController ciabatte;
    public RuntimeAnimatorController ruota;
    public RuntimeAnimatorController tacchi;

    private Sprite feetSprite;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateSprite(string s)
    {
        switch (s)
        {
            case "2":
                {
                    GetComponent<Animator>().runtimeAnimatorController = ruota;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
                break;
            case "17": GetComponent<Animator>().runtimeAnimatorController = tacchi; break;
            case "11": GetComponent<Animator>().runtimeAnimatorController = ciabatte; break;
            default:
            {
                GetComponent<Animator>().enabled = false;
                feetSprite = Resources.Load(s, typeof(Sprite)) as Sprite;
                GetComponent<SpriteRenderer>().sprite = feetSprite;
            }; break;
        }

    }
}
