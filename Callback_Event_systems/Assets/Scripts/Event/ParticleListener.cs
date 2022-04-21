using System.Collections;
using UnityEngine;

namespace Event
{
    public class ParticleListener : MonoBehaviour
    {
        private void Start()
        {
            EventSystem.Current.RegisterListener<UnitDeathEventInfo>(OnUnitDied);
        }
        
        

        void OnUnitDied(UnitDeathEventInfo unitDeathEventInfo)
        {
            StartCoroutine(PlayParticles(unitDeathEventInfo));
        }

        IEnumerator PlayParticles(UnitDeathEventInfo unitDeathEventInfo)
        {
            yield return new WaitForSeconds(unitDeathEventInfo.KillTimer - 0.5f);
            unitDeathEventInfo.EventUnitGo.GetComponent<ParticleSystem>().Play();
        }
    }
}