using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float speed = 0.05f;
    int health = 10;
    int coinCount = 0;

    public GameObject player;
    Animator animator;

    bool tapToStart = false;
    bool isJumping = false;
    bool isDead = false;

    private void Start()
    {
        animator = player.GetComponent<Animator>();
        PlayerPrefs.SetInt("HealthValue", health); //Reset health to max when restarting the level
    }

    void FixedUpdate()
    {
        if (tapToStart == true && isDead == false) // Forward Movement
        {
            transform.Translate(Vector3.forward * speed); 
        }
    }

    private void Update()
    {
        if(isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.X) && tapToStart == false) // Press "X" to start the game
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", true);
                tapToStart = true;
            }

            if (Input.GetKeyDown(KeyCode.A)) // Left Movement
            {
                if (player.transform.localPosition.x > -2)
                {
                    player.transform.DOLocalMoveX(player.transform.localPosition.x - 2, 0.25f);
                }
            }
            if (Input.GetKeyDown(KeyCode.D)) // Right Movement
            {
                if (player.transform.localPosition.x < 2)
                {
                    player.transform.DOLocalMoveX(player.transform.localPosition.x + 2, 0.25f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && isJumping == false) // Jumping
            {
                isJumping = true;
                if (player.transform.localPosition.y < 2)
                {
                    player.transform.DOLocalMoveY(player.transform.localPosition.y + 2, 0.5f);
                    Invoke(nameof(endJump), 0.5f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) // Get hit by obstacles
    {
        if (other.gameObject.tag == "LevelFinish")
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
            tapToStart = false;
            Invoke(nameof(LoadNextLevel), 1);
        }
    }

    void endJump() // Get the character back on the ground
    {
        player.transform.DOLocalMoveY(player.transform.localPosition.y - 2, 0.5f);
        isJumping = false;
    }

    public void decreaseHealth(int damage) // Decrease health
    {
        animator.SetTrigger("Damage");
        health -= damage;
        PlayerPrefs.SetInt("HealthValue", health);
        print("Health: " + health);
        checkPlayerHealth();
    }

    void checkPlayerHealth() // Check if player died
    {
        if(health <= 0)
        {
            print("You Died");
            PlayerPrefs.DeleteKey("HealthValue");
            isDead = true;
            animator.SetBool("PlayerDied", true);
            Invoke(nameof(RestartLevel0), 1);
        }
    }

    public void collectCoin(int coinValue) // Collect coin
    {
        coinCount += coinValue;
        print("Puan: " + coinCount);
    }

    void RestartLevel0() // Restart First Level
    {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel() // Load Next Level
    {
        SceneManager.LoadScene(1);
    }
}
