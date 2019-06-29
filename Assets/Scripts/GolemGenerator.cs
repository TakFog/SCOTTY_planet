using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemGenerator : MonoBehaviour
{

   // private static string[] headSpriteTable;

    public GolemController golem;
    public Transform spawnFX;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void GenerateGolem(CardMessage msg, Vector3 position)
    {
        Instantiate(spawnFX, new Vector3(position.x, position.y, position.z-0.5f), Quaternion.identity);
        GolemController created = Instantiate(golem, position, Quaternion.identity);
        
        Camera.main.GetComponent<CameraShake>().StartShake();

        created.head.GetComponent<GolemHead>().UpdateSprite(""+msg.headMessage.id);
        created.head.GetComponent<GolemHead>().damage = msg.headMessage.attack;
        created.head.GetComponent<GolemHead>().defense = msg.headMessage.defense;

        created.body.GetComponent<GolemBody>().UpdateSprite("" + msg.bodyMessage.id);
        created.body.GetComponent<GolemBody>().damage = msg.bodyMessage.attack;
        created.body.GetComponent<GolemBody>().defense = msg.bodyMessage.defense;

        created.feet.GetComponent<GolemFeet>().UpdateSprite("" + msg.legMessage.id);
        created.feet.GetComponent<GolemFeet>().damage = msg.legMessage.attack;
        created.feet.GetComponent<GolemFeet>().speed = msg.legMessage.speed;

        created.UpdateStats();
    }
}
