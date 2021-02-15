using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSecurityManager : MonoBehaviour
{
    public int x_max, y_max;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createBeam());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator createBeam()
    {
        

        while (true)
        {
            float t = Random.Range(0.10f, 0.50f);
            yield return new WaitForSeconds(t);

            int x = Random.Range(-x_max/4/2+2, x_max/4/2-2) * 4 + 2;
            int y = Random.Range(-y_max / 6 / 2+2, y_max / 6 / 2-2) * 6 + 1;

            Vector3 position = new Vector3(x, y, 0);
            Instantiate(Resources.Load<GameObject>("Minigame/cybersecurity/cs_beam"), position, Quaternion.identity);
        }
    }
}
