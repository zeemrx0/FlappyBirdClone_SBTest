using UnityEngine;

namespace LNE.Pipes
{
  public class PipePair : MonoBehaviour
  {
    public void SetSpaceBetween(float space)
    {
      transform.GetChild(0).localPosition = new Vector3(0, space / 2, 0);
      transform.GetChild(1).localPosition = new Vector3(0, -space / 2, 0);
    }
  }
}
