using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase instance; //A static reference of the class

    [SerializeField] private int health = 0;
    [SerializeField] private int fullHeartNum = 10;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxHealthPowerUp = 100;

    public int GetFullHeartNum() { return fullHeartNum; }

    private void Awake()
    {
        //---Make sure there is only one instance of this class for each Scene.

        //If there is no instance of the object
        if (instance == null)
        {
            //Set an instance of it
            instance = this;
        }

        //Else, if there's already an instance
        else
        {
            //Destroy it
            Destroy(gameObject);
            return;
        }


        //This won't get destroy when you switch scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    { 
        //set the health to max health of the player
        health = maxHealth;

        //Display it in the UI
        UIManager.instance.GetHealthBar().UpdateHealthBar(health, maxHealthPowerUp);
    }

    public void FullHeartPowerUp(GameObject healthPowerUpGameObject)
    {
        if (health == maxHealthPowerUp)
        {
            return;
        }
        else
        {
            //Power up health
            health = health + GetFullHeartNum();

            //Display it in the UI
            UIManager.instance.GetHealthBar().UpdateHealthBar(health, maxHealthPowerUp);

            GameManager.instance.SetTolatPoints(GameManager.instance.GetTolatPoints() + GameManager.instance.GetHealthPowerUpPointNum());

            //destroy the object
            healthPowerUpGameObject.SetActive(false);
        }
    }

    public void TakeDmage(int damage)
    {
		//The player take the damage from the monsters.
		health -= damage;

        //Display it in the UI
        UIManager.instance.GetHealthBar().UpdateHealthBar(health, maxHealthPowerUp);

        //If the player dies
        if (health <= 0)
		{
			Die();
		}
	}

    public void Die()
    {
        GameManager.instance.SetPlayerHasDie(true);
        gameObject.SetActive(false);

        //Respawn the player
        Respawn();
    }

    private void Respawn()
    {

        GameManager.instance.SetPlayerHasDie(false);

        //Reset the health
        health = maxHealth;

        //Reset the health bar
        UIManager.instance.GetHealthBar().UpdateHealthBar(health, maxHealthPowerUp);

        gameObject.transform.position = GameManager.instance.GetLastCheckpointPos();

        gameObject.SetActive(true);

    }
}
