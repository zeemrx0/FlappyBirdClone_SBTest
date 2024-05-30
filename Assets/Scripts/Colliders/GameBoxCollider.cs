using UnityEngine;

namespace LNE.Colliders
{
  public class GameBoxCollider : GameCollider
  {
    [field: SerializeField]
    public Vector2 Size { get; set; } = Vector2.one;

    [field: SerializeField]
    public Vector2 Offset { get; set; } = Vector2.zero;

    public override bool IsCollidingWith(GameCollider collider)
    {
      switch (collider)
      {
        case GameBoxCollider boxCollider:
          return IsCollidingWithBoxCollider(boxCollider);
      }

      return false;
    }

    public override bool IsCollidingWithBoxCollider(GameBoxCollider collider)
    {
      var thisMin = new Vector2(
        transform.position.x + Offset.x - Size.x / 2,
        transform.position.y + Offset.y - Size.y / 2
      );
      var thisMax = new Vector2(
        transform.position.x + Offset.x + Size.x / 2,
        transform.position.y + Offset.y + Size.y / 2
      );

      var otherMin = new Vector2(
        collider.transform.position.x + collider.Offset.x - collider.Size.x / 2,
        collider.transform.position.y + collider.Offset.y - collider.Size.y / 2
      );
      var otherMax = new Vector2(
        collider.transform.position.x + collider.Offset.x + collider.Size.x / 2,
        collider.transform.position.y + collider.Offset.y + collider.Size.y / 2
      );

      return thisMin.x < otherMax.x
        && thisMax.x > otherMin.x
        && thisMin.y < otherMax.y
        && thisMax.y > otherMin.y;
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(transform.position + (Vector3)Offset, (Vector3)Size);
    }
  }
}
