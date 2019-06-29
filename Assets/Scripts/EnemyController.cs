using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    [SerializeField] private int HP             = 5;
    [SerializeField] private int damage         = 5;
    [SerializeField] private int portalDamage   = 5;
    [SerializeField] private float speed        = 3;
    [SerializeField] private float AttackRate   = 1.0f;
    [SerializeField] private float RestartWalking = 0.2f;

    [SerializeField] private float currentHP;
    public Transform card;
    public Transform explosion;
    private int doubleDropChance = 30;
    private float NextAttack = 0f;
    private float NextMovement = 0f;
    private GameObject collidingObj = null;


	// Use this for initialization
	void Start ()
    {
        this.currentHP = HP;
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Card")
        {
            if (collision.transform.position.x < this.transform.position.x)
                collidingObj = collision.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Portal")
        {
            this.Explode(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collidingObj = null;
        NextMovement = Time.time + RestartWalking;
    }

    //Death and drop control
    private void Die()
    {
        int dropTimes = 1;
        if (Random.Range(1, 100) > 100 - doubleDropChance)
            dropTimes = 2;
        for (int i = 0; i<dropTimes; i++)
        {
            Instantiate(card, transform.position, Quaternion.identity);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject, 0);
        
    }

    public void Explode(GameObject collidingObj)
    {
        // If colliding object is a portal, then damage it
        if (collidingObj.gameObject.tag == "Portal")
        {
            collidingObj.gameObject.GetComponent<PortalController>().GetDamage(this.portalDamage);
        }
        else if (collidingObj.gameObject.tag == "Player")
        {
            collidingObj.gameObject.GetComponent<PlayerController>().Damage(damage);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraShake>().StartShake();
        GameObject.Destroy(this.gameObject, 0);
    }
    
    // Update is called once per frame
    void Update () {
        // Check if dead
        if (currentHP <= 0)
            Die();

        GetComponentInChildren<Slider>().value = currentHP / HP;
        // Enemy Behaviour
        if (collidingObj == null && Time.time > NextMovement)
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
        }
        else if (collidingObj != null && Time.time > NextAttack)
        {
            NextAttack = Time.time + AttackRate;
            /*
            switch (collidingObj.tag)
            {
                case "Player": Explode(); break;
                case "Golem": collidingObj.GetComponent<GolemController>().Damage(damage); break;
                case "Portal": Explode(); break;
            }
            */
            if (collidingObj.tag == "Golem")
            {
                collidingObj.GetComponent<GolemController>().Damage(damage);
            }
        }
	}

    public void getDamage(int d)
    {
        currentHP -= d;
    }
}
