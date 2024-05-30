using UnityEngine;

namespace LNE.Movements
{
  public class BirdMovementView : MonoBehaviour
  {
    public void Flap(float verticalSpeed, float rotateSpeed)
    {
      transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
      transform.rotation = Quaternion.Euler(0, 0, verticalSpeed * rotateSpeed);
    }
  }
}
