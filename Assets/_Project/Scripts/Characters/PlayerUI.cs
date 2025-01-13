using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI
{
    private Image[] spellIcons;

    private int iconInd = 0;

    private float scaleMultiplier = 1.5f;
    private float scaleDuration = 0.5f;

    private float initScale = 1f;

    public PlayerUI(params Image[] spellIcons)
    {
        this.spellIcons = spellIcons;

        spellIcons[0].transform.DOScale(initScale * scaleMultiplier, scaleDuration);
    }

    public void EmphasizeNextIcon()
    {
        spellIcons[iconInd].transform.DOScale(initScale, scaleDuration);

        iconInd = (1 + iconInd) % spellIcons.Length;

        spellIcons[iconInd].transform.DOScale(initScale * scaleMultiplier, scaleDuration);
    }
}
