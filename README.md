# DeepClean

Is a Visual Studio for macOS add-in / extension that let you easily clean projects, NuGet, Xamarin and VS cache without leaving the IDE.<br/>
**This extension is making it's first steps, please make sure you have a back up of your code before using it!**

## Features

Under the "Build" menu:
* Delete /bin /obj directories
    * Recursively delete all the /bin & /obj directories on the solution level
* Delete /packages directory

Right click on any directory within the solution:
* Open terminal at directory

Under the "Tools" menu:
* Clear NuGet Cache
    * Delete the next directories ~/.nuget/packages & ~/.local/share/NuGet
* Clear Android Library Cache
    * Delete all the directories within ~/.local/share/Xamarin that matching the next search pattern 'Xamarin.*;
* Clear Unused Framework Libraries
    * Delete all except current version within:
        * /Library/Frameworks/Mono.framework/Versions
        * /Library/Frameworks/Xamarin.Android.framework/Versions
        * /Library/Frameworks/Xamarin.iOS.framework/Versions
        * /Library/Frameworks/Xamarin.Mac.framework/Versions

Please note that after manipulating NuGet cache, both local or global, you will have to restore NuGet packages for your workspace.

## Getting Started

Currently the extension is not distributed on the official channels, so in order to use it you will have to download and install the mpack manually.

## Installation

### Automatic

You can download and install DeepClean using the Extension Manager of Visual Studio for Mac by searching the Gallery.

### Manual

Alternatively you can download and install it manually using the folowing steps:

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

The aim of this project is to practice and experiment with Visual Studio for macOS extensions development.
To play around with this solution just clone this repository and open it with you VS. To build a mpack package, build the solution in Release mode and execute *pack_addin.sh* the output will be redirected to *Versions* directory.

While development I used the next articles:<br/>
[Как сделать Xamarin Studio чуточку лучше?](https://habrahabr.ru/post/256393/)<br/>
[How to Write Add-ins of Visual Studio for Mac](https://blog.lextudio.com/how-to-write-add-ins-of-visual-studio-for-mac-ee6113db5ddf)<br/>
[Extending Visual Studio for Mac Walkthrough](https://docs.microsoft.com/en-us/visualstudio/mac/extending-visual-studio-mac-walkthrough)<br/>
[Cleaning Up Space on Your Xamarin Development Machine](https://montemagno.com/cleanup-up-space-xamarin-dev-machine/)

## Contributors

Big thanks to our contributors:
* [Giorgos Sgouridis](https://github.com/sgou)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
