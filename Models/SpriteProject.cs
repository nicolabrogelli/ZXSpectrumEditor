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

            // Crea mappa iniziale di esempio (Schermo 16x12 tile per ZX Spectrum 256x192 px)
            var defaultMap = project.AddMap("Schermo 1 - Ingresso", 16, 12);
            // Muri perimetrali
            for (int x = 0; x < 16; x++)
            {
                defaultMap.SetTile(x, 0, 1);
                defaultMap.SetTile(x, 11, 1);
            }
            for (int y = 0; y < 12; y++)
            {
                defaultMap.SetTile(0, y, 1);
                defaultMap.SetTile(15, y, 1);
            }
            // Piattaforme ed elementi stanza
            for (int x = 3; x <= 6; x++) defaultMap.SetTile(x, 8, 3);
            defaultMap.SetTile(4, 7, 2); // Nemico sulla piattaforma 1

            for (int x = 9; x <= 12; x++) defaultMap.SetTile(x, 5, 3);
            defaultMap.SetTile(10, 4, 4); // Nemico sulla piattaforma 2

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

        // Metodi Gestione Mappe di Gioco
        public GameMap AddMap(string name = null, int width = 16, int height = 12)
        {
            string mName = name ?? $"Mappa {Maps.Count + 1}";
            var map = new GameMap(mName, width, height);
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
