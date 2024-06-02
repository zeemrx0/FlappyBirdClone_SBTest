using UnityEngine;

namespace LNE.Colliders
{
  public class GameCircleCollider : GameCollider
  {
    [field: SerializeField]
    public float Radius { get; set; } = 1f;

    [field: SerializeField]
    public Vector2 Offset { get; set; } = Vector2.zero;

    public override bool IsCollidingWith(
      GameCollider collider,
      bool isTrigger = false
    )
    {
      return collider switch
      {
        GameBoxCollider boxCollider => IsCollidingWithBoxCollider(boxCollider),
        _ => false,
      };
    }

    public override bool IsCollidingWithBoxCollider(
      GameBoxCollider collider,
      bool isTrigger = false
    )
    {
      var thisMin = new Vector2(
        transform.position.x + Offset.x - Radius,
        transform.position.y + Offset.y - Radius
      );
      var thisMax = new Vector2(
        transform.position.x + Offset.x + Radius,
        transform.position.y + Offset.y + Radius
      );

      var otherMin = new Vector2(
        collider.transform.position.x + collider.Offset.x - collider.Size.x / 2,
        collider.transform.position.y + collider.Offset.y - collider.Size.y / 2
      );
      var otherMax = new Vector2(
        collider.transform.position.x + collider.Offset.x + collider.Size.x / 2,
        collider.transform.position.y + collider.Offset.y + collider.Size.y / 2
      );

      bool isColliding =
        thisMin.x < otherMax.x
        && thisMax.x > otherMin.x
        && thisMin.y < otherMax.y
        && thisMax.y > otherMin.y;

      if (isColliding && !isTrigger)
      {
        transform.position = _previousPosition;
      }
      else
      {
        _previousPosition = transform.position;
      }

      return isColliding;
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position + (Vector3)Offset, Radius);
    }
  }
}
