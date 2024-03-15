using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
///
/// </summary>

public class HudManager : MonoBehaviour
{
    #region Variables
    public static HudManager instance;

    Image lifeBar;
    Image dialogueBackground;
    public TextMeshProUGUI dialogueText;
    #endregion

    #region Funciones Unity
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region Funciones Propias
    public void UpdateLifeBar()
    {
        float _lifeActual = PlayerDataManager.instance.playerData.lifeActual;
        float _maxLife = PlayerDataManager.instance.playerData.maxLife;
        float _lifeNormalized = _lifeActual / _maxLife;
        lifeBar.fillAmount = _lifeNormalized;
    }

    public void Show(bool _show)
    {
        dialogueBackground.gameObject.SetActive(_show);
    }

    void UpdateDialogue(string _text)
    {
        dialogueText.text = _text;
    }
    #endregion
}
