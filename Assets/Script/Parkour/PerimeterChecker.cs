using UnityEngine;

public class PerimeterChecker : MonoBehaviour
{
   public Vector3 RayOffset = new Vector3(0, 0.2f, 0);
   public float RayLength = 0.9f, HeightRayLenght = 6f;
   public LayerMask ObstacleLayer;

   public ObstacleInfo CheckObstacle()
   {
     var _hitData = new ObstacleInfo();

     var _rayOrigin = transform.position + RayOffset;
     _hitData.isHitFound = Physics.Raycast(_rayOrigin, transform.forward, out _hitData.HitInfo, RayLength, ObstacleLayer);

     Debug.DrawRay(_rayOrigin, transform.forward * RayLength, (_hitData.isHitFound) ? Color.red : Color.green);

     if(_hitData.isHitFound)
     {
        var _heightOrigin = _hitData.HitInfo.point + Vector3.up * HeightRayLenght;
        _hitData.isHeightFound = Physics.Raycast(_heightOrigin, Vector3.down, out _hitData.HeightInfo, HeightRayLenght, ObstacleLayer);

        Debug.DrawRay(_heightOrigin, Vector3.down * HeightRayLenght, (_hitData.isHeightFound) ? Color.blue : Color.green);
     }

      return _hitData;
   }

}


   public struct ObstacleInfo
   {
    public bool isHitFound;
    public bool isHeightFound;
    public RaycastHit HitInfo;
    public RaycastHit HeightInfo;
   }
