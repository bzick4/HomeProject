using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =  "Parkour Menu / Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [SerializeField] string _AnimationName;
    [SerializeField] float _MinHeight;
    [SerializeField] float _MaxHeight;

    public bool IsCheckAvailable(ObstacleInfo HitInfo, Transform player)
    {
        float checkHeight = HitInfo.HeightInfo.point.y - player.position.y;
        
        if(checkHeight < _MinHeight || checkHeight > _MaxHeight) 
        {return false;}
        
        
        return true;
    }

    public string AnimationName => _AnimationName;
}
