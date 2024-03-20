using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

/// <summary>
///
/// </summary>

public class HudManager : MonoBehaviour
{
    #region Variables
    public static HudManager instance;

    Image lifeBar;
    public Image dialogueBackground;
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

    public void UpdateDialogue(float _startTime, float _delayStartTime, float _endTime, float _letterTime, string _text)
    {
        StartCoroutine(UpdateDialogueCoroutine(_startTime, _delayStartTime, _endTime, _letterTime, _text));
    }

    IEnumerator UpdateDialogueCoroutine(float _startTime, float _delayStartTime, float _endTime, float _letterTime, string _text)
    {
        string[] _lines = _text.Replace("\r", "").Split('\n');

        //    return lines.Length >= lineNo ? lines[lineNo - 1] : null;

        int _letter = 0;
        yield return new WaitForSeconds(_startTime);
        dialogueBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(_delayStartTime);

        for (int i = 0; i < _lines.Length; i++)
        {
            // remove the first line.
            if (i > 3)
            {
                dialogueText.text = String.Join("\n", dialogueText.text.Split('\n').Skip(1));
            }
            for (int j = 0; j < _lines[i].Length; j++)
            {
                dialogueText.text += _lines[i][j];
                yield return new WaitForSeconds(_letterTime);
            }
            dialogueText.text += "\r\n";

        }

        //while (_letter < _text.Length)
        //{
        //    dialogueText.text += _text[_letter];
        //    _letter++;
        //    yield return new WaitForSeconds(_letterTime);
        //}

        yield return new WaitForSeconds(_endTime);

        dialogueBackground.gameObject.SetActive(false);

    }
    #endregion
}
