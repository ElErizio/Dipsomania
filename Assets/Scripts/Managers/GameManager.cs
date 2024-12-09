using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {

        SkinShopManager2.Instance.LoadSavedSkin();

        switch (SkinShopManager2.Instance.currentSkinIndex)
        { 
            case 0:
                SkinBasica();
                break;
            case 1:
                SkinNegro();
                break;
            case 2:
                SkinRubio();
                break;
            case 3:
                SkinJoker();
                break;
        }
    }


    public Action<GAME_STATE> OnGameStateChanged;
    public GAME_STATE currentGameState = GAME_STATE.PLAY;

    public void ChangeGameState(GAME_STATE _newGameState)
    {
        if (currentGameState == _newGameState)
        {
            return;
        }

        currentGameState = _newGameState;
        Debug.Log("Se cambio el modo de juego a: " + currentGameState);
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged.Invoke(currentGameState);
        }
    }

    public Action<SKIN_SELECTED> onSkinChanged;
    public SKIN_SELECTED currentSkin;
    public void ChangeSkinState(SKIN_SELECTED _selectedSkin)
    {
        currentSkin = _selectedSkin;
        Debug.Log("Current skin: " + currentSkin);

        if (onSkinChanged != null)
        {
            onSkinChanged.Invoke(currentSkin);
        }
    }

    public void SkinBasica()
    {
        SkinShopManager2.Instance.ChangeSkin(0);
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.BASICO);
    }
    public void SkinNegro()
    {
        SkinShopManager2.Instance.ChangeSkin(1);
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.NEGRO);
    }
    public void SkinRubio()
    {
        SkinShopManager2.Instance.ChangeSkin(2);
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.RUBIO);
    }   
    public void SkinJoker()
    {
        SkinShopManager2.Instance.ChangeSkin(3);
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.JOKER);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.GetInstance().ChangeGameState(GAME_STATE.PLAY);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.GetInstance().ChangeGameState(GAME_STATE.GAME_OVER);
        }
    }
}

public enum GAME_STATE
{
    PLAY,
    PAUSE,
    GAME_OVER,
    MENU
}

public enum SKIN_SELECTED
{
    BASICO,
    NEGRO,
    RUBIO,
    JOKER
}