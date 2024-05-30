using UnityEngine;

namespace LNE.Movements
{
  public class BirdMovementView : MonoBehaviour
  {
    public void Flap(float verticalSpeed)
    {
      transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }
  }
}
