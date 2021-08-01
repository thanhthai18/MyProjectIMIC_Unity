using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationPopup : MonoBehaviour
{
    public static AnimationPopup instance;
    public GameObject popupEndGame;
    public Button btnReplay, btnMenu;
    public starFxController StarFxController;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    [ContextMenu("ShowPopWinGame")]
    public void ShowPopWinGame()
    {
        StartCoroutine(IEShowPopWinGame());
    }

    IEnumerator IEShowPopWinGame()
    {
        popupEndGame.SetActive(true);
        popupEndGame.transform.GetChild(1).DOScale(0.83f, 0.5f).OnComplete(()=> 
        { popupEndGame.transform.GetChild(1).DOScale(0.75f, 0.5f).OnComplete(()=>
        { popupEndGame.transform.GetChild(1).DOScale(0.8f, 0.5f); }); });

        yield return new WaitForSeconds(0.2f);
        StarFxController.isEnd = false;

        yield return new WaitForSeconds(0.8f);
        btnReplay.GetComponent<RectTransform>().DOScale(Vector2.one, 0.01f).SetUpdate(UpdateType.Normal, true).OnComplete(() => btnReplay.GetComponent<RectTransform>().DOPunchScale(new Vector2(0.3f, 0.3f), 0.3f, 10).SetUpdate(UpdateType.Normal, true));
        btnMenu.GetComponent<RectTransform>().DOScale(Vector2.one, 0.01f).SetUpdate(UpdateType.Normal, true).OnComplete(() => btnMenu.GetComponent<RectTransform>().DOPunchScale(new Vector2(0.3f, 0.3f), 0.3f, 10).SetUpdate(UpdateType.Normal, true));
    }
}
