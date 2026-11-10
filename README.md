# ZX Spectrum Studio & Level Editor (C#)

Applicazione grafica completa in C# per Windows per la progettazione, il disegno, l'animazione di **sprite software**, la creazione di **Tile/Tileset** e l'editing completo di **Mappe e Livelli per Sinclair ZX Spectrum** (processore Z80), con supporto nativo specializzato per:
* **Manic Miner** (formato autentico da 1024 byte - specifica SkoolKit 45056 / `$B000`)
* **Jet Set Willy** (stanze, guardiani, item e layout JSW)
* **Sprite Software e Nemici Generici** (16x16 pixel con maschera, player di animazione ed esportazione Assembly)

---

## 🚀 Come Avviare l'Applicazione

L'eseguibile compilato e pronto all'uso si trova in:
```
bin\Release\ZXSpectrumSpriteGenerator.exe
```

Puoi aprirlo direttamente facendo doppio clic su `ZXSpectrumSpriteGenerator.exe`, oppure da terminale PowerShell:
```powershell
.\bin\Release\ZXSpectrumSpriteGenerator.exe
```

Il progetto è compatibile con Visual Studio 2019 / 2022 (`SpriteGenerator.sln`) e compilabile tramite MSBuild.

---

## ⛏️ Modalità Manic Miner (Specifiche SkoolKit 45056)

L'applicazione include un modulo dedicato per progettare ed esportare le caverne di **Manic Miner** rispettando al 100% la struttura di memoria originale a partire dall'indirizzo **45056 ($B000)**:

### 1. Struttura del Blocco Caverna (1024 Byte)
* **0..511 (512 byte)**: Layout dello schermo in griglia 32x16 celle (offset $0000..$01FF).
* **512..543 (32 byte)**: Nome della caverna in formato ASCII (es. `Central Cavern`).
* **544..615 (72 byte)**: Definizioni degli 8 Tile standard (1 byte attributo + 8 byte bitmap per ciascuno):
  1. `[Background]`
  2. `[Floor]`
  3. `[Crumbling Floor]`
  4. `[Wall]`
  5. `[Conveyor]`
  6. `[Nasty 1]`
  7. `[Nasty 2]`
  8. `[Spare / Extra]`
* **616..622 (7 byte)**: Stato iniziale e posizione di Miner Willy (X, Y, animazione, direzione, indirizzo nel buffer attributi).
* **623..626 (4 byte)**: Nastro Trasportatore (Conveyor: direzione, indirizzo a video, lunghezza in celle).
* **627 (1 byte)**: Colore del bordo dello Spectrum (0..7).
* **629..653 (25 byte)**: Fino a 5 Oggetti / Chiavi collezionabili (5 byte ciascuno: attributo, indirizzo attributi, indirizzo video).
* **654..690 (37 byte)**: Portale d'uscita (Exit Portal 16x16, attributo, grafica a 32 byte e indirizzi a video).
* **691..698 (8 byte)**: Grafica bitmap dell'oggetto collezionabile.
* **699..700 (2 byte)**: Riserva iniziale d'aria (Air Supply) e clock.
* **701..728 (28 byte)**: Fino a 4 Guardiani Orizzontali (7 byte ciascuno con range di pattuglia MinX..MaxX, velocità e animazione).
* **734..765 (32 byte)**: Grafica Pesce Spada (The Final Barrier).
* **766..1021 (256 byte)**: 8 Sprite dei Guardiani (16x16 pixel, 32 byte ciascuno).

### 2. Strumenti Interattivi per Manic Miner
Nel tab **Mappe / Livelli** sono presenti pulsanti e scorciatoie dedicate:
* ⛏️ **Menu `Manic Miner`**:
  * *Attiva/Disattiva Modalità Manic Miner*: commuta l'editor abilitando overlay e controlli specifici.
  * *Nuova Caverna Manic Miner*: inizializza una nuova stanza con i parametri predefiniti di Central Cavern.
  * *📋 Incolla 512 Byte Layout Caverna dagli Appunti*: incolla istantaneamente i 512 byte (SkoolKit DEFB, hex o numeri) con auto-mappatura automatica per attributi ZX Spectrum!
  * *Importa Caverna (.BIN, .SNA o .ASM 512B)*: carica un singolo blocco da 1024 byte, estrae da snapshot `.sna` o importa da sorgenti Assembly / testo `.asm`/`.txt`.
  * *Esporta Caverna (1024 Byte .bin)*: genera il file binario puro pronto per injector o SkoolKit.
  * *Esporta Tutte le 20 Caverne (20.480 Byte)*: genera il blocco completo dei 20 livelli da iniettare da $B000 a $FFFF.
  * *Inietta Caverna Corrente in Snapshot (.sna)*: patcha direttamente un file `.sna` 48K esistente all'indirizzo $B000 (offset file 28.699).
  * *Esporta Codice Assembly (SkoolKit DEFB / DEFM)*: genera sorgente Z80 compatibile con SkoolKit con etichette ed espressioni commentate.
  * *Proprietà Caverna Manic Miner*: finestra di dialogo per modificare nome caverna, indice (0..19), colore del bordo, aria iniziale, Willy, Conveyor e Portale.
