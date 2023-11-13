using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement pm;
    public int damage = 1;
    bool isReturning = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pm.decreaseHealth(damage);
        }
    }
    void returnLocation()
    {
        Invoke(nameof(normalLocation), 0.5f);
        gameObject.transform.DOLocalMoveX(gameObject.transform.localPosition.x + 2, 0.5f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isReturning == false)
        {
            gameObject.transform.DOLocalMoveX(gameObject.transform.localPosition.x - 2, 0.5f);
            isReturning = true;
            Invoke(nameof(returnLocation), 0.5f);
        }
       
        
    }
    void normalLocation()
    {
        isReturning = false;
    }
}
