using System.Collections;
using UnityEngine;

namespace Event
{
    public class ParticleListener : MonoBehaviour
    {
        private void Start()
        {
            EventSystem.Current.RegisterListener<UnitDeathEventInfo>(OnUnitDied, gameObject.GetInstanceID());
        }
        
        private void Update()
        {
            // testing remove
            if (Input.GetKeyDown(KeyCode.V))
            {
                EventSystem.Current.UnregisterListener<UnitDeathEventInfo>(OnUnitDied, gameObject.GetInstanceID());
            }
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