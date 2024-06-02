using UnityEngine;

namespace LNE.Core
{
  public class VFX : MonoBehaviour
  {
    [field: SerializeField]
    public bool IsLoop { get; set; } = false;

    private Animator _animator;

    private void Awake()
    {
      _animator = GetComponent<Animator>();
    }

    private void Start()
    {
      if (_animator != null && !IsLoop)
      {
        Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
      }
    }
  }
}
