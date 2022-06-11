using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{

    public Vector2 startPosition;

    public Vector2 endPosition;

    public float movementTime;
    
    // Start is called before the first frame update
    void Start()
    {
        StartPatrol();
    }

    public void StartPatrol()
    {
        transform.DOMove(endPosition, movementTime).From(startPosition).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
    }
    
}
