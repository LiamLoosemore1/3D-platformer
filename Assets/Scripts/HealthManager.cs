using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnlength;

    public GameObject deathEffect;
    public Image BlackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;


	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;

        //thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength; 
            }

            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(BlackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (BlackScreen.color.a == 1f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void HurtPlayer(int Damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {


            currentHealth -= Damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {

                thePlayer.Knockback(direction);

                invincibilityCounter = invincibilityLength;

                playerRenderer.enabled = false;

                flashCounter = flashLength;
            }
        }
    }

    public void Respawn()
    {
        //thePlayer.transform.position = respawnPoint;
        //currentHealth = maxHealth;
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnlength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;

        playerRenderer.enabled = false;

        flashCounter = flashLength;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
