using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemController : MonoBehaviour {

    [SerializeField] private int damage;
    [SerializeField] private int defense;
    [SerializeField] private float speed;
    [SerializeField] private int HP = 1;
    [SerializeField] public float attackRate = 1.0f;
    [SerializeField] private float currentHP = 1;
    public Transform attackAnimation;
    [SerializeField] private float RestartWalking = 0.2f;
    private float NextMovement = 0f;

    public GameObject head;
    public GameObject body;
    public GameObject feet;


    private GameObject collidingObj = null;
    private float NextAttack = 0f;
	// Use this for initialization
	void Start () {
	}
	
    private void Die()
    {
        GameObject.Destroy(this.gameObject, 0);
    }

	// Update is called once per frame
	void Update () {
        if (currentHP <= 0 || transform.position.x -2 > Camera.main.orthographicSize * (Screen.width / (float)Screen.height))
            Die();


        GetComponentInChildren<Slider>().value = currentHP / HP;

        if (collidingObj == null && Time.time > NextMovement)
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }
        else if (Time.time > NextAttack && collidingObj.tag == "Enemy")
        {
            NextAttack = Time.time + attackRate;
            Instantiate(attackAnimation, new Vector3(collidingObj.transform.position.x, collidingObj.transform.position.y, collidingObj.transform.position.z-1f) , Quaternion.identity);
            collidingObj.GetComponent<EnemyController>().getDamage(damage);
        }
    }


    public void UpdateStats()
    {
        const float minATK = 2;
        const float maxATK = 27;
        const float minDEF = 2;
        const float maxDEF = 18;

        damage = head.GetComponent<GolemHead>().damage + body.GetComponent<GolemBody>().damage + feet.GetComponent<GolemFeet>().damage;
        defense = head.GetComponent<GolemHead>().defense + body.GetComponent<GolemBody>().defense;
        speed = feet.GetComponent<GolemFeet>().speed * 3;
        HP = Mathf.RoundToInt(defense * 2.5f);
        currentHP = HP;
        Debug.Log(currentHP);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collidingObj = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.tag == "Enemy")
        {
            collidingObj = null;
            NextMovement = Time.time + RestartWalking;
        }
    }

    public void Damage(int d)
    {
        currentHP -= d;
    }
}
