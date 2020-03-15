using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class sheepController : MonoBehaviour
{
    [SerializeField] Transform targetLocation = default;
    [SerializeField] Transform jumpPosition = default;
    [SerializeField] Transform sheepSpawnPosition = default;
    [SerializeField] Transform killPoint = default;
    [SerializeField] CountingSheepScreenController miniGameCtrl = default;

    [Header("Sheep stats")]
    [SerializeField] float timeToSpawn = 2f;
    [SerializeField] float timeToEnd = 5f;
    [SerializeField] float timeToJump = 1f;

    [SerializeField] Transform scoreFeedback;

    float orgSpawn;
    float orgEnd;
    float orgJump;
    
    
    bool jumped = false;
    
    void Start()
    {
         timeToEnd = miniGameCtrl.timeToReachEnd;
         timeToSpawn = miniGameCtrl.timeToSpawn;
         timeToJump = miniGameCtrl.timeToJump;

         orgEnd = miniGameCtrl.timeToReachEnd;
         orgSpawn = miniGameCtrl.timeToSpawn;
         orgJump = miniGameCtrl.timeToJump;
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
        
        //text feedback
        scoreFeedback.GetComponent<TextMeshProUGUI>().text = "+1";
        if(miniGameCtrl.playerScore > 0)
            scoreFeedback.transform.DOPunchScale ( Vector3.one, 0.5f , 3, 1f);
        
        //reset ship for spawn animation
        transform.position = sheepSpawnPosition.position;
        
        transform.localScale = new Vector3 ( 0f, 0f, 0f );
        
        //calculate new timings
        timeToEnd = timeToEnd * 0.9f;
        if(timeToEnd <= 2f)
            timeToEnd = 2f;

        timeToJump = timeToJump * 0.95f;
        if(timeToJump <= 0.75f)
            timeToJump = 0.75f;

        timeToSpawn = timeToSpawn * 0.95f;
        if(timeToSpawn <= 1)
            timeToSpawn = 1f;

        //reset conditions
        jumped = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        
        //start spawn animation + walk to end
        Sequence spawnSequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, timeToSpawn))
            .Append(transform.DOMove(targetLocation.position, timeToEnd))
            ;
    }

    public void resetSheep()
    {
        scoreFeedback.GetComponent<TextMeshProUGUI>().text = "Speed reset!";

        scoreFeedback.transform.DOPunchScale ( new Vector3 (2f, 2f, 2f), 0.75f , 4, 1f);

        timeToEnd = orgEnd;
        timeToSpawn = orgSpawn;
        timeToJump = orgJump;

        transform.position = sheepSpawnPosition.position;
        
        transform.localScale = new Vector3 ( 0f, 0f, 0f );
        
        jumped = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        
        Sequence spawnSequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, timeToSpawn))
            .Append(transform.DOMove(targetLocation.position, timeToEnd))
            ;

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("sheepFence"))
        {
            Debug.Log("Sheep hit fence");
            //tween kill -> death animation
            DOTween.Clear();
            
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

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
            DOTween.Clear();
            initSheep();
        }

        if (other.CompareTag("sheepKillPos"))
        {
            DOTween.Clear();
            resetSheep();
        }
    }


    private void OnBecameInvisible() {
        transform.localScale = new Vector3 ( 0f, 0f, 0f );
        //Debug.Log
        initSheep();
    }

    private void Update() {
        var anim = GetComponent<Animator>();
        anim.SetBool("jumped", jumped);
    }

}
