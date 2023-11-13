using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mantar : MonoBehaviour
{
    public PlayerMovement pm;
    public int point = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerMovement>().collectCoin(point);
            Destroy(gameObject);
        }
    }
}