* 🕹️ **Strumenti sulla Barra del Canvas**:
  * 🚶 **Piazza Willy**: clicca sulla mappa per impostare le coordinate di partenza di Miner Willy.
  * 🚪 **Piazza Portale**: clicca per posizionare il portale d'uscita 16x16.
  * 🔑 **Piazza Item / Chiave**: clicca per posizionare o riposizionare fino a 5 oggetti collezionabili.
  * 👾 **Piazza Guardiano**: clicca per posizionare e configurare i guardiani orizzontali.

### 3. Progetto Precaricato
Nel menu principale **File -> 🕹️ Nuovo Progetto Manic Miner (Central Cavern)** oppure aprendo:
```
Progetti\ManicMiner.zxproj
```
è possibile visualizzare e modificare immediatamente il primo livello storico del gioco (*Central Cavern*), completo di Willy a coordinate (2, 13), 5 chiavi posizionate, guardiano orizzontale di pattuglia e nastro trasportatore!

---

## 🎨 Caratteristiche Principali (Sprite & Mappe)

### 1. Canvas di Disegno Pixel-Art
* **Griglia interattiva** con zoom personalizzabile (+ / -).
* **Evidenziazione delle celle 8x8 ZX Spectrum**: linee guida tratteggiate ciano che mostrano i confini delle celle di carattere dello Spectrum.
* **Tasto Sinistro**: Disegna pixel (Ink).
* **Tasto Destro**: Cancella pixel (Paper).
* **Strumenti disponibili**:
  * **Matita (Pencil)**
  * **Linea (Line)** (algoritmo di Bresenham)
  * **Rettangolo Vuoto / Pieno**
  * **Cerchio Vuoto / Pieno**
  * **Secchiello (Flood Fill)**
* **Undo / Redo completi** (`Ctrl+Z`, `Ctrl+Y`).
* **Trasformazioni rapide**:
  * Specchia Orizzontale (`Flip H`)
  * Specchia Verticale (`Flip V`)
  * Ruota 90° Orario / Antiorario
  * Spostamento pixel a scorrimento (tasti freccia o pulsanti ▲ ▼ ◄ ►)
  * Inversione colori NOT (`Ctrl+I`)

### 2. Gestione Multi-Sprite 
* **Striscia orizzontale (Filmstrip)**: anteprima in miniatura di tutti gli sprite/frame con selezione immediata.
* **Pulsante `+ 1 Sprite`**: aggiunge un singolo sprite vuoto.
* **Pulsante `➕ Aggiungi Sprite`**: apre una finestra guidata con 3 modalità:
  1. **Libreria Preset Sprite ZX Spectrum**: permette di aggiungere Sprite completi pronti all'uso con 4 frame direzionali/animati:
     - *Nemico Tipo 1: Tank / Ricognitore (4 direzioni)*
     - *Nemico Tipo 2: Drone Alieno / Invader (4 frame rotazione)*
     - *Nemico Tipo 3: Pipistrello delle Caverne (4 frame battito ali)*
     - *Nemico Tipo 4: Ragno Cybernetico (4 direzioni)*
  2. **Nuovo Set Nemico (N Frame)**: genera al volo un blocco di N sprite vuoti con prefisso personalizzato e suffisso a 4 direzioni (`_Nord, _Est, _Sud, _Ovest`) o numerico (`_0, _1, _2...`).
  3. **Incolla Dati Assembly (`db`/`defb`)**: incolla liste di byte per creare automaticamente più sprite con rilevamento del numero di frame.
