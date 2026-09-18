using System;
using System.Collections.Generic;

namespace ZXSpectrumSpriteGenerator.Models
{
    /// <summary>
    /// Contenitore per un progetto di sprite per ZX Spectrum.
    /// Contiene la lista di sprite/frame, dimensioni predefinite e impostazioni di visualizzazione.
    /// </summary>
    public class SpriteProject
    {
        public string Title { get; set; } = "Nemico ZX Spectrum (4 Posizioni)";
        public int DefaultWidth { get; set; } = 16;
        public int DefaultHeight { get; set; } = 16;
        public List<Sprite> Sprites { get; private set; } = new List<Sprite>();
        public int SelectedIndex { get; set; } = 0;

        // Gestione Tileset Mappa (Tile 8x8)
        public List<Tile> Tiles { get; private set; } = new List<Tile>();
        public int SelectedTileIndex { get; set; } = 0;

        // Gestione Mappe di Gioco (Tilemaps)
        public List<GameMap> Maps { get; private set; } = new List<GameMap>();
        public int SelectedMapIndex { get; set; } = 0;

        // Dati di esempio predefiniti (i 4 sprite a 16x16 del nemico dell'utente)
        public static readonly byte[][] DefaultEnemyData = new byte[][]
        {
            // Sprite 0 (32 bytes)
            new byte[] {
                0,0,0,0,42,170,127,255,127,255,124,63,124,63,127,255,
                127,255,127,255,0,0,42,170,0,0,42,170,0,0,0,0
            },
            // Sprite 1 (32 bytes)
            new byte[] {
                0,72,1,44,4,188,18,254,75,254,46,127,188,63,254,124,
                255,242,127,201,127,37,60,148,50,80,9,64,5,0,4,0
            },
            // Sprite 2 (32 bytes)
            new byte[] {
                0,0,7,224,15,240,23,232,15,240,22,104,14,112,22,104,
                15,240,23,232,15,240,23,232,8,16,23,232,16,8,0,0
            },
            // Sprite 3 (32 bytes)
            new byte[] {
                18,0,52,128,61,32,127,72,127,210,254,116,252,61,62,127,
                79,255,147,254,164,254,41,60,10,76,2,144,0,160,0,32
            }
        };

        public Sprite CurrentSprite
        {
            get
            {
                if (SelectedIndex >= 0 && SelectedIndex < Sprites.Count)
                    return Sprites[SelectedIndex];
                return Sprites.Count > 0 ? Sprites[0] : null;
            }
        }

        public Tile CurrentTile
        {
            get
            {
                if (SelectedTileIndex >= 0 && SelectedTileIndex < Tiles.Count)
                    return Tiles[SelectedTileIndex];
                return Tiles.Count > 0 ? Tiles[0] : null;
            }
        }

        public GameMap CurrentMap
        {
            get
            {
                if (SelectedMapIndex >= 0 && SelectedMapIndex < Maps.Count)
                    return Maps[SelectedMapIndex];
                return Maps.Count > 0 ? Maps[0] : null;
            }
        }

        public static SpriteProject CreateDefault()
        {
            var project = new SpriteProject();
            string[] names = new string[]
            {
                "Nemico_Pos1_Nord",
                "Nemico_Pos2_Est",
                "Nemico_Pos3_Sud",
                "Nemico_Pos4_Ovest"
            };

            for (int i = 0; i < DefaultEnemyData.Length; i++)
            {
                var s = new Sprite(16, 16, names[i]);
                s.GroupName = "Nemico 1";
                s.SetFromBytesRowByRow(DefaultEnemyData[i]);
                s.InkColor = 7;     // White
                s.PaperColor = 0;   // Black
                s.Bright = true;
                project.Sprites.Add(s);
            }

            project.SelectedIndex = 0;

            // Inizializza Tileset predefinito (Tile 8x8 per la mappa)
            project.Tiles.Add(new Tile("Muro_Mattoni", TilePresets.BrickWall) { InkColor = 7, PaperColor = 0, Bright = true });
            project.Tiles.Add(new Tile("Piattaforma", TilePresets.Platform) { InkColor = 5, PaperColor = 0, Bright = true });
            project.Tiles.Add(new Tile("Scala", TilePresets.Ladder) { InkColor = 6, PaperColor = 0, Bright = true });
            project.Tiles.Add(new Tile("Blocco_Pietra", TilePresets.StoneBlock) { InkColor = 7, PaperColor = 0, Bright = false });
            project.Tiles.Add(new Tile("Spuntoni", TilePresets.Spikes) { InkColor = 2, PaperColor = 0, Bright = true });
            project.Tiles.Add(new Tile("Gemma", TilePresets.Gem) { InkColor = 6, PaperColor = 0, Bright = true });
            project.SelectedTileIndex = 0;

            // Crea mappa iniziale di esempio (Schermo 32x24 tile per ZX Spectrum 256x192 px con tile 8x8)
            var defaultMap = project.AddMap("Schermo 1 - Ingresso", 32, 24);
            // Muri perimetrali (Tile 1: Muro_Mattoni)
            for (int x = 0; x < 32; x++)
            {
                defaultMap.SetTile(x, 0, 1);
                defaultMap.SetTile(x, 23, 1);
            }
            for (int y = 0; y < 24; y++)
            {
                defaultMap.SetTile(0, y, 1);
                defaultMap.SetTile(31, y, 1);
            }
            // Piattaforme (Tile 2: Piattaforma)
            for (int x = 4; x <= 12; x++) defaultMap.SetTile(x, 16, 2);
            for (int x = 18; x <= 26; x++) defaultMap.SetTile(x, 11, 2);
            for (int x = 8; x <= 16; x++) defaultMap.SetTile(x, 7, 2);

            // Scale (Tile 3: Scala)
            for (int y = 11; y <= 15; y++) defaultMap.SetTile(14, y, 3);

            // Spuntoni sul fondo (Tile 5: Spuntoni)
            for (int x = 14; x <= 17; x++) defaultMap.SetTile(x, 22, 5);

            // Gemme / Pick-up (Tile 6: Gemma)
            defaultMap.SetTile(8, 15, 6);
            defaultMap.SetTile(22, 10, 6);
            defaultMap.SetTile(12, 6, 6);

            project.SelectedMapIndex = 0;
            return project;
        }

