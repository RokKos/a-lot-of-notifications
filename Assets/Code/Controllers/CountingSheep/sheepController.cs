using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class sheepController : MonoBehaviour
{
    [SerializeField] Transform targetLocation = default;
    [SerializeField] Transform jumpPosition = default;
    [SerializeField] Transform sheepSpawnPosition = default;
    [SerializeField] Transform killPoint = default;
    [SerializeField] CountingSheepScreenController miniGameCtrl = default;

    [Header("Sheep stats")]
    [SerializeField] float timeToEnd = 5f;
    [SerializeField] float timeToJump = 1f;
    
    
    bool jumped = false;
    
    void Start()
    {
         timeToEnd = miniGameCtrl.timeToReachEnd;
    }

    public void sheepJump()
    {
        
        if(!jumped)
        {
            DOTween.Clear();
            Vector3 jumpLocal = new Vector3 (jumpPosition.position.x, jumpPosition.position.y, transform.position.z);
            gameObject.GetComponent<Rigidbody2D>().DOJump(jumpLocal, 200f, 1, timeToJump);
            jumped = true;
        }
    }

    public void initSheep()
    {
        timeToEnd = timeToEnd * 0.9f;
        timeToJump = timeToJump * 0.95f;
        jumped = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;


        transform.position = sheepSpawnPosition.position;


        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetLocation.position);
        transform.DOMove(targetLocation.position, timeToEnd);
    }

    public void resetSheep()
    {
        timeToEnd = miniGameCtrl.timeToReachEnd;
        timeToJump = 1f;
        jumped = false;

        transform.position = sheepSpawnPosition.position;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetLocation.position);
        transform.DOMove(targetLocation.position, timeToEnd);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("sheepFence"))
        {
            Debug.Log("Sheep hit fence");
            //tween kill -> death animation
            DOTween.Clear();
            
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            transform.DOShakeScale(1f, 1, 10, 90);
            transform.DOShakeRotation(1f, 90f, 10, 90f);
            transform.DOMove(killPoint.position, 1f);  
                 
        }

        if (other.CompareTag("sheepWinPos"))
        {
            //move sheep back to start location
            //neka animacija z tweeningom?
            Debug.Log("Na win poziciji, ovco posiljamo nazaj");
            miniGameCtrl.addCounter();
            initSheep();
        }

        if(other.CompareTag("sheepKillPos"))
        {
            Debug.Log("camera shake and level reset");
            Camera.main.DOShakeRotation(0.2f, 25f, 2, 25f, true);
        }
    }

}
