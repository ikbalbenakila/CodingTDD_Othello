using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Otello
{
    class Othello
    {
        private CaseState _currentPlayerColor;
        private CaseState[,] _gameState;

        public Othello(CaseState firstPlayerColor = CaseState.White)
        {
            _currentPlayerColor = firstPlayerColor;
            _gameState = new CaseState[8,8];

            _gameState[4, 4] = CaseState.Black;
            _gameState[4, 5] = CaseState.White;
            _gameState[5, 4] = CaseState.White;
            _gameState[5, 5] = CaseState.Black;
        }

        public CaseState CurrentPlayerColor
        {
            get { return _currentPlayerColor; }
        }

        internal void Play()
        {
            if (_currentPlayerColor == CaseState.Black)
            {
                _currentPlayerColor = CaseState.White;
            }
            else
            {
                _currentPlayerColor = CaseState.Black;
            }
        }

        internal CaseState[,] GetGameState()
        {
            return _gameState;
        }

        internal CaseState[,] GetAvailablePositions()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_gameState[i, j] != CaseState.Free  && _gameState[i, j] != CaseState.Available && _gameState[i, j] != _currentPlayerColor)
                    {
                        Position currentPosition = new Position(){X = i, Y = j};

                        Position pieceOfMyColorOnSameLine = SearchForMyColorOnSameLine(currentPosition);

                        if (pieceOfMyColorOnSameLine != null)
                        {
                            _gameState[pieceOfMyColorOnSameLine.X, pieceOfMyColorOnSameLine.Y] = CaseState.Available;
                        }

                        Position pieceOfMyColorOnSameColumn = SearchForMyColorOnSameColumn(currentPosition);

                        if (pieceOfMyColorOnSameColumn != null)
                        {
                            _gameState[pieceOfMyColorOnSameColumn.X, pieceOfMyColorOnSameColumn.Y] = CaseState.Available;
                        }

                        Position pieceOfMyColorOnSameDiagonal = SearchForMyColorOnSameDiagonal(currentPosition);
                    }
                }
            }

            return _gameState;
        }

        private Position SearchForMyColorOnSameDiagonal(Position currentPosition)
        {
            return null;
        }

        private Position SearchForMyColorOnSameColumn(Position currentPosition)
        {
            // trouve une piece de ma couleur sur la meme ligne.
            var gameState = _gameState;

            var position = new Position() { X = currentPosition.X, Y = currentPosition.Y };
            bool pieceIsFound = false;

            while (position.Y < 7)
            {
                position.Y = position.Y + 1;
                var state = gameState[position.X, position.Y];

                if (state == _currentPlayerColor)
                {
                    pieceIsFound = true;
                    break;
                }
            }

            if (!pieceIsFound)
            {
                while (position.Y > 0)
                {
                    position.Y = position.Y - 1;
                    var state = gameState[position.X, position.Y];

                    if (state == _currentPlayerColor)
                    {
                        pieceIsFound = true;
                        break;
                    }
                } 
            }

            // cherche dans l'autre direction une case vide et si trouve => free
            // search free left
            if (pieceIsFound && position.Y > currentPosition.Y)
            {
                while (position.Y > 0)
                {
                    position.Y = position.Y - 1;
                    var state = gameState[position.X, position.Y];

                    if (state == CaseState.Free)
                    {
                        return position;
                    }
                }

                return null;
            }
            else if (pieceIsFound && position.Y < currentPosition.Y)
            {
                while (position.Y < 7)
                {
                    position.Y = position.Y + 1;
                    var state = gameState[position.X, position.Y];

                    if (state == CaseState.Free)
                    {
                        return position;
                    }
                } 

                return null;
            }

            // return cette position
            return position;
        }

        private Position SearchForMyColorOnSameLine(Position currentPosition)
        {
            // trouve une piece de ma couleur sur la meme ligne.
            var gameState = _gameState;

            var position = new Position() {X = currentPosition.X, Y = currentPosition.Y};
            bool pieceIsFound = false;

            while (position.X < 7)
            {
                position.X = position.X + 1;
                var state = gameState[position.X, position.Y];

                if (state == _currentPlayerColor)
                {
                    pieceIsFound = true;
                    break;
                }
            } 

            if (!pieceIsFound)
            {
                while (position.X > 0)
                {
                    position.X = position.X - 1;
                    var state = gameState[position.X, position.Y];

                    if (state == _currentPlayerColor)
                    {
                        pieceIsFound = true;
                        break;
                    }
                }
            }

            // cherche dans l'autre direction une case vide et si trouve => free
            // search free left
            if (pieceIsFound && position.X > currentPosition.X)
            {
                while (position.X > 0)
                {
                    position.X = position.X - 1;
                    var state = gameState[position.X, position.Y];

                    if (state == CaseState.Free)
                    {
                        return position;
                    }
                }

                return null;
            }
            else if (pieceIsFound && position.X < currentPosition.X)
            {
                while (position.X < 7)
                {
                    position.X = position.X + 1;
                    var state = gameState[position.X, position.Y];

                    if (state == CaseState.Free)
                    {
                        return position;
                    }
                }

                return null;
            }

            // return cette position
            return position;
        }
    }
}
