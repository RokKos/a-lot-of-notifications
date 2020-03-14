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
    
    float timeToEnd = 5f;
    bool jumped = false;
    
    void Start()
    {

    }

    public void sheepJump()
    {
        
        if(!jumped)
        {
            DOTween.Clear();
            Vector3 jumpLocal = new Vector3 (jumpPosition.position.x, jumpPosition.position.y, transform.position.z);
            gameObject.GetComponent<Rigidbody2D>().DOJump(jumpLocal, 150f, 1, 0.8f);
            jumped = true;
        }
    }

    public void initSheep()
    {
        transform.position = sheepSpawnPosition.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetLocation.position);
        transform.DOMove(targetLocation.position, 5f);
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
    }
}
