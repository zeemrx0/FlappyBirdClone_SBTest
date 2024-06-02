using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Pipes
{
  public class PipePair : MonoBehaviour
  {
    public IObjectPool<PipePair> BelongingPool { get; set; }

    [SerializeField]
    private float _destroyXPosition = -10f;

    public void SetSpaceBetween(float space)
    {
      transform.GetChild(0).localPosition = new Vector3(0, space / 2, 0);
      transform.GetChild(1).localPosition = new Vector3(0, -space / 2, 0);
    }

    private void LateUpdate()
    {
      if (transform.position.x < _destroyXPosition)
      {
        BelongingPool.Release(this.GetComponent<PipePair>());
      }
    }
  }
}
