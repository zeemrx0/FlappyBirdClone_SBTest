using LNE.Colliders;
using UnityEngine;

namespace LNE.Utilities
{
  public class ColliderHelper : MonoBehaviour
  {
    public static Vector2? GetTouchPoint(
      GameCircleCollider circleCollider,
      GameBoxCollider boxCollider
    )
    {
      var boxMin = new Vector2(
        boxCollider.transform.position.x
          + boxCollider.Offset.x
          - boxCollider.Size.x / 2,
        boxCollider.transform.position.y
          + boxCollider.Offset.y
          - boxCollider.Size.y / 2
      );
      var boxMax = new Vector2(
        boxCollider.transform.position.x
          + boxCollider.Offset.x
          + boxCollider.Size.x / 2,
        boxCollider.transform.position.y
          + boxCollider.Offset.y
          + boxCollider.Size.y / 2
      );

      var circleCenter = new Vector2(
        circleCollider.transform.position.x + circleCollider.Offset.x,
        circleCollider.transform.position.y + circleCollider.Offset.y
      );
      var circleRadius = circleCollider.Radius;

      // Check if colliding
      bool isColliding =
        circleCenter.x + circleRadius > boxMin.x
        && circleCenter.x - circleRadius < boxMax.x
        && circleCenter.y + circleRadius > boxMin.y
        && circleCenter.y - circleRadius < boxMax.y;

      if (!isColliding)
      {
        return null;
      }

      // Calculate touch point (simplified example, adjust as needed)
      float closestX = Mathf.Clamp(circleCenter.x, boxMin.x, boxMax.x);
      float closestY = Mathf.Clamp(circleCenter.y, boxMin.y, boxMax.y);

      Vector2 closestPoint = new Vector2(closestX, closestY);
      Vector2 direction = (closestPoint - circleCenter).normalized;
      Vector2 touchPoint = circleCenter + direction * circleRadius;

      return touchPoint;
    }
  }
}
