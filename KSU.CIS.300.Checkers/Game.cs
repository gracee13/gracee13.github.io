/*Game.cs
 * Auhor: Grace Earnhardt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.CIS300.Checkers
{
    public class Game
    {
        //dictionary that stores representation of checker board
        private Dictionary<int, LinkedListCell<BoardSquare>> _board;

        //number of red pieces
        private int _redCount;

        //number of black pieces
        private int _blackCount;

        //current piece selected
        public BoardSquare SelectedPiece;

        //keeps track of whose turn it is
        public SquareColor Turn;

        /// <summary>
        /// builds the game board using _board dictionary
        /// and each row is a linked list
        /// </summary>
        private void CreateBoard()
        {
            _board = new Dictionary<int, LinkedListCell<BoardSquare>>();
            for(int i = 1; i <= 8 ; i ++)
            {
                _board[i] = null;
              

                for (int j = 1; j <= 8; j++)
                {
                    BoardSquare create = new BoardSquare(i, j);
                    create.King = false;
     
                    if (i< 4 && j % 2 != i % 2)
                    {
                        create.Color = SquareColor.Red;
                        _redCount++;
                    }
                    else if(i > 5 && j % 2 != i % 2)
                    {
                        create.Color = SquareColor.Black;
                        _blackCount++;
                    }
                    else
                    {
                        create.Color = SquareColor.None;
                    }

                    LinkedListCell<BoardSquare> temp = new LinkedListCell<BoardSquare>();
                    temp.Data = create;
                    temp.Next = _board[i];
                    _board[i] = temp;

                }//close inner for

            }//close outer for
   

        }//close CreateBoard

        /// <summary>
        /// calls create board and sets turn to black
        /// </summary>
        public Game()
        {
            CreateBoard();
            Turn = SquareColor.Black;
        }//close Game

        /// <summary>
        /// returns color of the winner if either 
        /// count = 0
        /// </summary>
        /// <returns></returns>
        public SquareColor CheckWin()
        {
            if (_redCount == 0)
            {
                return SquareColor.Red;
            }
            else if(_blackCount == 0)
            {
                return SquareColor.Black;
            }
            else
            {
                return SquareColor.None;
            }
        }//close CheckWin

        /// <summary>
        /// returns LinkedListCell that references 
        /// first boardSquare of given row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public LinkedListCell<BoardSquare> GetRow(int row)
        {
            if (_board.ContainsKey(row)){
                return _board[row];
            }
            else
            {
                return null;
            }
        }//close GetRow

        /// <summary>
        /// finds and returns the BoardSquare that corresponds to
        /// given row and column.If square has same color as the
        /// turn, square is stored in SelectedPiece
        /// </summary>
        /// <param name="row">row parameter</param>
        /// <param name="col">column parameter</param>
        /// <returns></returns>
        private BoardSquare SelectSquare (int row, int col)
        {
            LinkedListCell<BoardSquare> temp = _board[row];
            while(temp.Next != null && temp.Data.Column != col)
            {
                temp = temp.Next;
            }
            if(temp.Data.Color == Turn && temp.Data.Color != SquareColor.None)
            {
                if(SelectedPiece != null)
                {
                    SelectedPiece.Selected = false;
                }
                SelectedPiece = temp.Data;
                SelectedPiece.Selected = true;
            }
            return temp.Data;
        }//close SelectSquare

        /// <summary>
        /// returns if row of cells given by cell contains a piece in the
        /// targetColor in the targetCol. out is set to square in column
        /// </summary>
        /// <param name="cell">given board cell</param>
        /// <param name="targetCol">given column</param>
        /// <param name="targetColor">given color</param>
        /// <param name="result">square returned in given column</param>
        /// <returns></returns>
        private bool CheckCapture(LinkedListCell<BoardSquare> cell, int targetCol, 
            SquareColor targetColor, out BoardSquare result)
        {
            while (cell.Data.Column != targetCol)
            {
                cell = cell.Next;
            }
            result = cell.Data;
            return cell.Data.Color == targetColor;
        }//close CheckCapture

        /// <summary>
        /// returns if row of cells given by cell contains a piece in the
        /// targetColor in the targetCol. out is set to square in column
        /// </summary>
        /// <param name="cell">given board cell</param>
        /// <param name="targetCol">given column</param>
        /// <param name="targetColor">given color</param>
        /// <returns></returns>
        private bool CheckCapture(LinkedListCell<BoardSquare> cell, int targetCol,
            SquareColor targetColor)
        {
            while(cell.Data.Column != targetCol)
            {
                cell = cell.Next;
            }
            return cell.Data.Color == targetColor;
        }//close CheckCapture

        /// <summary>
        /// checks if a jump is possible given location of the enemy
        /// </summary>
        /// <param name="enemyRow">row of location of enenmy</param>
        /// <param name="targetRow">row of location to jump to</param>
        /// <param name="enemyCol">column of location of enenmy</param>
        /// <param name="targetCol">column of location to jump to</param>
        /// <param name="current">current locaiton of piece</param>
        /// <param name="enemy">color of enemy</param>
        /// <returns></returns>
        private bool CheckJump(int enemyRow, int targetRow, int enemyCol, int targetCol,
            BoardSquare current, SquareColor enemy)
        {
            if (enemyRow < 1 || targetRow < 1 || enemyCol < 1 || targetCol < 1 ||
                enemyRow > 8 || targetRow > 8 || enemyCol > 8 || targetCol > 8)
            {
                return false;
            }
            else
                return (CheckCapture(_board[enemyRow], enemyCol, enemy) &&
                    (CheckCapture(_board[targetRow], targetCol, SquareColor.None)));
        }//close CheckJump

        /// <summary>
        /// uses checkJump to see if any jump is possible from
        /// current square, given enemy color
        /// </summary>
        /// <param name="current">current location of piece</param>
        /// <param name="enemy">enemy color</param>
        /// <returns></returns>
        private bool CheckAnyJump(BoardSquare current, SquareColor enemy)
        {
            //king, red, black 2 minus, 2 plus
            if (current.King == true)
            {
                return (CheckJump(current.Row - 1, current.Row - 2, current.Column - 1, current.Column - 2, current, enemy)) ||
                    (CheckJump(current.Row - 1, current.Row - 2, current.Column + 1, current.Column + 2, current, enemy)) ||
                    (CheckJump(current.Row + 1, current.Row + 2, current.Column - 1, current.Column - 2, current, enemy)) ||
                    (CheckJump(current.Row + 1, current.Row + 2, current.Column + 1, current.Column + 2, current, enemy));
            }
            else if(enemy == SquareColor.Red)
            {
                return (CheckJump(current.Row - 1, current.Row - 2, current.Column + 1, current.Column + 2, current, enemy)
                    || CheckJump(current.Row - 1, current.Row - 2, current.Column - 1, current.Column - 2, current, enemy));
            }
            else
                return (CheckJump(current.Row + 1, current.Row + 2, current.Column + 1, current.Column + 2, current, enemy)
                    || CheckJump(current.Row + 1, current.Row + 2, current.Column - 1, current.Column - 2, current, enemy));

        }//close CheckAnyJump

        /// <summary>
        /// makes the piece jump if there is a valid square to jump to
        /// </summary>
        /// <param name="enemyRow">row of location of enenmy</param>
        /// <param name="targetRow">row of location to jump to</param>
        /// <param name="enemyCol">column of location of enenmy</param>
        /// <param name="targetCol">column of location to jump to</param>
        /// <param name="current">current locaiton of piece</param>
        /// <param name="enemy">color of enemy</param>
        /// <param name="jumpMore">result of CheckAnyJump to find any more valid jumps</param>
        /// <returns></returns>
     private bool MakeJump(int enemyRow, int targetRow, int enemyCol, int targetCol,
            BoardSquare current, SquareColor enemy, out bool jumpMore)
        {
            if (enemyRow < 1 || targetRow < 1 || enemyCol < 1 || targetCol < 1 ||
               enemyRow > 8 || targetRow > 8 || enemyCol > 8 || targetCol > 8)
            {
                jumpMore = false;
                return false;
            }
            BoardSquare targetTemp;
            BoardSquare enemyTemp;

            if(CheckCapture(_board[enemyRow], enemyCol, enemy, out enemyTemp) && 
                CheckCapture(_board[targetRow], targetCol, SquareColor.None, out targetTemp))
            {
          
                if (enemyTemp.Color == SquareColor.Red)
                {
                    _redCount--;
                }
                else
                {
                    _blackCount--;
                }
                enemyTemp.Color = SquareColor.None;
                targetTemp.King = current.King;
                jumpMore = (CheckAnyJump(targetTemp, enemy));
                return true;
            }
            jumpMore = false;
            return false;

        }// close MakeJump 

        /// <summary>
        /// returns true if the piece at current BoardSquare 
        /// can jump over an enemy to the target square
        /// </summary>
        /// <param name="current">current location of piece</param>
        /// <param name="target">target square to jump to</param>
        /// <param name="enemy">color of enemy</param>
        /// <param name="jumpMore">returns if piece can jump again</param>
        /// <returns></returns>
        private bool Jump(BoardSquare current, BoardSquare target, 
            SquareColor enemy, out bool jumpMore)
        {
            int rowNum = 0;
            int colNum = 0;
            if (current.King)
            {
                if(current.Row - target.Row == 2)
                {
                    rowNum = current.Row - 1;
                }
                else
                {
                    rowNum = current.Row + 1;
                }
            }//close if
            else if(Math.Abs(current.Row - target.Row) == 2)
            {
                if(enemy == SquareColor.Red)
                {
                    rowNum = current.Row - 1;
                }
                else
                {
                    rowNum = current.Row + 1;
                }
            }//close else if
            else
            {
                jumpMore = true;
                return false;
            }//close else

            if(current.Column - target.Column == -2)
            {
                colNum = current.Column + 1;
            }
            else if(current.Column - target.Column == 2)
            {
                colNum = current.Column - 1;
            }
            else
            {
                jumpMore = true;
                return false;
            }
            return MakeJump(rowNum, target.Row, colNum, target.Column, current, enemy, out jumpMore);

        }//close Jump

        /// <summary>
        /// attempts to make a legal move in the target row/col 
        /// returns true if a move is made
        /// </summary>
        /// <param name="targetRow">row of location to move to</param>
        /// <param name="targetCol">column of location to move to</param>
        /// <returns></returns>
        public bool MakeMove(int targetRow, int targetCol)
        {
            BoardSquare target = SelectSquare(targetRow, targetCol);
            if (target == null || SelectedPiece == null)
            {
                return false;
            }
            else if (target == SelectedPiece)
            {
                return true;
            }

            if (target.Color == SquareColor.None)
            {
                SquareColor col = SelectedPiece.Color == SquareColor.Black ? SquareColor.Red : SquareColor.Black;
                bool canJump = CheckAnyJump(SelectedPiece, col);

                if (canJump == false)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        LinkedListCell<BoardSquare> temp = _board[i];
                        while (temp != null)
                        {
                            if (temp.Data.Color == Turn)
                            {
                                bool j = CheckAnyJump(temp.Data, col);
                                if (j)
                                {
                                    return false;
                                }
                            }
                            temp = temp.Next;
                        }

                    }
                }//close if

                int num = 0;
                if (SelectedPiece.Color == SquareColor.Black)
                {
                    num = -1;
                }
                else
                {
                    num = 1;
                }
                if ((canJump && Jump(SelectedPiece, target, col, out bool jumpAnother)) ||
                    (!canJump && target.Row - SelectedPiece.Row == num &&
                        Math.Abs(target.Column - SelectedPiece.Column) == 1 ||
                    (!canJump && SelectedPiece.King && Math.Abs(target.Row - SelectedPiece.Row) == 1
                        && (Math.Abs(target.Column - SelectedPiece.Column) == 1))))
                {
                    target.Color = SelectedPiece.Color;
                    if ((target.Row == 1 && SelectedPiece.Color == SquareColor.Black) ||
                        (target.Row == 8 && SelectedPiece.Color == SquareColor.Red))
                    {
                        target.King = true;
                    }
                    else
                    {
                        target.King = SelectedPiece.King;
                    }
                    if (!canJump)
                    {
                        if (SelectedPiece.Color == SquareColor.Red)
                        {
                            Turn = SquareColor.Black;
                        }
                        else
                        {
                            Turn = SquareColor.Red;
                        }
                    }//close if !canjump

                    SelectedPiece.Selected = false;
                    SelectedPiece.Color = SquareColor.None;
                    return true;
                }
            }
            return false;
            
            
        }//close MakeMove
    }
}
