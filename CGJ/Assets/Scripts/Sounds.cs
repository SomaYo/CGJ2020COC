
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sounds : MonoBehaviour
{
    private AudioSource _audioSource;

    private static Sounds _instance;

    private void OnEnable()
    {
        _instance = this;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static Sounds Get()
    {
        return _instance;
    }

    [SerializeField]
    private AudioClip closeEyesSound;
    [SerializeField]
    private float closeEyesSoundIntervalMin = 5.0F;
    [SerializeField]
    private float closeEyesSoundIntervalMax = 15.0F;

    private bool _playCloseEyesSound;
    private float _closeEyesSoundInterval;
    private float _closeEyesSoundDuration;

    public void PlayCloseEyesSound()
    {
        _playCloseEyesSound = true;
        _closeEyesSoundDuration = 0.0F;
        _closeEyesSoundInterval = Random.Range(closeEyesSoundIntervalMin, closeEyesSoundIntervalMax);
    }

    public void StopCloseEyesSound()
    {
        _playCloseEyesSound = false;
        _closeEyesSoundDuration = 0.0F;
        _closeEyesSoundInterval = 0.0F;
    }
    private void Update()
    {
        if (_playCloseEyesSound)
        {
            _closeEyesSoundDuration += Time.deltaTime;
            if (_closeEyesSoundDuration >= _closeEyesSoundInterval)
            {
                if (_audioSource != null)
                    _audioSource.PlayOneShot(closeEyesSound);
                _closeEyesSoundDuration = 0.0F;
                _closeEyesSoundInterval = Random.Range(closeEyesSoundIntervalMin, closeEyesSoundIntervalMax);
            }
        }
    }

    [SerializeField]
    private AudioClip destructionSound;
    public void PlayDestructionSound()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(destructionSound);
    }

    [SerializeField]
    private AudioClip startButtonSound;
    public void PlayStartButtonSound()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(startButtonSound);
    }

    [SerializeField]
    private AudioClip switchWorldSound;
    public void PlaySwitchWorldSound()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(switchWorldSound);
    }

    [SerializeField]
    private AudioClip deathSound;
    public void PlayDeathSound()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(deathSound);
    }

    [SerializeField]
    private AudioClip[] walkSounds;
    [SerializeField]
    private float walkSoundInterval = 0.5F;
    private float _walkSoundTime;
    public void PlayWalkSound()
    {
        if (_walkSoundTime.Equals(0.0F) || Time.time - _walkSoundTime > walkSoundInterval)
        {
            _walkSoundTime = Time.time;
            if (_audioSource != null)
                _audioSource.PlayOneShot(walkSounds[Random.Range(0, walkSounds.Length)]);
        }
    }

    [SerializeField]
    private AudioClip monsterDropSound;
    public void PlayMonsterDropSound()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(monsterDropSound);
    }

    [SerializeField]
    private AudioClip endLevelSound;
    public void PlayEndLevelSound()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(endLevelSound);
    }
}
