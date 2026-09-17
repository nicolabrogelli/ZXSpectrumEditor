using System;
using System.Collections.Generic;

namespace ZXSpectrumSpriteGenerator.Models
{
    /// <summary>
    /// Rappresenta una mappa di gioco per Sinclair ZX Spectrum.
    /// Composta da una griglia di tile (larghezza x altezza).
    /// Tile 0 = Vuoto / Sfondo nero.
    /// Tile 1..N = Sprite del progetto corrispondente a project.Sprites[tileId - 1].
    /// </summary>
    public class GameMap
    {
        public string Name { get; set; } = "Mappa 1";
        public int Width { get; set; } = 16;  // Larghezza in tile (default 16 per schermo 256px ZX Spectrum con tile 16x16)
        public int Height { get; set; } = 12; // Altezza in tile (default 12 per schermo 192px ZX Spectrum con tile 16x16)
        public int TileWidth { get; set; } = 16;
        public int TileHeight { get; set; } = 16;

        // Colori predefiniti ZX Spectrum per lo sfondo della mappa
        public int DefaultPaper { get; set; } = 0; // Black
        public int DefaultInk { get; set; } = 7;   // White
        public bool Bright { get; set; } = true;

        // Griglia di tile bidimensionale [x, y]
        public int[,] Tiles { get; private set; }

        // Stack per la gestione Undo / Redo
        private readonly Stack<int[,]> _undoStack = new Stack<int[,]>();
        private readonly Stack<int[,]> _redoStack = new Stack<int[,]>();

        public GameMap(string name = "Mappa 1", int width = 16, int height = 12)
        {
            Name = name;
            Width = Math.Max(1, width);
            Height = Math.Max(1, height);
            Tiles = new int[Width, Height];
        }

        public int GetTile(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return Tiles[x, y];
            return 0;
        }

        public void SetTile(int x, int y, int tileId)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                Tiles[x, y] = tileId;
            }
        }

        public void PushUndoState()
        {
            _undoStack.Push(CloneTiles(Tiles, Width, Height));
            if (_undoStack.Count > 30)
            {
                var list = new List<int[,]>(_undoStack);
                list.RemoveAt(list.Count - 1);
                _undoStack.Clear();
                for (int i = list.Count - 1; i >= 0; i--) _undoStack.Push(list[i]);
            }
            _redoStack.Clear();
        }

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public bool Undo()
        {
            if (_undoStack.Count == 0) return false;
            _redoStack.Push(CloneTiles(Tiles, Width, Height));
            Tiles = _undoStack.Pop();
            return true;
        }

        public bool Redo()
        {
            if (_redoStack.Count == 0) return false;
            _undoStack.Push(CloneTiles(Tiles, Width, Height));
            Tiles = _redoStack.Pop();
            return true;
        }

        public void Clear(int fillTile = 0)
        {
            PushUndoState();
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Tiles[x, y] = fillTile;
        }

        public void Resize(int newWidth, int newHeight, int fillTile = 0)
        {
            if (newWidth <= 0 || newHeight <= 0) return;
            PushUndoState();
            int[,] newTiles = new int[newWidth, newHeight];
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    if (x < Width && y < Height)
                        newTiles[x, y] = Tiles[x, y];
                    else
                        newTiles[x, y] = fillTile;
                }
            }
            Width = newWidth;
            Height = newHeight;
            Tiles = newTiles;
        }

        public GameMap Clone()
        {
            var clone = new GameMap(Name, Width, Height)
            {
                TileWidth = this.TileWidth,
                TileHeight = this.TileHeight,
                DefaultPaper = this.DefaultPaper,
                DefaultInk = this.DefaultInk,
                Bright = this.Bright
            };
            clone.Tiles = CloneTiles(this.Tiles, Width, Height);
            return clone;
        }

        private static int[,] CloneTiles(int[,] src, int w, int h)
        {
            int[,] copy = new int[w, h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    copy[x, y] = src[x, y];
            return copy;
        }

        public byte[] ToByteArray()
        {
            byte[] bytes = new byte[Width * Height];
            int idx = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    bytes[idx++] = (byte)(Tiles[x, y] & 0xFF);
                }
            }
            return bytes;
        }

        public void FromByteArray(byte[] data, int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new int[Width, Height];
            int idx = 0;
            for (int y = 0; y < Height && idx < data.Length; y++)
            {
                for (int x = 0; x < Width && idx < data.Length; x++)
                {
                    Tiles[x, y] = data[idx++];
                }
            }
        }
    }
}