        public Sprite AddSprite(string name = null)
        {
            string sName = name ?? $"Sprite_{Sprites.Count}";
            var s = new Sprite(DefaultWidth, DefaultHeight, sName);
            if (CurrentSprite != null)
            {
                s.GroupName = CurrentSprite.GroupName;
                s.InkColor = CurrentSprite.InkColor;
                s.PaperColor = CurrentSprite.PaperColor;
                s.Bright = CurrentSprite.Bright;
                s.Flash = CurrentSprite.Flash;
            }
            Sprites.Add(s);
            SelectedIndex = Sprites.Count - 1;
            return s;
        }

        public Sprite DuplicateCurrent()
        {
            if (CurrentSprite == null) return null;
            var clone = CurrentSprite.Clone();
            clone.Name = CurrentSprite.Name + "_copia";
            Sprites.Insert(SelectedIndex + 1, clone);
            SelectedIndex++;
            return clone;
        }

        public bool RemoveCurrent()
        {
            if (Sprites.Count <= 1) return false;
            Sprites.RemoveAt(SelectedIndex);
            if (SelectedIndex >= Sprites.Count)
            {
                SelectedIndex = Sprites.Count - 1;
            }
            return true;
        }

        public bool MoveCurrentUp()
        {
            if (SelectedIndex <= 0) return false;
            var item = Sprites[SelectedIndex];
            Sprites.RemoveAt(SelectedIndex);
            SelectedIndex--;
            Sprites.Insert(SelectedIndex, item);
            return true;
        }

        public bool MoveCurrentDown()
        {
            if (SelectedIndex >= Sprites.Count - 1) return false;
            var item = Sprites[SelectedIndex];
            Sprites.RemoveAt(SelectedIndex);
            SelectedIndex++;
            Sprites.Insert(SelectedIndex, item);
            return true;
        }

        // Metodi Gestione Tileset Mappa (Tile 8x8)
        public Tile AddTile(string name = null, byte[] data = null)
        {
            string tName = name ?? $"Tile_{Tiles.Count + 1}";
            var tile = new Tile(tName, data);
            if (CurrentTile != null && data == null)
            {
                tile.InkColor = CurrentTile.InkColor;
                tile.PaperColor = CurrentTile.PaperColor;
                tile.Bright = CurrentTile.Bright;
                tile.Flash = CurrentTile.Flash;
            }
            Tiles.Add(tile);
            SelectedTileIndex = Tiles.Count - 1;
            return tile;
        }

        public Tile DuplicateCurrentTile()
        {
            if (CurrentTile == null) return null;
            var clone = CurrentTile.Clone();
            clone.Name = CurrentTile.Name + "_copia";
            Tiles.Insert(SelectedTileIndex + 1, clone);
            SelectedTileIndex++;
            return clone;
        }

        public bool RemoveCurrentTile()
        {
            if (Tiles.Count <= 1) return false;
            Tiles.RemoveAt(SelectedTileIndex);
            if (SelectedTileIndex >= Tiles.Count)
            {
                SelectedTileIndex = Tiles.Count - 1;
            }
            return true;
        }

        public bool MoveCurrentTileUp()
        {
            if (SelectedTileIndex <= 0) return false;
            var item = Tiles[SelectedTileIndex];
            Tiles.RemoveAt(SelectedTileIndex);
            SelectedTileIndex--;
            Tiles.Insert(SelectedTileIndex, item);
            return true;
        }

        public bool MoveCurrentTileDown()
        {
            if (SelectedTileIndex >= Tiles.Count - 1) return false;
            var item = Tiles[SelectedTileIndex];
            Tiles.RemoveAt(SelectedTileIndex);
            SelectedTileIndex++;
            Tiles.Insert(SelectedTileIndex, item);
            return true;
        }

        // Metodi Gestione Mappe di Gioco
        public GameMap AddMap(string name = null, int width = 32, int height = 24)
        {
            string mName = name ?? $"Mappa {Maps.Count + 1}";
            var map = new GameMap(mName, width, height)
            {
                TileWidth = 8,
                TileHeight = 8
            };
            Maps.Add(map);
            SelectedMapIndex = Maps.Count - 1;
            return map;
        }

        public GameMap DuplicateCurrentMap()
        {
            if (CurrentMap == null) return null;
            var clone = CurrentMap.Clone();
            clone.Name = CurrentMap.Name + " (Copia)";
            Maps.Insert(SelectedMapIndex + 1, clone);
            SelectedMapIndex++;
            return clone;
        }

        public bool RemoveCurrentMap()
        {
            if (Maps.Count <= 1) return false;
            Maps.RemoveAt(SelectedMapIndex);
            if (SelectedMapIndex >= Maps.Count)
            {
                SelectedMapIndex = Maps.Count - 1;
            }
            return true;
        }
    }
}
