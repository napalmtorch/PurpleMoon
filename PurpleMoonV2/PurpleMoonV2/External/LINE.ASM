WORD msg, "Drawing line in assembly!"

JUMP store_data

main:
    CLS
    ADD $R2, 1
    CURSY $R2
    PRINTL 40, 3700
    JUMP draw_line

on_quit:
    ADD $R2, 4
    CURSX $R1
    CURSY $R2
    EXIT

draw_line:
    CURSX $RA
    CURSY $RC
    SNER $RA, $RC
    JUMP on_quit
    PIXAR $R0, $RA, $RB
    ADD $RA, 1
    JUMP draw_line

setup_registers:
    ADD $R0, 15		; color
    ADD $RA, 4 		; x coordinate
    ADD $RB, 3 		; y coordinate
    ADD $RC, 20 	; width
    JUMP main        

store_data:
    STORA msg, 3700 ; startup message
    JUMP setup_registers
