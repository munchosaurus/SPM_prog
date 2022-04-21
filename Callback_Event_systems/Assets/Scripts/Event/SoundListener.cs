using System.Collections;
using UnityEngine;

namespace Event
{
    public class SoundListener : MonoBehaviour
    {
        [SerializeField] private GameObject musicPlayer;
        
        private void Start()
        {
            EventSystem.Current.RegisterListener<UnitDeathEventInfo>(OnUnitDied);
            
        }
        
        void OnUnitDied(UnitDeathEventInfo unitDeathEventInfo)
        {
            // Where to store the music player
            musicPlayer.transform.position = unitDeathEventInfo.EventUnitGo.transform.position;
            // Starts playing the sound after a delay of the killtimer
            StartCoroutine(PlaySound(unitDeathEventInfo));
            
        }

        IEnumerator PlaySound(UnitDeathEventInfo unitDeathEventInfo)
        {
            yield return new WaitForSeconds(unitDeathEventInfo.KillTimer);
            musicPlayer.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}