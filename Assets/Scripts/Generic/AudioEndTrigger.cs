using UnityEngine;
using UnityEngine.Events;

public class AudioEndTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAudioEnd;
    [SerializeField] private AudioSource audioSource;
    public bool TriggerIsActive {get; set;}

    void Update()
    {
        
        if ( !audioSource.isPlaying && TriggerIsActive)
        {
            _onAudioEnd.Invoke();
        }
    }
}
