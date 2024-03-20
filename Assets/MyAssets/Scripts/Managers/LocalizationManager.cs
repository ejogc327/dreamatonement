using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Tables;

/// <summary>
///
/// </summary>

public class LocalizationManager : MonoBehaviour
{
    #region Variables
    public static LocalizationManager instance;
    Animator anim;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
    }
    #endregion

    #region Funciones Propias

    public string GetFeriaDialogueText(string locate, string key)
    {
        var collection = LocalizationEditorSettings.GetStringTableCollection("FeriaDialogues");
        var spanishTable = (StringTable)collection.GetTable(locate);
        var entry = spanishTable.GetEntry(key);
        if (entry != null) return entry.Value;
        return "";
    }
    #endregion
}
