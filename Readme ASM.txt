--------------------------------------------------------------------------------
PurpleMoon Assembler Documentation
--------------------------------------------------------------------------------

  This document is written to help users understand how to write applications
  to run in PurpleMoon's custom assembly language. The purpose of this language
  is to allow users to develop and share applications between users running
  this platform. This is not mandatory, but if you make any applications,
  please share them with me! Even though I wrote the assembler, I'm still
  not very good at actually constructing applications at such a low level.

--------------------------------------------------------------------------------
PurpleMoon Virtual Machine Specs
--------------------------------------------------------------------------------
  
  This virtual machine is exactly the most powerful. Its similar to the Chip-8,
  however I've modified the drawing commands, as well as implemented an extra
  register, larger stack, and a few extra instructions to make programming a
  bit easier.

REGISTERS
  - R0-R15 are used as general-purpose registers, with register 15 being used
    as carry in math operations.

  - I is the index register, which cannot be referenced directly. Instead,
    there are a handful of instructions to manage this register.

  - W is the wait register, currently only used for reading keyboard input.
    When the WAIT instruction is called, it waits for a key press, then
    puts the key in the W register, then continues code execution.

STACK
  - A basic stack implementation, allowing up the 256 entries.

RAM
  - As of now, the memory is limited to 4KB(4096 bytes). I will work torwards
    increasing this value later on, if even necessary.

--------------------------------------------------------------------------------
Syntax
--------------------------------------------------------------------------------
  There isn't much syntax available for this assembler, however its enough
  to write somewhat complex applications while maintaining organization.

DECIMAL VALUES
  Decimal values are written exactly as they are, and require no syntax.
  For example:

  - ADD $RF, 140
  
  this will add the decimal value 140 to register 15.
  Registers can also be accessed using decimal values, for example the
  same instruction using only decimal values would look like:

  - ADD R15, 140

HEXIDECIMAL VALUES
  Hex values are written with a dollar sign($) in front of them
  For example, the command above using only hex would look like:

  - ADD $RF, $8C

CONSTANTS
  Right now, there is very basic constant implementation, which simply
  takes the pointer of the constant, and converts it to its hardcoded value
  apon assembly. For strings(WORD), you cannot directly use them other than
  to store it in memory. Once it it stored in memory, you will have to access
  it using its assigned memory address, for example:

  - WORD myMsg, "This is my message!"
  - STORA myMsg, 3700
  - PRINTL myMsg

  In this example, the first 2 instructions are completely valid, however
  you cannot call the constant from PRINTL, instead the arguments would
  need to reference the memory address, and also need a fixed length of the
  string. The code should actually look something like this:
 
  - WORD myMsg, "This is my working message!"
  - STORA myMsg, 3700
  - PRINTL 27, 3700

  This tells the assembler to print a line of text thats 27 characters long
  at memory address decimal 3700.

  Constant BYTE, SHORT, and CHAR, unlike WORD, are actually able to be
  directly referenced in place of their value.
  For example:

  - BYTE myByte, $12
  - SHORT myShort, $FD0
  - CHAR myChar, 'X'
  - ADD $RF, myByte
  - STORA myByte, 3000
  - STORA myChar, 3001
  - LOADI myShort

  This example will create 3 constants to use, save myByte($12) to
  register 15, store myByte at memory address 3000, store myChar
  at memory address 3001, then load myShort into the I register.

--------------------------------------------------------------------------------
PurpleMoon Instruction Set
--------------------------------------------------------------------------------

MATH
  01 ADD	Add byte to register
  02 ADDR	Add register to register
  03 ADDI	Add short to I register
  04 SUB	Subtract byte from register
  05 SUBR	Subtract register from register
  06 SUBI	Subtract short from I register
  07 AND 	Bitwise AND register and register
  08 OR 	Bitwise OR register and register
  09 XOR 	Bitwise Exclusive OR register and register
  0A SHL 	Shift register left by amount
  0B SHR 	Shift register right by amount
  0C SE 	Skip next instruction if register equal to byte
  0D SER 	Skip next instruction if register equal to register
  0E SNE	Skip next instruction if register not equal to byte
  0F SNER 	Skip next instruction if register not equal to register

DATA
  10 LOAD 	Load byte from address into register
  11 LOADW 	Copy data from W into register
  12 LOADI 	Load address into I register
  13 STOR 	Store register to address
  14 STORA	Store constant value to address
  15 STORW 	Store W register to address
  16 STORI	Store register to address in I register

PROGRAM
  20 JUMP 	Jump to address
  21 JUMPI 	Jump to address in I register
  22 CALL 	Call address, push program counter to stack
  23 CALLI 	Call address at I, push program counter to stack
  24 RET	Return from subroutine

SYSTEM
  30 HALT 	Halts the virtual machine
  31 EXIT 	Returns to operating system
  32 WAIT	Wait for keyboard input, store into W register

GRAPHICS
  40 CLS 	Clears the screen
  41 PRINT	Write data from address based on length
  42 PRINTL     Write new line from address based on length
  43 PRINTI     Write data from address at I based on length
  44 PRINTIL    Write new line from address at I based on length
  45 FCOL	Changes foreground color
  46 BCOL	Changes background color
  47 PIX	Draw constant color at constant X and Y
  48 PIXR	Draw color in register at constant X and Y
  49 PIXA       Draw constant color at register and register
  4A PIXAR	Draw color in register at register and register
  4B CURSX      Set cursor X to register
  4C CURSY      Set cursor Y to register
  4D CURGX      Load cursor X into register
  4E CURGY      Load cursor Y into register

--------------------------------------------------------------------------------
