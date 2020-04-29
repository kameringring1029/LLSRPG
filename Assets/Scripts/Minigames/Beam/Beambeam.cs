using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beambeam : MonoBehaviour
{

    bool trg;

    // Start is called before the first frame update
    void Start()
    {
        trg = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {
        if (!trg) { 
			Debug.Log("trigger");
			trg = true;
		}

        ParticleSystem ps = GetComponent<ParticleSystem>();

        //　Particle型のインスタンス生成
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        //　Inside、Enterのパーティクルを取得
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //　データがあればキャラクターに接触した
        if (numInside != 0 || numEnter != 0)
        {
            Debug.Log("接触");
        }

    }
}
