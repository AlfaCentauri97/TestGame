using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : SingletonMonoBehaviour<QuestMgr>
{
    public QuestLogController questLogController;

    public void ToggleQuestLog()
    {
        bool isActive = questLogController.gameObject.activeSelf;
        questLogController.gameObject.SetActive(!isActive);
    }
}
