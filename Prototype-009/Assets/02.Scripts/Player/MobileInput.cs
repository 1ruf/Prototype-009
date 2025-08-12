using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] private Transform ring1;
    [SerializeField] private Transform ring2;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }
    public void OnPointerMove(PointerEventData arg)
    {
        Vector2 curPos = _cam.ScreenToWorldPoint(arg.position);
        SetRing(curPos-Vector2.zero);
    }

    private void SetRing(Vector2 targetPos)
    {
        Vector2 ring1Pos = ring1.position;
        Vector2 ring2Pos = ring2.position;

        Vector2 direction1 = ring1Pos - targetPos;
        Vector2 direction2 = ring2Pos - targetPos;

        if (direction1.sqrMagnitude > 0.0001f)
        {
            ring1.up = direction1;
        }
        if (direction2.sqrMagnitude > 0.0001f)
        {
            ring2.up = direction2;
        }
    }

}
