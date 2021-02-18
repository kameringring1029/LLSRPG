using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSecurityManager : MonoBehaviour
{
    public int x_max, y_max; // 縦横長さ設定（向き考えてない）
    private int height, width; // 画面の向きに合わせた幅と高さ

    public GameObject web_landscape; // 横向き用のWebのSprite
    public GameObject web_portrait; // 縦向き用のWebのSprite

    // Start is called before the first frame update
    void Start()
    {

        // アスペクト比によって表示を変更
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

            int x = Random.Range(-width/4/2+2, width/4/2-2) * 4 + 2;
            int y = Random.Range(-height / 6 / 2+2, height / 6 / 2-2) * 6 + 1;

            Vector3 position = new Vector3(x, y, 0);
            Instantiate(Resources.Load<GameObject>("Minigame/cybersecurity/cs_beam"), position, Quaternion.identity);
        }
    }
}
