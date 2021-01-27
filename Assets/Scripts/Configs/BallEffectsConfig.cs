using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BallEffectsConfig", menuName = "Configs/Ball", order = 1)]
    public class BallEffectsConfig : ScriptableObject
    {
        [SerializeField] private ParticleSystem _spawn;
        [SerializeField] private ParticleSystem _collision;
        [SerializeField] private ParticleSystem _reduce;
        [SerializeField] private ParticleSystem _destroy;

        public ParticleSystem Spawn => _spawn;
        public ParticleSystem Collision => _collision;
        public ParticleSystem Reduce => _reduce;
        public ParticleSystem Destroy => _destroy;
    }
}