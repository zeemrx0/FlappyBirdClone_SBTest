using UnityEngine;

namespace LNE.Colliders
{
  public abstract class GameCollider : MonoBehaviour
  {
    public abstract bool IsCollidingWith(
      GameCollider collider,
      bool isTrigger = false
    );
    public abstract bool IsCollidingWithBoxCollider(
      GameBoxCollider collider,
      bool isTrigger = false
    );

    protected Vector2 _previousPosition;

    private void Start()
    {
      _previousPosition = transform.position;
    }
  }
}
