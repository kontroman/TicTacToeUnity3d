using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameBoard gameBoard;
    public GameObject yourMoveObject;

    public List<Field> _fields;

    public enum turn
    {
        firstPlayerTurn,
        secondPlayerTurn
    }
    private enum gameResoult
    {
        nobodyWon,
        somebodyWon,
        deadHeat
    }
    public turn currentTurn;

    private Field.StateOfField firstPlayerMarker;
    private Field.StateOfField secondPlayerMarker;

    public UnityEngine.UI.Image crossCurrentTurnRepresentation;
    public UnityEngine.UI.Image circleCurrentTurnRepresentation;

    public void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            this.firstPlayerMarker = Field.StateOfField.CROSS;
            this.secondPlayerMarker = Field.StateOfField.CIRCLE;
            this.currentTurn = turn.firstPlayerTurn;
        }
        else
        {
            Destroy(this);
        }
    }

    public void RestartGame()
    {
        currentTurn = turn.secondPlayerTurn;
        this.gameBoard.LeftTop.State = Field.StateOfField.EMPTY;
        this.gameBoard.MiddleTop.State = Field.StateOfField.EMPTY;
        this.gameBoard.RightTop.State = Field.StateOfField.EMPTY;
        this.gameBoard.LeftMiddle.State = Field.StateOfField.EMPTY;
        this.gameBoard.Center.State = Field.StateOfField.EMPTY;
        this.gameBoard.RightMiddle.State = Field.StateOfField.EMPTY;
        this.gameBoard.LeftDown.State = Field.StateOfField.EMPTY;
        this.gameBoard.MiddleDown.State = Field.StateOfField.EMPTY;
        this.gameBoard.RightDown.State = Field.StateOfField.EMPTY;
    }

    private gameResoult someoneWon()
    {
        if (gameBoard.LeftTop.State == gameBoard.MiddleTop.State &&
           gameBoard.LeftTop.State == gameBoard.RightTop.State &&
           gameBoard.MiddleTop.State == gameBoard.RightTop.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.LeftMiddle.State == gameBoard.Center.State &&
            gameBoard.LeftMiddle.State == gameBoard.RightMiddle.State &&
            gameBoard.Center.State == gameBoard.RightMiddle.State &&
           gameBoard.LeftMiddle.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.LeftDown.State == gameBoard.MiddleDown.State &&
           gameBoard.LeftDown.State == gameBoard.RightDown.State &&
           gameBoard.MiddleDown.State == gameBoard.RightDown.State &&
           gameBoard.LeftDown.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.LeftTop.State == gameBoard.LeftMiddle.State &&
           gameBoard.LeftTop.State == gameBoard.LeftDown.State &&
           gameBoard.LeftMiddle.State == gameBoard.LeftDown.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.MiddleTop.State == gameBoard.Center.State &&
           gameBoard.MiddleTop.State == gameBoard.MiddleDown.State &&
           gameBoard.Center.State == gameBoard.MiddleDown.State &&
           gameBoard.MiddleTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.RightTop.State == gameBoard.RightMiddle.State &&
           gameBoard.RightTop.State == gameBoard.RightDown.State &&
           gameBoard.RightMiddle.State == gameBoard.RightDown.State &&
           gameBoard.RightTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.LeftTop.State == gameBoard.Center.State &&
           gameBoard.LeftTop.State == gameBoard.RightDown.State &&
           gameBoard.Center.State == gameBoard.RightDown.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.RightTop.State == gameBoard.Center.State &&
           gameBoard.RightTop.State == gameBoard.LeftDown.State &&
           gameBoard.Center.State == gameBoard.LeftDown.State &&
           gameBoard.RightTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.LeftTop.State != Field.StateOfField.EMPTY &&
            gameBoard.MiddleTop.State != Field.StateOfField.EMPTY &&
            gameBoard.RightTop.State != Field.StateOfField.EMPTY &&
            gameBoard.LeftMiddle.State != Field.StateOfField.EMPTY &&
            gameBoard.Center.State != Field.StateOfField.EMPTY &&
            gameBoard.RightMiddle.State != Field.StateOfField.EMPTY &&
            gameBoard.LeftDown.State != Field.StateOfField.EMPTY &&
            gameBoard.MiddleDown.State != Field.StateOfField.EMPTY &&
            gameBoard.RightDown.State != Field.StateOfField.EMPTY)
            return gameResoult.deadHeat;

        return gameResoult.nobodyWon;
    }
    public void FieldWasClicked(Field field)
    {
        if (field.State != Field.StateOfField.EMPTY) return;

        field.State = this.currentTurn == turn.firstPlayerTurn ? firstPlayerMarker : secondPlayerMarker;

        gameResoult currentGameReoult = someoneWon();

        if (currentGameReoult != gameResoult.nobodyWon) endGame(currentGameReoult);

        if (currentTurn == turn.firstPlayerTurn)
        {
            this.currentTurn = turn.secondPlayerTurn;
            yourMoveObject.SetActive(false);
            ComputerMove();
        }
        else
        {
            this.currentTurn = turn.firstPlayerTurn;
            yourMoveObject.SetActive(true);
        }
    }

    void ComputerMove()
    {
        Shuffle();
        foreach (Field f in _fields)
        {
            if (f.State == Field.StateOfField.EMPTY)
            {
                FieldWasClicked(f);
                f.State = Field.StateOfField.CIRCLE;
                return;
            }
        }
    }
    public void Shuffle()
    {
        Field tempGO;
        for (int i = 0; i < _fields.Count; i++)
        {
            int rnd = Random.Range(0, _fields.Count);
            tempGO = _fields[rnd];
            _fields[rnd] = _fields[i];
            _fields[i] = tempGO;
        }
    }

    private void endGame(gameResoult currentGameReoult)
    {
        Debug.Log("endGame");
        if (currentGameReoult == gameResoult.somebodyWon)
        {
            Debug.Log("somebodyWon");
            string winnerName = currentTurn == turn.firstPlayerTurn ? "First Player" : "Second Player";
            MenuController.won++;
            //StatementsManager.Instance.ShowStatement(winnerName + " won", "Restart Game", this.RestartGame);
        }
        else if (currentGameReoult == gameResoult.deadHeat)
        {
            MenuController.lose++;
            Debug.Log("dead-heat");
            //StatementsManager.Instance.ShowStatement("Dead-heat! Nobody won!", "Restart Game", this.RestartGame);
        }
        RestartGame();
    }
}
