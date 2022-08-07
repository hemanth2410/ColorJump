using UnityEngine;
using UnityEngine.Rendering;
namespace Assets.Scripts
{
    /// <summary>
    /// Behaviors for each prefab like update it's size and color to audio
    /// </summary>
    public class ParticleBehaviour : MonoBehaviour
    {
        #region Fields
        
        #endregion

        #region Properties
        internal float responseSpeed;
        internal float maxHeight { get; set; }
        [SerializeField] int spectrumIndex;
        #endregion
        ParticleSystem pr;
        #region Event Functions
        /// <summary>
        /// Saves starting state to properties
        /// </summary>
        private void Start()
        {
            pr = GetComponent<ParticleSystem>();
            WebGLBehaviour.Instance.ParticleBurst += Instance_ParticleBurst;
        }

        private void Instance_ParticleBurst()
        {
            pr.Emit(Random.Range(10,50));
        }
        private void OnDestroy()
        {
            WebGLBehaviour.Instance.ParticleBurst -= Instance_ParticleBurst;
        }
        /// <summary>
        /// Updates prefab's height or size as well as color
        /// </summary>
        private void Update()
        {
#if !UNITY_WEBGL
            pr.Emit((int)(AudioManager.GetSpectrumValue(spectrumIndex) * 5));
#endif
        }
#endregion

        
    }
}


