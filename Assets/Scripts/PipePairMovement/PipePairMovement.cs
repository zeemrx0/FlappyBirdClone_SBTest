using UnityEngine;

namespace LNE.Movements
{
  public class PipePairMovement : MonoBehaviour {
    [SerializeField]
    private float _speed = 1f;

    void Update() {
      transform.position += Vector3.left * _speed * Time.deltaTime;
    }
  }
}
