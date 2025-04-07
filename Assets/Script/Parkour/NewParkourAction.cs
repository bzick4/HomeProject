using UnityEngine;

[CreateAssetMenu(menuName =  "Parkour Menu / Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [Header("Check Obstacle height")]
    [SerializeField] string _AnimationName;
    [SerializeField] float _MinHeight;
    [SerializeField] float _MaxHeight;

    [Header("Roating Player forward obstacle")]
    [SerializeField] bool _IsLookAtObstacle;
    public Quaternion RequiredRotation {get; set;}

    [Header("Target Matching")]
    [SerializeField] private bool _IsAllowTargetMatching =true;
    [SerializeField] AvatarTarget _AvatarTarget;
    [SerializeField] private float _CompareStartTime;
    [SerializeField] private float _CompareEndTime;
    public Vector3 ComparePosition {get; set;}
    
    public bool IsCheckAvailable(ObstacleInfo HitData, Transform player)
    {
        float checkHeight = HitData.HeightInfo.point.y - player.position.y;
        
        if(checkHeight < _MinHeight || checkHeight > _MaxHeight) return false;

        if(_IsLookAtObstacle) RequiredRotation = Quaternion.LookRotation(-HitData.HitInfo.normal);
        
        if(_IsAllowTargetMatching) ComparePosition = HitData.HeightInfo.point;
        
        return true;
    }

    public string AnimationName => _AnimationName;
    public bool IsLookAtObstacle => _IsLookAtObstacle;
    public bool IsAllowTargetMatching => _IsAllowTargetMatching;
    public AvatarTarget AvatarTarget => _AvatarTarget;
    public float CompareStartTime => _CompareStartTime;
    public float CompareEndTime => _CompareEndTime;

}
