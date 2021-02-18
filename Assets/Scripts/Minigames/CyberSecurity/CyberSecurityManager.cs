using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSecurityManager : MonoBehaviour
{
    public int x_max, y_max;
    private int height, width;

    public GameObject web_landscape;
    public GameObject web_portrait;

    // Start is called before the first frame update
    void Start()
    {

        //現在のアスペクト比を取得
        float currentAspect = (float)Screen.height / (float)Screen.width;

        if(currentAspect > 1)/* portrait */
        {
            height = x_max; width = y_max;
            web_landscape.SetActive(false);
            web_portrait.SetActive(true);
        }
        else/* landscape */
        {
            height = y_max; width = x_max;
            web_landscape.SetActive(true);
            web_portrait.SetActive(false);
        }


        GetComponent<CameraSizeUpdater>().setHeight(height);
        GetComponent<CameraSizeUpdater>().setWidth(width);

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
