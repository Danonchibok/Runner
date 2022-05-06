using UnityEngine;

[CreateAssetMenu(menuName = "Runner/GameEvents/Void Event", fileName = "New Void Event")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());   
}
