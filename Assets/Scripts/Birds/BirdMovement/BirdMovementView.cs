using UnityEngine;

namespace LNE.Birds
{
  public class BirdMovementView : MonoBehaviour
  {
    public void Flap(float verticalSpeed)
    {
      transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }

    public void Rotate(float verticalSpeed, float rotateSpeed)
    {
      transform.rotation = Quaternion.Euler(0, 0, verticalSpeed * rotateSpeed);
    }
  }
}
