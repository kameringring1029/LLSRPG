using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
//using DG.Tweening;

public class WRhythmResult : MonoBehaviour
{
    public GameObject text_end;
    TextMeshProUGUI tmpro;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        tmpro = text_end.GetComponent<TextMeshProUGUI>();
    

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 結果開始 */
    public void startResult(int score) {

        this.score = score;


 //       tmpro.DOFade(0, 0);
        tmpro.text = getMessage();
 //       tweenText();
    }

    /* TextのTween */
 /*   void tweenText()
    {
        DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(tmpro);

        for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
        {
            tmproAnimator.DOScaleChar(i, 0.7f, 0);
            Vector3 currCharOffset = tmproAnimator.GetCharOffset(i);
            DOTween.Sequence()
                .Append(tmproAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 30, 0), 0.4f).SetEase(Ease.OutFlash, 2))
                .Join(tmproAnimator.DOFadeChar(i, 1, 0.4f))
                .Join(tmproAnimator.DOScaleChar(i, 1, 0.4f).SetEase(Ease.OutBounce))
                .SetDelay(0.07f * i);
        }
    }
 */
    /**/
    public string getMessage()
    {
        string message = "";

        if(score < 1000)
        {
            message += "まだまだ ですわね。";
        }
        else if(score < 2000)
        {
            message += "そこそこ できていましたね。";
        }

        message += "<br>" + score + "点";

        return message;
    }
}
