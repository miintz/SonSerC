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

4 functions are supported so far. Start by opening a terminal in the exe's folder, then use one of the following functions.

- start

*SonSerC.exe start*

Starts Spotify and maximizes it. NOTE: Only works when Spotify is in the default roaming folder, it will not work on Mac. 

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
 
# Known problems

- The resolution check doesn't work very well

May cause the program to fail by miss-clicking. Works best with a 1920x1080 resolution.  

- Doesn't handle two screens at all

Make sure the Spotify window opens on your primary screen, else it will click whatever is on that screen which can cause problems. 

- Spotify Premium vs Free Spotify

*May* cause issues when trying to find the process. It should work out pretty well, but couldn't test with a free Spotify here.  

- start argument can fail with a nullreference. 
 
Happens when the Spotify window's title is empty. Not sure why that happens, possibly a bug in Spotify?

- No mac support

Well. Yeah. So there you go. 




