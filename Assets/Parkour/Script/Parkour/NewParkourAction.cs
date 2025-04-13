using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =  "Parkour Menu / Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [Header("Check Obstacle height")]
    [SerializeField] private string _AnimationName;
    [SerializeField] private string _BarrierTag;
    [SerializeField] private float _MinHeight;
    [SerializeField] private float _MaxHeight;

    [Header("Roating Player forward obstacle")]
    [SerializeField] bool _IsLookAtObstacle;
    [SerializeField] float _ParkourActionDelay;
    public Quaternion RequiredRotation {get; set;}

    [Header("Target Matching")]
    [SerializeField] private bool _IsAllowTargetMatching =true;
    [SerializeField] AvatarTarget _AvatarTarget;
    [SerializeField] private float _CompareStartTime;
    [SerializeField] private float _CompareEndTime;
    [SerializeField] private Vector3 _ComparePositionWeight = new Vector3(0,1,0);
    public Vector3 ComparePosition {get; set;}
    


    public bool IsCheckAvailable(ObstacleInfo HitData, Transform player)
    {
        if(!string.IsNullOrEmpty(_BarrierTag) && HitData.HitInfo.transform.tag !=_BarrierTag) return false;

        float checkHeight = HitData.HeightInfo.point.y - player.position.y;
        
        if(checkHeight < _MinHeight || checkHeight > _MaxHeight) return false;

        if(_IsLookAtObstacle) RequiredRotation = Quaternion.LookRotation(-HitData.HitInfo.normal);
        
        if(_IsAllowTargetMatching) ComparePosition = HitData.HeightInfo.point;
        
        return true;
    }

    public string AnimationName => _AnimationName;
    public bool IsLookAtObstacle => _IsLookAtObstacle;
    public float ParkourActionDelay => _ParkourActionDelay;
    public bool IsAllowTargetMatching => _IsAllowTargetMatching;
    public AvatarTarget AvatarTarget => _AvatarTarget;
    public float CompareStartTime => _CompareStartTime;
    public float CompareEndTime => _CompareEndTime;
    public Vector3 ComparePositionWeight => _ComparePositionWeight;


}
