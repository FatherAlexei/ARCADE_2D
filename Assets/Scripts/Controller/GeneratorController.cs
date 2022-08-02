using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Arcade
{
    public class GeneratorController
    {
        private Tilemap _tilemap;
        private Tile _groundTile;

        private int _mapHeight;
        private int _mapWidth;

        private bool _borders;

        private int _fillPercent;
        private int _factorSmooth;


        private int[,] _map;

        private MarshingSquareController _controller;

        public GeneratorController(GeneratorLevelView view)
        {
            _tilemap = view._tilemap;
            _groundTile = view._tile;
            _mapHeight = view._mapHeight;
            _mapWidth = view._mapWidth;
            _borders = view._borders;
            _fillPercent = view._fillPercent;
            _factorSmooth = view._factorSmooth;

            _map = new int[_mapWidth, _mapHeight];
        }

        public void Start()
        {
            FillMap();
            for (int i = 0; i < _factorSmooth; i++)
            {
                SmoothMap();
            }


            _controller = new MarshingSquareController();
            _controller.GenerateGrid(_map, 1);
            _controller.DrawTiles(_tilemap, _groundTile);

            //DrawTiles();
        }

        public void FillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if(x==0 || x== _mapWidth-1 || y == 0 || y==_mapHeight-1)
                    {
                        if(_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                    }
                }
            }
            int temp = _map[10, 10];
        }

        public void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbour = GetNeighour(x,y);

                    if(neighbour > 4)
                    {
                        _map[x, y] = 1;
                    }
                    else if(neighbour < 4)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }

        private int GetNeighour(int x, int y)
        {
            int neighourCounter = 0;

            for (int gridX = x-1; gridX <= x+1; gridX++)
            {
                for (int gridY = y-1; gridY <= y+1; gridY++)
                {
                    if(gridX >= 0 && gridX < _mapWidth && gridY >= 0 && gridY < _mapHeight)
                    {
                        if(gridX!=x||gridY!=y)
                        {
                            neighourCounter += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        neighourCounter++;
                    }
                }
            }
            return neighourCounter;
        }


        private void DrawTiles()
        {
            if (_map == null)
            {
                return;
            }

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (_map[x, y] == 1)
                    {
                        Vector3Int tilePosition = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);
                        _tilemap.SetTile(tilePosition, _groundTile);
                    }
                }
            }
        }
    }

}