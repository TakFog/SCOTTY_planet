using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChanibaL;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    [SerializeField] private int totalHP = 100;
    [SerializeField] private float halfChance;
    public int currHP;
    [SerializeField] public Transform fxSpark;
    private GameObject sparkGenerator;
    
    Slider manabar;
    GameBehaviour behaviour;
    CommunicationBehaviour networkManager;

	// Use this for initialization
	void Start ()
    {
        networkManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<CommunicationBehaviour>();
        sparkGenerator = GameObject.Find("SparkGenerator");
        currHP = totalHP;
        
        manabar = GameObject.Find("PortalManaBar").GetComponent<Slider>();
        manabar.value = 0;

        behaviour = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehaviour>();
	}

    // Update is called once per frame
    void Update ()
    {
        if (RandomGenerator.global.HalfChanceInTime(halfChance))
        {

            var newX = sparkGenerator.transform.position.x + RandomGenerator.global.GetFloatRange(0f, 0.6f);
            var newY = sparkGenerator.transform.position.y - RandomGenerator.global.GetFloatRange(0.5f, 9f);

            Instantiate(fxSpark, new Vector3(newX, newY, 0), Quaternion.identity);
        }
        
        manabar.value += Time.deltaTime / behaviour.GameTime;
        if (manabar.value >= 1)
        {
            manabar.value = 0;
            behaviour.WinGame();
        }
	}

    public void GetDamage(int damage)
    {
        currHP -= damage;

        if (currHP == 30 ||
            currHP == 20 ||
            currHP == 10 ||
            currHP == 0)
        {
            networkManager.SendDamageMessage();
        }

        if (this.currHP <= 0)
        {
            this.Die();
        }
    }

    private void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehaviour>().GameOver();
    }
}
