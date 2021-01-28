using UnityEngine;

namespace Unit
{
    public static class BallExtention
    {
        public static void PlayEffect(this Ball ball, ParticleSystem effect)
        {
            return;
            var spawnedEffect = Object.Instantiate(effect, ball.transform.position,
                Quaternion.identity);
            spawnedEffect.transform.SetParent(ball.transform);
            Object.Destroy(spawnedEffect.gameObject, spawnedEffect.main.duration);
        }

        public static void PlayEffect(this Ball ball, ParticleSystem effect, Color color)
        {
            return;
            
            var spawnedEffect = Object.Instantiate(effect, ball.transform.position,
                Quaternion.identity);
            
            var mainModule = spawnedEffect.main;
            mainModule.startColor = color;
            
            Object.Destroy(spawnedEffect.gameObject, mainModule.duration);
        }
    }
}