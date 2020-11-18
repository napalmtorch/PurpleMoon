# PurpleMoon
  Less graphically ambitious than my previous OS, with managed code execution!
Youtube Channel: https://www.youtube.com/channel/UC9ZaDKg7dBUzoJx6gk-e26w

# Current Features
- Command-line interface
- Customizable titlebar/taskbar containing date, time, and title
- Command parser with some built in commands for managing the file system, help information, power options, and customization
- System configuration to customize color scheme and user properties
- Text editor
- Custom bytecode-interpreter with assembler and debugging tools

# Planned Features
- Calculator
- Live assembler interface
- High-resolution command-line interface
- Text-mode mouse support

# Ambitious Features
- Fullblown graphical subsystem with window manager and multitasking(I've somewhat done this in the previous version of this OS,
      but its quite buggy! It does look pretty nice however, and shows the potential of Cosmos.
- BASIC interpreter
- C compiler for custom bytecode-language


# Usage
  Simply open the project in visual studio, compile and run in VMWare.
  The only required external file by the operating system is the config file located at "0:\SYSTEM\USERCFG.PMC", however
  if the file and/or directory cannot be located at boot, the OS will attempt to generate it.
  
# PurpleMoon Assembler
  Include with this is my own assembly language designed specifically for Cosmos. As of now, the OS is able
  to write, assemble, and execute files directly from within the operating system! I've included a demo app 
  that draws a line to the screen, but I've yet to actually write much more programs. I plan on writing a handful
  of my OS's programs in my assembly language eventually! If you're interested in writing programs for my OS,
  or implementing this language in your own OS, read the "Readme ASM.txt" file to learn the language, and read
  the code carefully. The virtual machine works very well, however the assembler could be better.

 # Screenshots
 
![SC1](https://raw.githubusercontent.com/napalmtorch/PurpleMoon/main/Screenshot1.png)
![SC2](https://raw.githubusercontent.com/napalmtorch/PurpleMoon/main/Screenshot2.png)
![SC3](https://raw.githubusercontent.com/napalmtorch/PurpleMoon/main/Screenshot3.png)

