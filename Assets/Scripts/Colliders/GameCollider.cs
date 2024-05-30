using UnityEngine;

namespace LNE.Colliders
{
  public abstract class GameCollider : MonoBehaviour {
    public abstract bool IsCollidingWith(GameCollider collider);
    public abstract bool IsCollidingWithBoxCollider(GameBoxCollider collider);
  }
}
