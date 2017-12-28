# DeepClean

Extension for Visual Studio for macOS that allows you to delete /bin, /obj and /packages directories via Build menu.

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

The aim of this project is to practice and expirement with Visual Studio for macOS extensions development.<br/>
In order to play arround with the solution locally without distributing the mpack package, all you have todo is to clone this repository 
and open it with you VS. Otherwise if you wish to distribute a mpack package you will have to use the next command:

``
/Applications/Visual\ Studio.app/Contents/MacOS/vstool 
setup pack /VSMacDeepClean/VSMacDeepClean/bin/Release/net461/VSMacDeepClean.dll
``

While development I used the next articles:<br/>
[Как сделать Xamarin Studio чуточку лучше?](https://habrahabr.ru/post/256393/)<br/>
[How to Write Add-ins of Visual Studio for Mac](https://blog.lextudio.com/how-to-write-add-ins-of-visual-studio-for-mac-ee6113db5ddf)<br/>
[Extending Visual Studio for Mac Walkthrough](https://docs.microsoft.com/en-us/visualstudio/mac/extending-visual-studio-mac-walkthrough)<br/>

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
