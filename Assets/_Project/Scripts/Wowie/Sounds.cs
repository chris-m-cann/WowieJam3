using UnityEngine;
using Util;
using Util.Events;

namespace Wowie
{
    [RequireComponent(typeof(AudioSource))]
    public class Sounds : MonoBehaviour
    {
        [SerializeField] private AudioClip[] breakClips;
        [SerializeField] private AudioClip[] pickupClips;
        [SerializeField] private AudioClip goalClip;
        


        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlayBreak()
        {
            var clip = breakClips.RandomElement();
            _source.clip = clip;
            _source.Play();
        }

        public void PlayPickup()
        {
            var clip = pickupClips.RandomElement();
            _source.clip = clip;
            _source.Play();
        }

        public void PlayGoal()
        {
            _source.clip = goalClip;
            _source.Play();
        }
    }
}