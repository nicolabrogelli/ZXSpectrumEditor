using System.Collections.Generic;

namespace ZXSpectrumSpriteGenerator.Models
{
    public class TilePreset
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int DefaultInk { get; set; } = 7;
        public int DefaultPaper { get; set; } = 0;
        public bool Bright { get; set; } = true;
        public byte[] Data { get; set; }

        public TilePreset(string id, string displayName, string description, int ink, int paper, bool bright, byte[] data)
        {
            Id = id;
            DisplayName = displayName;
            Description = description;
            DefaultInk = ink;
            DefaultPaper = paper;
            Bright = bright;
            Data = data;
        }
    }

    /// <summary>
    /// Libreria di preset grafici per tile 8x8 pixel autentici Sinclair ZX Spectrum (8 byte ciascuno).
    /// Include elementi classici per piattaforme, dungeon, labirinti e scenari d'azione.
    /// </summary>
    public static class TilePresets
    {
        public static readonly byte[] BrickWall = new byte[] { 255, 130, 130, 255, 32, 32, 255, 0 };
        public static readonly byte[] Platform = new byte[] { 255, 170, 85, 170, 85, 170, 85, 255 };
        public static readonly byte[] Ladder = new byte[] { 130, 254, 130, 130, 254, 130, 130, 254 };
        public static readonly byte[] GrassSoil = new byte[] { 255, 170, 85, 0, 36, 0, 73, 0 };
        public static readonly byte[] StoneBlock = new byte[] { 126, 129, 165, 129, 153, 129, 126, 0 };
        public static readonly byte[] Spikes = new byte[] { 16, 56, 56, 124, 124, 254, 254, 255 };
        public static readonly byte[] Water = new byte[] { 0, 68, 170, 17, 0, 68, 170, 17 };
        public static readonly byte[] Gem = new byte[] { 60, 66, 153, 189, 189, 153, 66, 60 };
        public static readonly byte[] Door = new byte[] { 60, 126, 255, 219, 219, 219, 219, 219 };
        public static readonly byte[] Pipe = new byte[] { 255, 129, 129, 129, 129, 129, 129, 255 };
        public static readonly byte[] Crate = new byte[] { 255, 129, 189, 165, 165, 189, 129, 255 };
        public static readonly byte[] Torch = new byte[] { 16, 40, 56, 16, 16, 56, 16, 16 };

        public static readonly List<TilePreset> AllPresets = new List<TilePreset>();

        static TilePresets()
        {
            AllPresets.Add(new TilePreset("brick_wall", "Muro di Mattoni", "Parete solida per delimitare stanze o ostacoli.", 7, 0, true, BrickWall));
            AllPresets.Add(new TilePreset("platform", "Piattaforma Metallica", "Superficie a griglia calpestabile per giochi platform.", 5, 0, true, Platform));
            AllPresets.Add(new TilePreset("ladder", "Scala a Pioli", "Scala verticale per arrampicata del protagonista.", 6, 0, true, Ladder));
            AllPresets.Add(new TilePreset("grass_soil", "Terreno con Erba", "Blocco di terra con ciuffi d'erba in cima.", 4, 0, true, GrassSoil));
            AllPresets.Add(new TilePreset("stone_block", "Blocco di Pietra", "Mattonella in pietra lavorata squadrata.", 7, 0, false, StoneBlock));
            AllPresets.Add(new TilePreset("spikes", "Spuntoni / Hazard", "Trappola letale appuntita sul pavimento o soffitto.", 2, 0, true, Spikes));
            AllPresets.Add(new TilePreset("water", "Acqua / Fluido", "Onde decorative per laghi, canali o lava.", 5, 0, true, Water));
            AllPresets.Add(new TilePreset("gem", "Gemma / Pick-up", "Oggetto da raccogliere per accumulare punteggio o sbloccare porte.", 6, 0, true, Gem));
            AllPresets.Add(new TilePreset("door", "Porta / Portale", "Uscita del livello o passaggio verso un'altra stanza.", 7, 0, true, Door));
            AllPresets.Add(new TilePreset("pipe", "Tubo / Pilastro", "Condotto industriale o colonna di sostegno.", 3, 0, true, Pipe));
            AllPresets.Add(new TilePreset("crate", "Cassa di Legno", "Contenitore o blocco spingibile in legno.", 6, 0, false, Crate));
            AllPresets.Add(new TilePreset("torch", "Torcia a Muro", "Punto luce e decorazione per pareti di castelli e caverne.", 6, 0, true, Torch));
        }
    }
}
