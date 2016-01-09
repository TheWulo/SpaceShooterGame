using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Camera
{
    public class ScreenShake : MonoBehaviour
    {
        float shake = 0;
        float shakeAmount = 0.2f;
        float decreaseFactor = 1.0f;
        Vector3 beforeShakePosition;

        void Start()
        {
            EventManager.PlayerTookDamage.Listeners += OnPlayerTookDamage;
        }

        private void OnPlayerTookDamage(PlayerTookDamageEventArgs args)
        {
            StartShake(args.Damage * 0.01f);
        }
 
        void Update() 
        {
            if (shake > 0)
            {
                HandleShake();
                shake -= Time.deltaTime * decreaseFactor;
                if (shake <= 0)
                {
                    shake = 0;
                    EndShake();
                }
            }
        }

        void StartShake(float time)
        {
            beforeShakePosition = gameObject.transform.position;
            shake += time;
        }

        void EndShake()
        {
            gameObject.transform.position = beforeShakePosition;
        }

        void HandleShake()
        {
            Vector3 targetPosition = Random.insideUnitSphere * shakeAmount;
            targetPosition.z = beforeShakePosition.z;
            gameObject.transform.localPosition = targetPosition;
        }
    }
}
