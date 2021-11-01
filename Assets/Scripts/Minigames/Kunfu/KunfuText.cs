using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;

public class KunfuText : MonoBehaviour
{

    TextMeshProUGUI tmpro;

    // Start is called before the first frame update
    void Start()
    {

        tmpro = GetComponent<TextMeshProUGUI>();
        tmpro.DOFade(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive()
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
}