* **Pulsante `📋 Incolla da Appunti`**: scorciatoia con un clic per importare istantaneamente sequenze `db` di altri Sprite dagli appunti di Windows.
* **Ridenominazione immediata**: casella di testo dedicata nel pannello destro per rinominare lo sprite corrente (aggiorna la filmstrip e l'etichetta assembly in tempo reale).

### 3. Palette Ufficiale Sinclair ZX Spectrum
* 8 colori standard (Black, Blue, Red, Magenta, Green, Cyan, Yellow, White) e varianti ad alta luminosità (**Bright**).
* Selettori dedicati per **Ink** e **Paper**.
* Anteprima CRT 1x in scala reale (16x16 pixel autentici) e zoom 4x.

### 4. Player Animazione in Tempo Reale
* Pulsante **▶ Avvia / Ferma Animazione**.
* Slider per regolare la velocità di riproduzione (**1 - 20 FPS**).
* Visualizzazione istantanea del ciclo di animazione tra i 4 frame del nemico.

### 5. Salvataggio & Riapertura Progetto (.zxproj)
* **Salva Progetto (`Ctrl+S`)**: Salva tutte le modifiche apportate (sprite, colori, dimensioni, ordine frame). Se il file è già stato salvato in precedenza, sovrascrive direttamente; altrimenti apre la finestra di dialogo.
* **Salva Progetto con Nome (`Ctrl+Shift+S`)**: Permette di salvare con un nuovo nome o percorso.
* **Apri Progetto (`Ctrl+O`)**: Riapre istantaneamente qualsiasi file `.zxproj` precedentemente salvato.
* **Rilevamento modifiche non salvate**: Il titolo della finestra include un asterisco (`*`) in presenza di modifiche; in caso di chiusura dell'applicazione o apertura di un nuovo progetto, viene richiesta la conferma di salvataggio.
* **Formato aperto JSON**: I file `.zxproj` sono in formato JSON chiaro e strutturato, facilmente ispezionabili e condivisibili.

### 6. Esportazione & Importazione Avanzata
* **Assembly Z80**:
  * Formato numerico a scelta: Decimale (`db 0,0,0,0,42,170...`), Esadecimale (`$48` o `0x48`), Binario (`%00101011`).
  * Direttive configurabili: `db`, `defb`, `DEFB`, `.byte`.
  * Generazione opzionale di **Maschera (Mask)**:
    * Interleaved Sprite + Mask (`db sprite, mask`)
    * Interleaved Mask + Sprite (`db mask, sprite`)
    * Sequenziale (blocco sprite seguito da blocco maschera per istruzioni `AND`/`OR` Z80).
* **Linguaggio C (Z88DK / SDCC)**: array di byte formattati in esadecimale con puntatori ai frame.
* **Sinclair BASIC (DATA)**: dichiarazioni `DATA` con loader BASIC automatico (`POKE 60000+I, B`).
* **File Binario (.bin)**: salvataggio o caricamento diretto dei byte grezzi.
* **Spritesheet PNG**: esportazione grafica in scala.
* **Pulsante di Copia Rapida**: copia l'assembly dello sprite corrente con un solo clic!

---

## 👾 Dati del Nemico di Esempio (16x16 Pixel)

I dati Assembly forniti generano i 4 sprite direzionali (128 byte totali, 32 byte ciascuno):

```assembly
; Posizione 1 (Nord / Retro)
db 0,0,0,0,42,170,127,255,127,255,124,63,124,63,127,255
db 127,255,127,255,0,0,42,170,0,0,42,170,0,0,0,0

; Posizione 2 (Est / Diagonale Dx)
db 0,72,1,44,4,188,18,254,75,254,46,127,188,63,254,124
db 255,242,127,201,127,37,60,148,50,80,9,64,5,0,4,0

; Posizione 3 (Sud / Fronte)
db 0,0,7,224,15,240,23,232,15,240,22,104,14,112,22,104
db 15,240,23,232,15,240,23,232,8,16,23,232,16,8,0,0

; Posizione 4 (Ovest / Diagonale Sx)
db 18,0,52,128,61,32,127,72,127,210,254,116,252,61,62,127
db 79,255,147,254,164,254,41,60,10,76,2,144,0,160,0,32
```

---

## ⌨️ Scorciatoie da Tastiera

| Tasto | Azione |
| :--- | :--- |
| `Ctrl + N` | Nuovo Progetto |
| `Ctrl + O` | Apri Progetto salvato (`.zxproj`) |
| `Ctrl + S` | Salva Progetto |
| `Ctrl + Shift + S` | Salva Progetto con Nome |
| `B` | Seleziona strumento **Matita (Pencil)** |
| `L` | Seleziona strumento **Linea** |
| `R` | Seleziona strumento **Rettangolo** |
| `F` | Seleziona strumento **Secchiello (Fill)** |
| `Ctrl + Z` | Annulla (Undo) |
| `Ctrl + Y` | Ripeti (Redo) |
| `Ctrl + I` | Inverti colori (NOT) |
| `Ctrl + E` | Apri finestra Esportazione Codice |
| `Ctrl + C` | Copia dati Assembly sprite negli appunti |
| `Alt + Freccia` | Sposta sprite (Sinistra/Destra/Su/Giù) |
