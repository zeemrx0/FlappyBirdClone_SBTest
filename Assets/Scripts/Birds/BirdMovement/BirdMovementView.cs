using System.Collections;
using UnityEngine;

namespace LNE.Birds
{
  public class BirdMovementView : MonoBehaviour
  {
    [SerializeField]
    private AudioClip _flapSound;

    [SerializeField]
    private AudioClip _hitSound;

    [SerializeField]
    private AudioClip _fallSound;

    private AudioSource _audioSource;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
    }

    public void Fly(float verticalSpeed, float rotateSpeed)
    {
      transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
      transform.rotation = Quaternion.Euler(0, 0, verticalSpeed * rotateSpeed);
    }

    public void PlayFlapSound()
    {
      _audioSource.PlayOneShot(_flapSound);
    }

    public void PlayHitSound()
    {
      _audioSource.PlayOneShot(_hitSound);
    }

    public void PlayFallSound()
    {
      _audioSource.PlayOneShot(_fallSound);
    }

    private IEnumerator PlayFallSound(float delay)
    {
      yield return new WaitForSeconds(delay);
      _audioSource.PlayOneShot(_fallSound);
    }
  }
}
