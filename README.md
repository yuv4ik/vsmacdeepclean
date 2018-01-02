# DeepClean

Is a Visual Studio for macOS add-in / extension that brings you features like deleting /bin, /obj, /packages directories and opening a terminal window at any solution directory level without leaving the IDE.<br/>
**This extension is making it's first steps, please make sure you have a back up of your code before using it!**

## Features

Under the "Build" menu:
- Delete /bin /obj directories
- Delete /pacakges directory

Right click on any directory within the solution to use a "New Terminal at Directory" command.

Please note that after deleting /packages directory you will have to restore nuget packages for your workspace.

## Getting Started

Currenlty the extension is not distributed on the official channels, so in order to use it you will have to download and install the mpack manually.

### Installing

Download the latest version from [here](https://github.com/yuv4ik/vsmacdeepclean/tree/master/Versions). <br/>
Open Visual Studio for macOS and open the "Extensions..." menu<br/>
<img src="https://github.com/yuv4ik/vsmacdeepclean/raw/master/Graphics/0_install.png" width="250"><br/>
Now click on "Install from file" and choose the downloaded mpack<br/>
<img src="https://github.com/yuv4ik/vsmacdeepclean/raw/master/Graphics/1_install.png" width="400"><br/>
It may take few seconds to install but the result should be<br/>
<img src="https://github.com/yuv4ik/vsmacdeepclean/raw/master/Graphics/2_install.png" width="400"><br/>
<br/>
The extension will be enabled only when a solution is opened in VS.

## Development

### Prerequisites
In order to debug this solution you will have to install [MonoDevelop.AddinMaker](https://github.com/mhutch/MonoDevelop.AddinMaker).

The aim of this project is to practice and expirement with Visual Studio for macOS extensions development.
To play arround with this solution just clone this repository and open it with you VS. To build a mpack package, build the solution in Release mode and execute *pack_addin.sh* the output will be redirected to *Versions* directory.

While development I used the next articles:<br/>
[Как сделать Xamarin Studio чуточку лучше?](https://habrahabr.ru/post/256393/)<br/>
[How to Write Add-ins of Visual Studio for Mac](https://blog.lextudio.com/how-to-write-add-ins-of-visual-studio-for-mac-ee6113db5ddf)<br/>
[Extending Visual Studio for Mac Walkthrough](https://docs.microsoft.com/en-us/visualstudio/mac/extending-visual-studio-mac-walkthrough)<br/>

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
