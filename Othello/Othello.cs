namespace Otello
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Othello
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

                        if (pieceOfMyColorOnSameDiagonal != null)
                        {
                            _gameState[pieceOfMyColorOnSameDiagonal.X, pieceOfMyColorOnSameDiagonal.Y] = CaseState.Available;
                        }
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
            var position = new Position { X = currentPosition.X, Y = currentPosition.Y };
            return CalculateAvailablePosition(Direction.Vertical, currentPosition.Y, position, position.Y);
        }

        private Position SearchForMyColorOnSameLine(Position currentPosition)
        {
            var position = new Position {X = currentPosition.X, Y = currentPosition.Y};
            return CalculateAvailablePosition(Direction.Horizontal, currentPosition.X, position, position.X);
        }

        private Position CalculateAvailablePosition(Direction direction, int currentPositionCoordToStudy, Position position, int coordToStudy)
        {
            var gameState = _gameState;

            bool isPieceFound = false;

            while (coordToStudy < 7)
            {
                coordToStudy += 1;
                var state = direction == Direction.Horizontal ? gameState[coordToStudy, position.Y] : gameState[position.X, coordToStudy];

                if (state == _currentPlayerColor)
                {
                    isPieceFound = true;
                    break;
                }
            }

            if (!isPieceFound)
            {
                while (coordToStudy > 0)
                {
                    coordToStudy -= 1;
                    var state = direction == Direction.Horizontal ? gameState[coordToStudy, position.Y] : gameState[position.X, coordToStudy];

                    if (state == _currentPlayerColor)
                    {
                        isPieceFound = true;
                        break;
                    }
                }
            }

            // cherche dans l'autre direction une case vide et si trouve => free
            // search free left
            if (isPieceFound && coordToStudy > currentPositionCoordToStudy)
            {
                while (coordToStudy > 0)
                {
                    coordToStudy -= 1;
                    var state = direction == Direction.Horizontal ? gameState[coordToStudy, position.Y] : gameState[position.X, coordToStudy];

                    if (state == CaseState.Free)
                    {
                        break;
                    }
                }
            }
            else if (isPieceFound && coordToStudy < currentPositionCoordToStudy)
            {
                while (coordToStudy < 7)
                {
                    coordToStudy += 1;
                    var state = direction == Direction.Horizontal ? gameState[coordToStudy, position.Y] : gameState[position.X, coordToStudy];

                    if (state == CaseState.Free)
                    {
                        break;
                    }
                }
            }

            // return cette position
            if (direction == Direction.Horizontal)
            {
                position.X = coordToStudy;
            }
            else
            {
                position.Y = coordToStudy;
            }

            return position;
        }
    }
}
