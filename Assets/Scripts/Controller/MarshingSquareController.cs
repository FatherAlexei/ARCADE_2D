using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Arcade
{
    public class MarshingSquareController
    {
        private SquareGrid _squareGrid;
        private Tilemap _tilemap;
        private Tile _tile;

        public void GenerateGrid(int[,] map, float squareSize)
        {
            _squareGrid = new SquareGrid(map, squareSize);
        }

        public void DrawTile(bool active, Vector3 position)
        {
            if (active)
            {
                Vector3Int tilePosition = new Vector3Int((int)position.x, (int)position.y, 0);
                _tilemap.SetTile(tilePosition, _tile);
            }
        }

        public void DrawTiles(Tilemap tilemapGround, Tile groundTile)
        {
            if(_squareGrid == null)
            {
                return;
            }

            _tile = groundTile;
            _tilemap = tilemapGround;

            for (int x = 0; x < _squareGrid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squareGrid.Squares.GetLength(1); y++)
                {
                    DrawTile(_squareGrid.Squares[x, y].TL.Active, _squareGrid.Squares[x, y].TL.position);
                    DrawTile(_squareGrid.Squares[x, y].TR.Active, _squareGrid.Squares[x, y].TR.position);
                    DrawTile(_squareGrid.Squares[x, y].BL.Active, _squareGrid.Squares[x, y].BL.position);
                    DrawTile(_squareGrid.Squares[x, y].BR.Active, _squareGrid.Squares[x, y].BR.position);
                }
            }

        }
    }



    public class Node
    {
        public Vector3 position;
        public Node(Vector3 pos)
        {
            position = pos;
        }
    }

    public class ControlNode : Node
    {
        public bool Active;

        public ControlNode(Vector3 pos, bool active) : base(pos)
        {
            Active = active;
        }
    }

    public class Square
    {
        public ControlNode TL, TR, BL, BR;

        public Square(ControlNode tl, ControlNode tr, ControlNode bl, ControlNode br)
        {
            TL = tl;
            TR = tr;
            BL = bl;
            BR = br;
        }
    }

    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(0);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            float size = squareSize / 2;

            float width = -mapWidth / 2;
            float height = -mapHeight / 2;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for(int x =0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 position = new Vector3(width+x*squareSize+size, height+y*squareSize+size,0);
                    controlNodes[x, y] = new ControlNode(position, map[x,y]==1);
                }
            }

            Squares = new Square[nodeCountX - 1, nodeCountY - 1];
            for (int x = 0; x < nodeCountX-1; x++)
            {
                for (int y = 0; y < nodeCountY-1; y++)
                {
                    Squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x+1,y+1], controlNodes[x+1,y], controlNodes[x,y]);
                }
            }
        }
    }

}