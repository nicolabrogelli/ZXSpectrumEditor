using System;
using System.Collections.Generic;

namespace ZXSpectrumSpriteGenerator.Models
{
    public class EnemyPreset
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int DefaultInk { get; set; } = 7;
        public int DefaultPaper { get; set; } = 0;
        public List<EnemyPresetFrame> Frames { get; set; } = new List<EnemyPresetFrame>();
    }

    public class EnemyPresetFrame
    {
        public string FrameName { get; set; }
        public byte[] Data { get; set; }

        public EnemyPresetFrame(string frameName, byte[] data)
        {
            FrameName = frameName;
            Data = data;
        }
    }

    /// <summary>
    /// Libreria di preset grafici per sprite di nemici su Sinclair ZX Spectrum (16x16 pixel, 32 byte a frame).
    /// </summary>
    public static class EnemyPresets
    {
        public static readonly List<EnemyPreset> AllPresets = new List<EnemyPreset>();

        static EnemyPresets()
        {
            // 1. Nemico 1 - Ricognitore / Tank (Dati del prompt)
            var p1 = new EnemyPreset
            {
                Id = "tank",
                DisplayName = "Nemico Tipo 1: Tank / Ricognitore (4 Direzioni)",
                Description = "4 viste direzionali (Nord, Est, Sud, Ovest) con torretta e cingoli.",
                DefaultInk = 4, // Green
                DefaultPaper = 0,
                Frames = new List<EnemyPresetFrame>
                {
                    new EnemyPresetFrame("Nemico1_Nord", new byte[] {
                        0,0,0,0,42,170,127,255,127,255,124,63,124,63,127,255,
                        127,255,127,255,0,0,42,170,0,0,42,170,0,0,0,0
                    }),
                    new EnemyPresetFrame("Nemico1_Est", new byte[] {
                        0,72,1,44,4,188,18,254,75,254,46,127,188,63,254,124,
                        255,242,127,201,127,37,60,148,50,80,9,64,5,0,4,0
                    }),
                    new EnemyPresetFrame("Nemico1_Sud", new byte[] {
                        0,0,7,224,15,240,23,232,15,240,22,104,14,112,22,104,
                        15,240,23,232,15,240,23,232,8,16,23,232,16,8,0,0
                    }),
                    new EnemyPresetFrame("Nemico1_Ovest", new byte[] {
                        18,0,52,128,61,32,127,72,127,210,254,116,252,61,62,127,
                        79,255,147,254,164,254,41,60,10,76,2,144,0,160,0,32
                    })
                }
            };
            AllPresets.Add(p1);

            // 2. Nemico 2 - Drone Alieno / Invader (4 frame / rotazioni)
            var p2 = new EnemyPreset
            {
                Id = "drone",
                DisplayName = "Nemico Tipo 2: Drone Alieno / Invader (4 Frame)",
                Description = "Drone orbitante a 4 frame con antenne pulsanti e propulsori ionici.",
                DefaultInk = 6, // Yellow
                DefaultPaper = 0,
                Frames = new List<EnemyPresetFrame>
                {
                    new EnemyPresetFrame("Drone_Pos1", new byte[] {
                        0,0, 3,192, 7,224, 15,240, 31,248, 27,216, 31,248, 63,252,
                        127,254, 115,206, 115,206, 99,198, 33,132, 49,140, 96,6, 0,0
                    }),
                    new EnemyPresetFrame("Drone_Pos2", new byte[] {
                        0,0, 1,128, 7,224, 15,240, 27,216, 31,248, 63,252, 127,254,
                        127,254, 115,206, 99,198, 49,140, 33,132, 96,6, 192,3, 0,0
                    }),
                    new EnemyPresetFrame("Drone_Pos3", new byte[] {
                        0,0, 3,192, 7,224, 15,240, 31,248, 63,252, 127,254, 115,206,
                        115,206, 127,254, 63,252, 31,248, 15,240, 18,72, 33,132, 0,0
                    }),
                    new EnemyPresetFrame("Drone_Pos4", new byte[] {
                        0,0, 1,128, 7,224, 15,240, 27,216, 31,248, 63,252, 127,254,
                        127,254, 115,206, 99,198, 49,140, 33,132, 96,6, 192,3, 0,0
                    })
                }
            };
            AllPresets.Add(p2);

            // 3. Nemico 3 - Pipistrello delle Caverne (4 Frame di volo)
            var p3 = new EnemyPreset
            {
                Id = "bat",
                DisplayName = "Nemico Tipo 3: Pipistrello / Volatile (4 Frame Volo)",
                Description = "Classico nemico volante dei giochi platform ZX Spectrum (ali su, aperte, giù, chiuse).",
                DefaultInk = 5, // Cyan
                DefaultPaper = 0,
                Frames = new List<EnemyPresetFrame>
                {
                    // Ali su
                    new EnemyPresetFrame("Pipistrello_AliSu", new byte[] {
                        0,0, 128,1, 192,3, 224,7, 112,14, 56,28, 28,56, 15,240,
                        7,224, 6,96, 6,96, 3,192, 1,128, 0,0, 0,0, 0,0
                    }),
                    // Ali orizzontali
                    new EnemyPresetFrame("Pipistrello_AliOrizz", new byte[] {
                        0,0, 0,0, 0,0, 255,255, 127,254, 63,252, 31,248, 15,240,
                        7,224, 6,96, 7,224, 3,192, 1,128, 0,0, 0,0, 0,0
                    }),
                    // Ali giù
                    new EnemyPresetFrame("Pipistrello_AliGiu", new byte[] {
                        0,0, 0,0, 0,0, 3,192, 7,224, 15,240, 63,252, 127,254,
                        231,231, 198,99, 134,97, 4,32, 0,0, 0,0, 0,0, 0,0
                    }),
                    // Ali chiuse
                    new EnemyPresetFrame("Pipistrello_Chiuso", new byte[] {
                        0,0, 0,0, 3,192, 7,224, 15,240, 10,80, 15,240, 15,240,
                        7,224, 7,224, 3,192, 3,192, 1,128, 1,128, 0,0, 0,0
                    })
                }
            };
            AllPresets.Add(p3);

            // 4. Nemico 4 - Ragno Cybernetico (4 Direzioni)
            var p4 = new EnemyPreset
            {
                Id = "spider",
                DisplayName = "Nemico Tipo 4: Ragno Cybernetico (4 Direzioni)",
                Description = "Aracnide a 8 zampe con zanne e corpo segmentato in 4 direzioni.",
                DefaultInk = 2, // Red
                DefaultPaper = 0,
                Frames = new List<EnemyPresetFrame>
                {
                    // Sud / Fronte
                    new EnemyPresetFrame("Ragno_Sud", new byte[] {
                        0,0, 137,145, 78,114, 56,28, 15,240, 31,248, 27,216, 31,248,
                        15,240, 63,252, 111,246, 97,134, 145,137, 137,145, 0,0, 0,0
                    }),
                    // Est / Destra
                    new EnemyPresetFrame("Ragno_Est", new byte[] {
                        0,0, 3,144, 7,208, 14,160, 31,192, 63,248, 125,188, 125,188,
                        63,248, 31,192, 14,160, 7,208, 3,144, 0,0, 0,0, 0,0
                    }),
                    // Nord / Retro
                    new EnemyPresetFrame("Ragno_Nord", new byte[] {
                        0,0, 137,145, 145,137, 97,134, 111,246, 63,252, 15,240, 31,248,
                        27,216, 31,248, 15,240, 56,28, 78,114, 137,145, 0,0, 0,0
                    }),
                    // Ovest / Sinistra
                    new EnemyPresetFrame("Ragno_Ovest", new byte[] {
                        0,0, 9,192, 11,224, 5,112, 3,248, 31,252, 61,190, 61,190,
                        31,252, 3,248, 5,112, 11,224, 9,192, 0,0, 0,0, 0,0
                    })
                }
            };
            AllPresets.Add(p4);
        }
    }
}
