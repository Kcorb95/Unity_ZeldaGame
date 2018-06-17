using UnityEngine;

public class DestroyParticlesWhenDone : MonoBehaviour
{
    private ParticleSystem m_Particles;

    // Use this for initialization
    private void Awake()
    {
        m_Particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_Particles.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}