# snake
A version of the classic Snake game written in C# and WinForms. 

![Example gameplay](/images/gameplay_static.png)

## Features
### Comfortable gameplay
Many basic Snake implementations will draw the snake as multiple shapes, which not only makes it hard to see which direction the snake is facing, where the snake's head is, etc, but it just looks plain ugly and like a blob on the screen.

This implementation draws the snake as a line, which not only makes it easy to see what direction the snake is facing, but it actually looks like a snake, and is much faster than drawing multiple shapes.

An input buffer is used to keep track of the user's inputs. This makes the game more responsive and makes sure no input is lost.

![Input buffer demonstration](/images/input_buffer.gif)

When you are about to game over, the game gives you a grace period to allow you to quickly turn away and save your run.

![Input buffer demonstration](/images/grace_period.gif)

### Customisation
You can change:
+ Field width and height
+ Tick interval
+ Graphics mode
+ Game scale

![Input buffer demonstration](/images/options_pane.png)

You can make your graphics mode by simply making a C# script and implementing the `SnakeRenderer` interface.

## Why did I make this?
Yes, Snake implementations miles ahead of my version do exist. 

I wrote this a while ago during my free time in between classes, and it's been sitting in my repos folder for a little over a month or so. However, I had a lot of fun making this and learning about the C# drawing library. My goal of this project was to make a minimal base implementation of Snake with some comfort features and high customisability. Yeah, I haven't reached these goals 100% but I am working towards it. 

You can choose to play this or not, you have free will. Browse the code or not, I don't mind (but if you do, thanks <3). The only guarantee I have is that this implementation of Snake is functional, and I hope you have as much fun with it as I did.

## Getting started
If you are not building from source, navigate to the Releases page.

The binaries are compressed in a ZIP file, which can be directly extracted using Windows Explorer/File Explorer. (If your version of Windows cannot do this, it is probably too old to run this game)

### Installing .NET
It is recommended you install a .NET runtime.

1. Visit https://dotnet.microsoft.com/en-us/download.
2. Download any version of .NET that is version 8.0 or later.
3. Optionally, verify the checksums of the installers. Do this if you are extremely worried about malware, however because it is an official Microsoft download link it is very unlikely it has been compromised. 
4. Run the .NET installer. Installing .NET does require administrative permissions, so you may be prompted with User Account Control and you may need to enter a password.
5. Finish the install and reboot the computer if necessary.

To check if .NET was successfully installed:
1. Open the Command Prompt (Windows 10 or earlier) or Terminal (Windows 11 and some versions of Windows 10)
2. Enter the command `dotnet --info`. If there is output (not `"dotnet" is not a recognised as an internal or external command...`), .NET has been successfully installed.
3. Scroll down to `.NET runtimes installed` and if you see `Microsoft.WindowsDesktop.App` 8.0.0 or later then the Windows Desktop App dependencies have been successfully installed.

### Which release do I pick?
The files are named like this:
`snake_{release}_{architecture}_{r2r_sc}.zip`

`{release}` refers to the version of Snake you are downloading.
`{architecture}` refers to what processor architecture the binary is. 

x64 systems will be able to run x86 binaries, but using the x64 binaries is recommended for better performance.

x86 will not be able to run x64 binaries. You must use the x86 binaries. 

If you don't know what processor architecture your computer has, check in System in the Control Panel/Settings app. 

Some binaries also have `{r2r_sc}` at the end of the filename. This indicates that the binaries are self-contained and standalone, and do not need a .NET runtime installed to launch the game. 

To check if you have .NET installed see [Installing .NET](#installing-net).

`r2r` refers to Ready To Run. 

These executables usually start faster and are portable (portable here means the binary is self-contained - it includes everything it needs to run and doesn’t require a pre-installed .NET runtime) however they are almost 1,000× larger than their non-standalone counterparts. So if for whatever reason you need to conserve 60MB or can't get a .NET runtime, use the self-contained binaries. Otherwise it is advised to use the non-standalone binaries with a .NET runtime. For instructions on installing .NET, see [Installing .NET](#installing-net).

### Building from source
You will need Visual Studio 2022 and a .NET SDK version 8.0 or higher. Make sure the .NET Desktop Development workload is installed. 

1. Open the solution file (`Snake.sln`). No NuGet packages or external dependencies (other than WinForms) are required.
2. Verify the configuration is correct. Release | Any CPU is recommended. 
3. Navigate to the Build menu, and you can choose to either build the solution (`F7`) or the project (`Ctrl+B`). By default, the binaries are built to `\bin\Release\net8.0-windows\`.

You can also choose to publish the solution. This gives you more control over how the binaries are compiled, like Ready To Run, self-contained binaries, compiling for different architectures, etc. 
1. Navigate to the Build menu and click Publish Selection.
2. Click publish. You may use the publish profile included with the project, however feel free to change any options. By default, the binaries are published to `\bin\release\net8.0-windows\publish`. 
