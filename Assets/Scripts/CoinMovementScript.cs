using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CoinMovementScript : MonoBehaviour
{
    public Vector2 ToPosition;

    public Vector2 FromPosition;

    public Vector2 OffsetPosition;

    public Ease easeMovement;
    // Start is called before the first frame update
    void Start()
    {
        FromPosition = transform.position;
        ToPosition = FromPosition + OffsetPosition;
        MoveTween();
    }

    // Update is called once per frame
    public void MoveTween()
    {
        transform.DOMove(ToPosition,1f).From(FromPosition).SetLoops(-1,LoopType.Yoyo).SetEase(easeMovement);
    }
}
