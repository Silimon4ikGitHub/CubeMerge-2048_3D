using UnityEngine;

public interface ICubeCollider
{
    public int Id {  get; set; }
    public int Value { get; set; }
    public void OnCollisionSuccess(ICubeCollider otherCollider);
}
