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
        SkinShopManager.Instance.LoadPrefabSkin();
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
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.BASICO);
    }
    public void SkinNegro()
    {
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.NEGRO);
    }
    public void SkinRubio()
    {
        GameManager.GetInstance().ChangeSkinState(SKIN_SELECTED.RUBIO);
    }
    public void SkinJoker()
    {
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