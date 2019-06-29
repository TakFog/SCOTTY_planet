using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    [SerializeField] private int totalHP = 1;
    [SerializeField] private int HP;
    [SerializeField] private int speed;
    [SerializeField] private float requestCooldown = 1.0f;

    CommunicationBehaviour communication;
    GameBehaviour behaviour;
    AudioSource cardLoot;
    Vector3[] spawnPoints;
    Animator anim;
    Camera cam;

    private float canRequest = 0f;
    private float spriteHeight;
    private float spriteWidth;
    private float whratio;

    // Use this for initialization
    void Start()
    {
        HP = totalHP;
        communication = GameObject.FindGameObjectWithTag("GameController").GetComponent<CommunicationBehaviour>();
        behaviour = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehaviour>();
        spawnPoints = new Vector3[GameObject.FindGameObjectsWithTag("SpawnPoint").Length];
        for (int i = 1; i < spawnPoints.Length + 1; i++)
        {
            spawnPoints[i - 1] = GameObject.Find("Row" + i).transform.position;
        }
        cardLoot = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();

        var pixperunit = gameObject.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        spriteHeight = gameObject.GetComponent<SpriteRenderer>().sprite.rect.height / pixperunit;
        spriteWidth = gameObject.GetComponent<SpriteRenderer>().sprite.rect.width / pixperunit;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        whratio = Screen.width / (float)Screen.height;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            DestroyObject(collision.gameObject);
            cardLoot.Play();
            communication.SendLootMsg();
        }
    }
    // Update is called once per frame
    void Update ()
    {
        GetComponentInChildren<Slider>().value = ((float) HP )/ ((float) totalHP);
        behaviour.Dehighlight();

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var xPos = gameObject.transform.position.x;
        var yPos = gameObject.transform.position.y;

        int linenmbr = this.FindLineNumber();

        if (Input.GetButton("Fire1"))
        {
            behaviour.HighlightLine(linenmbr);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (Time.time > canRequest)
            {
                canRequest = Time.time + requestCooldown;

                communication.SendGolemRequestMessage(new Vector3(gameObject.transform.position.x + 1, spawnPoints[linenmbr].y, 0));
                anim.SetTrigger("pressTrig");
            }
        }
        
        if (yPos + spriteHeight / 2 >= cam.orthographicSize && y > 0)
        {
            y = 0;
        }
        else if (yPos - spriteHeight / 2 <= - cam.orthographicSize && y < 0)
        {
            y = 0;
        }
        if (xPos + spriteWidth / 2 >= cam.orthographicSize * whratio && x > 0)
        {
            x = 0;
        }
        else if (xPos - spriteWidth / 2 <= - cam.orthographicSize * whratio && x < 0)
        {
            x = 0;
        }
        gameObject.transform.position += new Vector3(x, y, 0);
    }

    public void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            behaviour.GameOver();
        }
    }

    private int FindLineNumber()
    {
        var y = gameObject.transform.position.y;

        int minIndex = 0;
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            if (Mathf.Abs(y - spawnPoints[i].y) < Mathf.Abs(y - spawnPoints[minIndex].y))
            {
                minIndex = i;
            }
        }

        return minIndex;
    }
}
