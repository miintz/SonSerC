#SonSerC

Song Service for Console

Console adaptation of a part from AnOminousSun

# Getting started

Open the .sln in Visual Studio and compile it, nuget package restore does it's magic and an error will pop up: 

"Could not copy the file "\x86\libtesseract302.dll..."

To fix this:

1. Go to solution folder (where the .sln is) and  go to **packages\Tesseract.2.4.1.0\content**
2. Copy the 2 folders in there (x64 and x86)
3. Paste them in the SonSerC folder in the solution's folder. 
4. Compile again. 
5. Run the SonSerC.exe tru MinGW or CMD. 

# Functions

3 functions are supported so far. Start by opening a terminal in the exe's folder, then use one of the following functions.

- pause/play

*SonSerC.exe pause*

Or

*SonSerC.exe play*

Pauses/unpauses current song. 

- Play album

*SonSerC.exe spotify:album:4un8HmKG7W3C7zpPpo6bfO*

This will play the album's first track. 

- Play track

SonSerC.exe spotify:track:1xjKtzJPJJACzsFnTgh5Al

This will play a specific track.  
 
 




