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

    public Image lifeBar;
    public Image dialogueBackground;
    public TextMeshProUGUI dialogueText;
    public Image helpBackground;
    public TextMeshProUGUI helpText;

    public float startTime;
    public float delayStartTime;
    public float endTime;
    public float lineTime;
    public float letterTime;
    #endregion

    #region Funciones Unity
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            helpBackground.gameObject.SetActive(!helpBackground.gameObject.activeSelf);

        }
    }
    #endregion

    #region Funciones Propias
    public void UpdateLifeBar()
    {
        float _lifeActual = PlayerDataManager.instance.playerData.lifeActual;
        float _maxLife = PlayerDataManager.instance.playerData.maxLife;
        float _lifeNormalized = _lifeActual / _maxLife;
        lifeBar.fillAmount = _lifeNormalized;

        Debug.Log("La vida actual es " + _lifeNormalized);
    }

    public void UpdateDialogue(string _text)
    {
        dialogueText.text = "";
        StartCoroutine(UpdateDialogueCoroutine(_text));
    }
    public void UpdateHelp(string _text)
    {
        helpBackground.gameObject.SetActive(true);
        helpText.text = _text;
    }

    public void HideHelp()
    {
        helpBackground.gameObject.SetActive(false);
    }

    IEnumerator UpdateDialogueCoroutine(string _text)
    {
        string[] _lines = _text.Replace("\r", "").Split('\n');

        //    return lines.Length >= lineNo ? lines[lineNo - 1] : null;

        //int _letter = 0;
        yield return new WaitForSeconds(startTime);
        dialogueBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayStartTime);

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
                yield return new WaitForSeconds(letterTime);
            }
            dialogueText.text += "\r\n";
            yield return new WaitForSeconds(lineTime);
        }

        //while (_letter < _text.Length)
        //{
        //    dialogueText.text += _text[_letter];
        //    _letter++;
        //    yield return new WaitForSeconds(_letterTime);
        //}

        yield return new WaitForSeconds(endTime);

        dialogueBackground.gameObject.SetActive(false);

    }
    #endregion
}
