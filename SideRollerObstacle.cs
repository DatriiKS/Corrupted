using UnityEngine;
using DG.Tweening;

public class SideRollerObstacle : MovingObstacle
{
    [SerializeField]
    private float _moveTime = 3;

    private Vector3 _firstPoint;
    private Vector3 _secondPoint;
    private RaycastHit _hit;

    private void Start()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
            _hit = hit;
        else
            gameObject.SetActive(false);

        CalculatePoints(_hit);
        transform.position = _firstPoint;
        AlignRotationToTrajectory();
        _tween = transform.DOMove(_secondPoint, _moveTime).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
    }

    private void AlignRotationToTrajectory()
    {
        Vector3 trajDirection = (_firstPoint - _secondPoint).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, trajDirection), Vector3.up);
    }
    private void CalculatePoints(RaycastHit hit)
    {
        if (hit.collider is BoxCollider boxCollider)
        {
            Transform boxTransform = boxCollider.transform;

            Vector3 boxCenter = boxCollider.center;
            Vector3 localHitPos = boxTransform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
            float halfXSize = boxCollider.size.x * 0.5f;

            Vector3 firstPointLocal = new Vector3(boxCenter.x + halfXSize, localHitPos.y, localHitPos.z);
            Vector3 secondPointLocal = new Vector3(boxCenter.x - halfXSize, localHitPos.y, localHitPos.z);
            Vector3 firstPointWorld = boxTransform.TransformPoint(firstPointLocal);
            Vector3 secondPointWorld = boxTransform.TransformPoint(secondPointLocal);
            Vector3 pathDirection = (secondPointWorld - firstPointWorld).normalized;

            Vector3 sideOffset = pathDirection * transform.localScale.x * .5f;
            Vector3 verticalOffset = Vector3.up * transform.localScale.y * .5f;

            _firstPoint = firstPointWorld + sideOffset + verticalOffset;
            _secondPoint = secondPointWorld - sideOffset + verticalOffset;
        }
    }
    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
            _hit = hit;
        CalculatePoints(_hit);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(_firstPoint, .5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_secondPoint, .5f);
    }
}
