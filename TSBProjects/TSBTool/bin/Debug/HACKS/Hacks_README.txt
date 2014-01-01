========================== HACKS README ===========================
This folder contains some hacks. The hacks in this folder were taken 
from ROMS found in the message boards at tecmobowl.org.

These hacks were mostly made by using the windows command line utility
'fc' and a text editor like folows:

At Windows command line:
C:\RomDirectory> FC /B hacked_rom.nes OriginalTecmoROM.nes > hack.txt

Open 'hack.txt' with text editor (I use notepad++)[http://notepad-plus-plus.org/]
This will give output like:
===================================================================
Comparing files hacked_rom.nes and OriginalTecmoROM.nes 
00000006: 42 40
0002912D: 50 40
0002B2C1: E0 AA
0002B2C2: BF DD
0002B2C3: AA A8
0002BFF0: 20 FF
0002BFF1: AA FF
0002BFF2: DD FF
0002BFF3: AA FF
0002BFF4: E0 FF
===================================================================
Then I used a text editor (notepad++, Search Mode=Regular expression )
(type Ctrl+H to bring up the replace dialog) and make these 
replacements (the space matters!):
': '            with   ',0x'
' [0-9A-F]+$'   with   ')'
'^000'          with   'SET(0x'
===================================================================
Which gives us the output:

Comparing files hacked_rom.nes and OriginalTecmoROM.nes 
SET(0x00006,0x42)
SET(0x2912D,0x50)
SET(0x2B2C1,0xE0)
SET(0x2B2C2,0xBF)
SET(0x2B2C3,0xAA)
SET(0x2BFF0,0x20)
SET(0x2BFF1,0xAA)
SET(0x2BFF2,0xDD)
SET(0x2BFF3,0xAA)
===================================================================


Note:
In regular expressions, the '^' denotes the beginning of a line, 
the '$' denotes the ending of a line.
===================================================================